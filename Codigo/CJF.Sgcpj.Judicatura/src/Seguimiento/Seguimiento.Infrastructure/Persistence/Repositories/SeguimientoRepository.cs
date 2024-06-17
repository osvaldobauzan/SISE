using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Persistence;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Seguimiento.Infrastructure.Files;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PolyCache.Cache;
using Seguimientos = CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Infrastructure.Persistence.Repositories;
public class SeguimientoRepository : ISeguimientoRepository
{
    private readonly IStaticCacheManager _staticCacheManager;
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<SeguimientoRepository> _logger;
    private readonly IMapper _mapper;

    public SeguimientoRepository(IStaticCacheManager staticCacheManager,
                                 IConfiguration configuration,
                                 ApplicationDbContext dbContext,
                                 ILogger<SeguimientoRepository> logger,
                                 IMapper mapper)
    {
        _staticCacheManager = staticCacheManager;
        _configuration = configuration;
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
    }

    /// <summary>
    /// Obtiene la lista de seguimientos  al cargar la pagina a partir de la fecha inicio. fecha fin y catOrganismoId
    /// </summary>
    /// <param name="FechaIni"></param>
    /// <param name="FechaFin"></param>
    /// <param name="CatOrganismoId"></param>
    /// <returns></returns>
    public async Task<List<Seguimientos.Seguimiento>> getByList(Seguimientos.Seguimiento seguimiento)
    {

        SqlParameter[] parametros = {
            new SqlParameter("@pi_Fecha", seguimiento.FechaIni),
            new SqlParameter("@pi_FechaFin", seguimiento.FechaFin),
            new SqlParameter("@pi_CatOrganismoId", seguimiento.CatOrganismoId)
    };

        string sqlQuery = "SISE3.uspx_getSeguimientoExp";

        var result = await this._dbContext.ExecuteStoredProc<Seguimientos.Seguimiento>(sqlQuery, parametros);


        return result;

    }

    /// <summary>
    /// Inserta un seguimiento al leer el qr o teclar el expediente 
    /// </summary>
    /// <param name="CatOrganismoId"></param>
    /// <param name="AreaId"></param>
    /// <param name="EmpleadoId"></param>
    /// /// <param name="AsuntoNeunId"></param>
    /// <param name="Mensaje"></param>
    /// <param name="Tipo"></param>
    /// <param name="DocumentoId"></param>
    /// <returns></returns>
    public async Task<int> InsertarSeguimientoConFiltro(Seguimientos.Seguimiento seguimiento)
    {
        int Result = 0;
        long AsuntoNeunId;
        int AreaId;
        string DocumentoId;
        string cadena = seguimiento.QrString;
        Seguimientos.Documentos doc = new Seguimientos.Documentos();
        Seguimientojson dat = new Seguimientojson();
        DocumentosRepository DocModel = new DocumentosRepository(_staticCacheManager, _configuration, _dbContext, _mapper);
        if (seguimiento.QrString.StartsWith("{"))
        {
            seguimiento.QrString = SerializesQR.Serializes(seguimiento.QrString);

            dat = JsonConvert.DeserializeObject<Seguimientojson>(seguimiento.QrString);

            AsuntoNeunId = dat.Seguimiento.AsuntoNeun;
            seguimiento.AsuntoNeun = AsuntoNeunId;
            AreaId = await ObtieneArea(seguimiento);
            seguimiento.AreaId = AreaId;
            DocumentoId = dat.Seguimiento.DocumentoId;
        }
        else
        {
            if (ValidarExpediente(seguimiento.QrString))
            {
                seguimiento.Expediente = seguimiento.QrString;
                AsuntoNeunId = await ObtieneAsuntoNeun(seguimiento.QrString, seguimiento.CatOrganismoId);
                seguimiento.AsuntoNeun = AsuntoNeunId;
            }
            else {
                AsuntoNeunId = long.Parse(seguimiento.QrString.ToString());
                seguimiento.AsuntoNeun = AsuntoNeunId;
            }
            AreaId = await ObtieneArea(seguimiento); 
            seguimiento.AreaId = AreaId;
            DocumentoId = (await ObtieneDocumento(AsuntoNeunId)).ToString();
            seguimiento.DocumentoId = DocumentoId;
        }

        Seguimientos.Documentos documentos = new Seguimientos.Documentos();
        documentos.CatOrganismoId = seguimiento.CatOrganismoId;
        documentos.Id = DocumentoId;
        documentos.Neun = AsuntoNeunId;       

        doc = await DocModel.getDocumentos(documentos);
        if (doc != null)
        {

            SqlParameter[] parametros = {
                new SqlParameter("@pi_organismoId", seguimiento.CatOrganismoId),
                new SqlParameter("@pi_areaId", AreaId),
                new SqlParameter("@pi_empleadoId", seguimiento.EmpleadoId),
                new SqlParameter("@pi_asuntoNeun", AsuntoNeunId),
                new SqlParameter("@pi_descripcion", doc.Mensaje),
                new SqlParameter("@pi_status", 2),
                new SqlParameter("@pi_tipo", doc.Tipo),
                new SqlParameter("@pi_documentoId", DocumentoId)
                //new SqlParameter("@new_id", 0)
            };


            string sqlQuery = "[SISE3].[uspx_addSeguimientoQRAc]";

            var respuesta = await this._dbContext.ExecuteStoredProc<object?> (sqlQuery, parametros);
            if (respuesta != null)
            {
                return 1;
            }
            else
            { return 0; }
        }
        return 0;
    }
    /// <summary>
    
    /// </summary>
    /// <param name= Valida si la cadena contiene / para saber si es un AsuntoNeunId o un numero de Expediente></param>
    /// <returns></returns>
    private bool ValidarExpediente(string Expediente)
    {       
        string caracteresBuscar = "/";

        return Expediente.Any(caracter => caracteresBuscar.Contains(caracter));
 
    }
    /// <summary>
    /// Obtiene la lista de seguimientos  de un expediente a partir del asuntoAlias y el TipoAsunto
    /// </summary>
    /// <param name="Expediente"></param>
    /// <param name="TipoAsunto"></param>
    /// <returns></returns>

    public async Task<IEnumerable<Seguimientos.Seguimiento>> getAllExpediente(Seguimientos.Seguimiento seguimiento)
    {

        SqlParameter[] parametros = {
                new SqlParameter("@pi_AsuntoAlias", seguimiento.Expediente),
                new SqlParameter("@pi_TipoAsunto", seguimiento.TipoAsunto),
                new SqlParameter("@pi_CatOrganismoId", seguimiento.CatOrganismoId),
                new SqlParameter("@pi_TipoProcedimiento", seguimiento.TipoProcedimiento)

        };
        string sqlQuery = "[SISE3].[uspx_getSeguimientoAllExpediente]";

        var result = (IEnumerable<Seguimientos.Seguimiento>)(await this._dbContext.ExecuteStoredProc<Seguimientos.Seguimiento>(sqlQuery, parametros));
        return result;


    }
    /// <summary>
    /// Obtiene la lista de seguimientos  de un expediente a partir del expediente y el catOrganismoId
    /// </summary>
    /// <param name="Expediente"></param>
    /// <param name="CatOrganismoId"></param>
    /// <returns></returns>
    public async Task<List<Seguimientos.Seguimiento>> getBusca(Seguimientos.Seguimiento seguimiento)
    {
       
            SqlParameter[] parametros =
            {
                new SqlParameter("@pi_Expediente", seguimiento.Expediente),
                new SqlParameter("@pi_CatOrganismoId", seguimiento.CatOrganismoId)
            };

            var sqlQuery = "[SISE3].[uspx_getSeguimientoBusca]";
            var result = await this._dbContext.ExecuteStoredProc<Application.Common.Models.Seguimiento>(sqlQuery, parametros);

            return result;
        
    }

    /// <summary>
    /// Obtiene el informacion para el combo de expediente.
    /// </summary>
    /// <param ></param>       
    /// <returns></returns>
    public async Task<List<Seguimientos.Seguimiento>> getCombExp(Seguimientos.Seguimiento seguimiento)
    {
        SqlParameter[] parametros = {
            new SqlParameter("@pi_CatOrganismoId", seguimiento.CatOrganismoId),
            new SqlParameter("@pi_expediente", seguimiento.Expediente),
            new SqlParameter("@pi_tipoDocumento", seguimiento.TipoDocumento)
    };
        string sqlQuery = "SISE3.uspx_getSeguimientoExpediente";

        var result = await _dbContext.ExecuteStoredProc<Seguimientos.Seguimiento>(sqlQuery, parametros);
        return result;


    }

    /// <summary>
    /// Obtiene el informacion para el combo de asuntoa.
    /// </summary>
    /// <param ></param>       
    /// <returns></returns>
    public async Task<List<Seguimientos.Seguimiento>> getCombAsunto(Seguimientos.Seguimiento seguimiento)
    {
        SqlParameter[] parametros = {
            new SqlParameter("@pi_CatOrganismoId", seguimiento.CatOrganismoId),
            new SqlParameter("@pi_Expediente", seguimiento.Expediente),
            new SqlParameter("@pi_TipoAsunto", seguimiento.TipoAsunto),
            new SqlParameter("@pi_TipoDocumento", seguimiento.TipoDocumento),
            new SqlParameter("@pi_TipoProcedimiento", seguimiento.TipoProcedimiento)
    };
        string sqlQuery = "SISE3.uspx_getSeguimientoAsuntos";

        var result = await _dbContext.ExecuteStoredProc<Seguimientos.Seguimiento>(sqlQuery, parametros);

        return result;

    }

    /// <summary>
    /// Obtiene el informacion para el combo de asuntoa.
    /// </summary>
    /// <param ></param>       
    /// <returns></returns>
    public async Task<List<Seguimientos.Seguimiento>> getCombPartes(Seguimientos.Seguimiento seguimiento)
    {

        SqlParameter[] parametros = {
            new SqlParameter("@pi_CatOrganismoId", seguimiento.CatOrganismoId),
            new SqlParameter("@pi_Expediente", seguimiento.Expediente),
            new SqlParameter("@pi_TipoAsunto", seguimiento.TipoAsunto),
            new SqlParameter("@pi_TipoDocumento", seguimiento.TipoDocumento),
            new SqlParameter("@pi_Fecha", seguimiento.Fecha ),
            new SqlParameter("@pi_TipoProcedimiento", seguimiento.TipoProcedimiento)
    };
        string sqlQuery = "SISE3.uspx_getSeguimientoNombreParte";


        var result = await _dbContext.ExecuteStoredProc<Seguimientos.Seguimiento>(sqlQuery, parametros);
        return result;

    }
    /// <summary>
    /// Obtiene el seguimiento a un documento filtrado por Organismo y por fecha.
    /// </summary>
    /// <param name="catOrganismoId"></param>       
    /// <returns></returns>
    public async Task<List<Seguimientos.Seguimiento>> getTipoAsuntos(Seguimientos.Seguimiento seguimiento)
    {
        SqlParameter[] parametros = {
            new SqlParameter("@pi_CatOrganismoId", seguimiento.CatOrganismoId)

    };

        string sqlQuery = "[SISE3].[uspx_getTiposAsunto]";

        var result = await _dbContext.ExecuteStoredProc<Seguimientos.Seguimiento>(sqlQuery, parametros);
        return result;

    }




    public class Seguimientojson
    {
        public CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models.Seguimiento Seguimiento { get; set; }
    }
    public Task<int> ObtieneArea(Seguimientos.Seguimiento seguimiento)
    {

        SqlParameter[] parametros = {
            new SqlParameter("@pi_AsuntoNeunId", seguimiento.AsuntoNeun),
            new SqlParameter("@pi_EmpleadoId", seguimiento.EmpleadoId)
        };

        string sqlQuery = "[SISE3].[uspx_getArea]";

        var result =  _dbContext.ExecuteStoredProcObj<int>(sqlQuery, parametros);
        
            return result;
        
    }
    public Task<long> ObtieneAsuntoNeun(string Expediente,int CatOrganismoId)
    {

        SqlParameter[] parametros = {
            new SqlParameter("@pi_Expediente", Expediente),
            new SqlParameter("@pi_OrganismoId", CatOrganismoId)

        };

        string sqlQuery = "[SISE3].[uspx_getAsuntoNeun]";

        var result = _dbContext.ExecuteStoredProcObj<long>(sqlQuery, parametros);

        return result;

    }
    public Task<string> ObtieneDocumento(long asuntoNeunId)
    {

        SqlParameter[] parametros = {
            new SqlParameter("@pi_AsuntoNeunId", asuntoNeunId)

        };

        string sqlQuery = "[SISE3].[uspx_getDocumentoId]";

        var result = _dbContext.ExecuteStoredProcObj<string>(sqlQuery, parametros);

        return result;
    }
}







