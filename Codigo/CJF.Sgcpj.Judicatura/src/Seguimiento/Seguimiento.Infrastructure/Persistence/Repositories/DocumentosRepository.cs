using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PolyCache.Cache;
using Newtonsoft.Json;
using CJF.Sgcpj.Judicatura.Seguimiento.Infrastructure.Files;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Persistence;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Data;
using IdentityModel.Client;
using Dapper;
using Seguimientos = CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;
using System.Globalization;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;


namespace CJF.Sgcpj.Judicatura.Seguimiento.Infrastructure.Persistence.Repositories;
/// <summary>
/// /////////////      INTERFASE PARA EL DOCUMENTOS DE SEGUIMIENTO ////////////////////////////////
/// </summary>
public class DocumentosRepository : IDocumentosRepository
{

    private readonly IStaticCacheManager _staticCacheManager;
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public DocumentosRepository(
    IStaticCacheManager staticCacheManager,
        IConfiguration configuration,
        ApplicationDbContext dbContext,
        IMapper mapper)
    {
        _staticCacheManager = staticCacheManager;
        _configuration = configuration;
        _dbContext = dbContext;
        _mapper = mapper;
    }

    /// <summary>
    /// Obtiene el documento a partir de la información del código de barras que se le pasó.
    /// </summary>
    /// <param name="documentoId">
    /// El número que contiene el código de barras.
    /// </param>
    /// <returns>
    /// El documento según el tipo al que se refiere el código de barras.
    /// </returns>
    public async Task<Seguimientos.Documentos> getDocumentos(Seguimientos.Documentos documento)
    {
        Seguimientos.Documentos doc = null;


        if (string.IsNullOrEmpty(documento.Id))
            return doc;
        Seguimientos.CatTipoDocumento tipo = (Seguimientos.CatTipoDocumento)int.Parse(documento.Id.Substring(0, 1).ToString());

        switch (tipo)
        {
            case Seguimientos.CatTipoDocumento.Expediente:
                Seguimientos.ExpedienteObtener expediente = new Seguimientos.ExpedienteObtener();
                expediente.Id = documento.Id;
                expediente.AsuntoNeunId = int.Parse(documento.Neun.ToString());
                expediente.Neun = documento.Neun;
                doc = await getExpediente(expediente);
                if (doc != null)
                {
                    doc.Id = documento.Id;
                    doc.Tipo = Seguimientos.CatTipoDocumento.Expediente;
                }
                break;
            case Seguimientos.CatTipoDocumento.Promocion:
                Seguimientos.Promociones promocion = new Seguimientos.Promociones();
                promocion.Neun = documento.Neun;
                promocion.Id = documento.Id;
                promocion.OrganismoId = documento.CatOrganismoId;
                promocion.Neun= documento.Neun;

                doc = await getPromocion(promocion);
                if (doc != null)
                {
                    doc.Id = documento.Id;
                    doc.Tipo = Seguimientos.CatTipoDocumento.Promocion;
                }
                break;
            case Seguimientos.CatTipoDocumento.Acuerdo:
                Seguimientos.Acuerdo acuerdo = new Seguimientos.Acuerdo();
                acuerdo.Neun = documento.Neun;
                acuerdo.Id = documento.Id;
                acuerdo.OrganismoId = documento.CatOrganismoId;
                acuerdo.Neun = documento.Neun;
                doc = await getAcuerdo(acuerdo);
                if (doc != null)
                {
                    doc.Id = documento.Id;
                    doc.Tipo = Seguimientos.CatTipoDocumento.Acuerdo;
                }
                break;
            case Seguimientos.CatTipoDocumento.Oficio:
                Seguimientos.Oficio oficio = new Seguimientos.Oficio();
                oficio.Neun = documento.Neun;
                oficio.Id = documento.Id;
                oficio.CatOrganismoId = documento.CatOrganismoId;
                oficio.Neun = documento.Neun;
                doc = await getOficio(oficio);
                if (doc != null)
                {
                    doc.Id = documento.Id;
                    doc.Tipo = Seguimientos.CatTipoDocumento.Oficio;
                }
                break;
        }
        return doc;

    }

    /// <summary>
    /// Obtiene el expediente a partir de la información del código de barras que se le pasó.
    /// </summary>
    /// <param name="documentoId">
    /// El número que contiene el código de barras.
    /// </param>
    /// <returns>
    /// El documento según el tipo al que se refiere el código de barras.
    /// </returns>
    /// <remarks>
    /// La distribución del código de barras es la siguiente:
    /// dígito [1]: indica el <see cref="CatTipoDocumento"/>.
    /// neun [11]:  indica el número de expediente único nacional, se rellenan con ceros a la izquierda los espacios no ocupados.
    /// </remarks>
    /// 
    public async Task<Seguimientos.ExpedienteObtener> getExpediente(Seguimientos.ExpedienteObtener expediente)
    {
        Seguimientos.ExpedienteObtener result = null;
        string neun = expediente.Neun.ToString();

        ExpedienteRepository model = new ExpedienteRepository(_staticCacheManager, _configuration, _dbContext, _mapper);

        result = await model.getExpediente(expediente);


        if (result != null)
        {
            result.Mensaje = string.Format(CultureInfo.CurrentCulture, "El expediente " + expediente.Id + " fue turnado correctamente.", result.Numero);
        }

        return result;

    }

    /// <summary>
    /// Obtiene la promoción a partir de la información del código de barras que se le pasó.
    /// </summary>
    /// <param name="documentoId">
    /// El número que contiene el código de barras.
    /// </param>
    /// <returns>
    /// El documento según el tipo al que se refiere el código de barras.
    /// </returns>
    /// <remarks>
    /// La distribución del código de barras es la siguiente: 
    /// dígito [1]:     indica el <see cref="CatTipoDocumento"/>.
    /// organismo[4]    indica el identificador del organismo al que pertenece la promoción.
    /// año[2]:         indica el año de la promoción en dos posiciones
    /// orden[5]:       indica el número de orden de la promoción.
    /// </remarks>
    public async Task<Seguimientos.Promociones> getPromocion(Seguimientos.Promociones promocion)
    {
        Seguimientos.Promociones result = new Seguimientos.Promociones();
        Seguimientos.Promociones Orden = null;
        int organismo;
        int? year;
        int? orden;

        PromocionRepository model = new PromocionRepository(_staticCacheManager, _configuration, _dbContext, _mapper);
        Orden = await model.getOrdenPromocion(promocion);
        if (Orden != null)
        {
            organismo = promocion.OrganismoId;
            year = Orden.YearPromocion;
            orden = Orden.Orden;
            //year = year >= 0 && year <= 90 ? year + 2000 : year + 1900;



            result = await model.getPromocion(promocion);
            if (result != null)
            {
                result.Mensaje = string.Format(CultureInfo.CurrentCulture, "La promoción " + promocion.Id + " fue turnada correctamente.", result.Numero);
            }
        }

        return result;
    }

    /// <summary>
    /// Obtiene el acuerdo a partir de la información del código de barras que se le pasó.
    /// </summary>
    /// <param name="documentoId">
    /// El número que contiene el código de barras.
    /// </param>
    /// <returns>
    /// El documento según el tipo al que se refiere el código de barras.
    /// </returns>Acuerdo getAcuerdo(Acuerdo filtro , int organismoId)

    public async Task<Seguimientos.Acuerdo> getAcuerdo(Seguimientos.Acuerdo acuerdo)
    {

        Seguimientos.Acuerdo result = new Seguimientos.Acuerdo();
        Seguimientos.Acuerdo filtro = new Seguimientos.Acuerdo();
        string neun = acuerdo.Neun.ToString();
        int Orden;

        AcuerdoRepository AcModel = new AcuerdoRepository(_staticCacheManager, _configuration, _dbContext, _mapper);

        Orden = await AcModel.getOrden(acuerdo);

        filtro.Neun = acuerdo.Neun;
        filtro.orden = Orden;
        filtro.organismoId = acuerdo.OrganismoId;

        AcuerdoRepository model = new AcuerdoRepository(_staticCacheManager, _configuration, _dbContext, _mapper);

        //result = await model.getAcuerdo(filtro);

        if (result != null)
        {
            result.Mensaje = string.Format(CultureInfo.CurrentCulture, "El acuerdo " + acuerdo.Id + " fue turnado correctamente.", filtro.Numero);
        }
        

        return result;
    }

    /// <summary>
    /// Obtiene el oficio a partir de la información del código de barras que se le pasó.
    /// </summary>
    /// <param name="documentoId">
    /// El número que contiene el código de barras.
    /// </param>
    /// <returns>
    /// El documento según el tipo al que se refiere el código de barras.
    /// </returns>
    /// <remarks>
    /// La distribución del código de barras es la siguiente: 
    /// digito [1]:     indica el <see cref="CatTipoDocumento"/>.
    /// Año [4]:        indica el año de la promoción en cuatro posiciones
    /// Folio [6]:      indica el número de folio de oficio 
    /// Tipo anexo [1]  indica el tipo de anexo 
    /// </remarks>        
    public async Task<Seguimientos.Oficio> getOficio(Seguimientos.Oficio oficio)
    {
        Seguimientos.Oficio result = new Seguimientos.Oficio();
        Seguimientos.Oficio Office = new Seguimientos.Oficio();

        OficioRepository model = new OficioRepository(_staticCacheManager, _configuration, _dbContext, _mapper);

        Office = await model.getFolioOficio(oficio);
        int year = Office.Anio;
        int folio = Office.Folio;
        int anexo = Office.TipoAnexo;

        result = await model.getOficio(oficio);

        if (result != null)
        {
            result.Mensaje = string.Format(CultureInfo.CurrentCulture, "La promoción " + oficio.Id + " fue turnada correctamente.", result.Numero);
        }

        return result;
    }


}