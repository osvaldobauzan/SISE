using PolyCache.Cache;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;
using CJF.Sgcpj.Judicatura.Tramite.Domain.Entities;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Persistence;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerFiltrosTramite;
using DocumentFormat.OpenXml.Presentation;
using AngleSharp.Html;
using CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerDetalleTramite;
using CJF.Sgcpj.Judicatura.Tramite.Application.Models;
using CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerArchivos;
using CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerDatosAudiencia;

namespace CJF.Sgcpj.Judicatura.Tramite.Infrastructure.Persistence.Repositories;
public class TramitesRepository : ITramitesRepository
{
    private readonly IStaticCacheManager _staticCacheManager;
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    public TramitesRepository(IConfiguration configuration, IStaticCacheManager staticCacheManager, ApplicationDbContext dbContext, IMapper mapper)
    {
        _configuration = configuration;
        _staticCacheManager = staticCacheManager;
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<(List<CJF.Sgcpj.Judicatura.Tramite.Domain.Entities.Tramite>, MetaDataEstadosTramite)> ObtenerTramitesConFiltro(ConsultaPaginadaTramite consultaPaginada)
    {
        var resultado = new ListaPaginada<CJF.Sgcpj.Judicatura.Tramite.Domain.Entities.Tramite, MetaDataEstadosTramite>();
        consultaPaginada.Texto = consultaPaginada?.Texto?.ToLower();

        string cachePrefix = "promociones_";
        int cacheExpiryTime = Convert.ToInt32(_configuration["duracionCache"]); //minitues
        if (Convert.ToBoolean(_configuration["habilitarCache"]))
        {
            return await _staticCacheManager.GetWithExpireTimeAsync(
                  new CacheKey(LlaveCache(consultaPaginada.FechaInicial, consultaPaginada.FechaFinal, consultaPaginada.Estado, consultaPaginada.Texto, cachePrefix)),
                  cacheExpiryTime,

                  async () => await ObtenerTramitesAsync(consultaPaginada));
        }
        else
        {
            return await ObtenerTramitesAsync(consultaPaginada);
        }
    }

    private async Task<(List<CJF.Sgcpj.Judicatura.Tramite.Domain.Entities.Tramite>, MetaDataEstadosTramite)> ObtenerTramitesAsync(ConsultaPaginadaTramite consultaPaginada)
    {
        consultaPaginada.Texto = consultaPaginada?.Texto?.ToLower();
        var metadata = new MetaDataEstadosTramite();
        var listaTablero = new List<CJF.Sgcpj.Judicatura.Tramite.Domain.Entities.Tramite>();

        List<SqlParameter> listaTableroSql = new List<SqlParameter>();
        var pi_CatOrganismoId = new SqlParameter("@pi_CatOrganismoId", consultaPaginada.OrganismoID);
        var pi_TamanoPagina = new SqlParameter("@pi_TamanoPagina", consultaPaginada.RegistrosPorPagina);
        var pi_NumeroPagina = new SqlParameter("@pi_NumeroPagina", consultaPaginada.Pagina);
        var pi_AsuntoNeunId = new SqlParameter("@pi_AsuntoNeunId", consultaPaginada.AsuntoNeunId);
        var pi_FechaPresentacionIni = new SqlParameter("@pi_FechaPresentacionIni", consultaPaginada.FechaInicial);
        var pi_FechaPresentacionFin = new SqlParameter("@pi_FechaPresentacionFin", consultaPaginada.FechaFinal);
        var pi_UsuariId = new SqlParameter("@pi_UsuariId", consultaPaginada.UsuariId);
        var pi_Texto = new SqlParameter("@pi_Texto", consultaPaginada.Texto);
        var pi_OrdenarPor = new SqlParameter("@pi_OrdenarPor", consultaPaginada.OrdenarPor);
        var pi_TipoOrden = new SqlParameter("@pi_TipoOrden", consultaPaginada.Descendente);
        var pi_FiltroTipo = new SqlParameter("@pi_FiltroTipo", consultaPaginada.Estado);
        var pi_SecretarioId = new SqlParameter("@pi_SecretarioId", consultaPaginada.SecretarioId);
        var pi_Origen = new SqlParameter("@pi_Origen", consultaPaginada.Origen);
        var pi_CatTipoAsuntoId = new SqlParameter("@pi_CatTipoAsuntoId", consultaPaginada.CatTipoAsuntoId);
        var pi_CapturoId = new SqlParameter("@pi_CapturoId", consultaPaginada.CapturoId);
        var pi_PreautorizoId = new SqlParameter("@pi_PreautorizoId", consultaPaginada.PreautorizoId);
        var pi_AutorizoId = new SqlParameter("@pi_AutorizoId", consultaPaginada.AutorizoId);
        var pi_CanceloId = new SqlParameter("@pi_CanceloId", consultaPaginada.CanceloId);


        listaTableroSql.Add(pi_CatOrganismoId);
        listaTableroSql.Add(pi_TamanoPagina);
        listaTableroSql.Add(pi_NumeroPagina);
        listaTableroSql.Add(pi_AsuntoNeunId);
        listaTableroSql.Add(pi_FechaPresentacionIni);
        listaTableroSql.Add(pi_FechaPresentacionFin);
        listaTableroSql.Add(pi_UsuariId);
        listaTableroSql.Add(pi_Texto);
        listaTableroSql.Add(pi_OrdenarPor);
        listaTableroSql.Add(pi_TipoOrden);
        listaTableroSql.Add(pi_FiltroTipo);
        listaTableroSql.Add(pi_SecretarioId);
        listaTableroSql.Add(pi_Origen);
        listaTableroSql.Add(pi_CatTipoAsuntoId);
        listaTableroSql.Add(pi_CapturoId);
        listaTableroSql.Add(pi_PreautorizoId);
        listaTableroSql.Add(pi_AutorizoId);
        listaTableroSql.Add(pi_CanceloId);

        var (metadatos, datos) = await _dbContext.ExecuteStoredProc<ContadoresEstadosTramiteTablero, TramiteItemTablero>("SISE3.pcTableroTramites", listaTableroSql.ToArray());
        listaTablero = datos.Select(x => _mapper.Map<CJF.Sgcpj.Judicatura.Tramite.Domain.Entities.Tramite>(x)).ToList();

        metadata.TotalSinAcuerdo = metadatos.FirstOrDefault()?.SinAcuerdo ?? 0;
        metadata.TotalCancelados = metadatos.FirstOrDefault()?.Cancelados ?? 0;
        metadata.TotalConAcuerdo = metadatos.FirstOrDefault()?.ConAcuerdo ?? 0;
        metadata.TotalPreAutorizados = metadatos.FirstOrDefault()?.PreAutorizados ?? 0;
        metadata.TotalAutorizados = metadatos.FirstOrDefault()?.Autorizados ?? 0;
        metadata.TotalTramites = metadatos.FirstOrDefault()?.Total ?? 0;

        return await Task.FromResult((listaTablero, metadata));

    }

    private static string LlaveCache(DateTime fechaInicial, DateTime fechaFinal, int estado, string texto, string cachePrefix)
    {
        return cachePrefix + fechaInicial.ToString("yyyy-MM-dd") + fechaFinal.ToString("yyyy-MM-dd") + estado.ToString() + texto;
    }

    public async Task<IEnumerable<Ruta>?> RutaArchivo(string modulo)
    {

        List<SqlParameter> parametros = new List<SqlParameter>();

        var pi_Modulo = new SqlParameter("@pi_Modulo", modulo);
        parametros.Add(pi_Modulo);

        var rutas = await _dbContext.ExecuteStoredProc<RutasNas>("[SISE3].[pcRutasChunkXModulo]", parametros.ToArray());

        var ruta = rutas.FirstOrDefault(s => s.Iescritura);

        if (ruta == null)
        {
            throw new Exception("No existe ruta configurada para guardar el documento");
        }

        return rutas is null ? null : rutas.Select(r => new Ruta
        {
            RutaId = r.KId,
            RutaNas = r.Sruta
        }).ToList();
    }

    public async Task<DatosDocumento> GuardarDocumentoAcuerdo(AgregarDocumento agregarDocumento)
    {

        var resultado = new DatosDocumento();

        List<SqlParameter> parametros = new List<SqlParameter>();

        var pi_AsuntoNeunId = new SqlParameter("@pi_AsuntoNeunId", agregarDocumento.AsuntoNeunId);
        parametros.Add(pi_AsuntoNeunId);

        var pi_NombreDocumento = new SqlParameter("@pi_NombreDocumento", agregarDocumento.NombreDocumento);
        parametros.Add(pi_NombreDocumento);

        var pi_ExtensionDocumento = new SqlParameter("@pi_ExtensionDocumento", agregarDocumento.ExtensionDocumento);
        parametros.Add(pi_ExtensionDocumento);
        var pi_Contenido = new SqlParameter("@pi_Contenido", agregarDocumento.Contenido);
        parametros.Add(pi_Contenido);
        var pi_TipoCuaderno = new SqlParameter("@pi_TipoCuaderno", agregarDocumento.TipoCuaderno);
        parametros.Add(pi_TipoCuaderno);
        var pi_FechaAuto = new SqlParameter("@pi_FechaAcuerdo", agregarDocumento.FechaAcuerdo);
        parametros.Add(pi_FechaAuto);
        var pi_IPUsuario = new SqlParameter("@pi_IPUsuario", "192.169.0.2"/*agregarDocumento.IpUsuario*/);
        parametros.Add(pi_IPUsuario);
        var pi_UsuarioCaptura = new SqlParameter("@pi_UsuarioCaptura", agregarDocumento.UsuarioCaptura);
        parametros.Add(pi_UsuarioCaptura);
        var pi_SintesisOrden = new SqlParameter("@pi_SintesisOrden", agregarDocumento.SintesisOrden);
        parametros.Add(pi_SintesisOrden);

        parametros.Add(new SqlParameter("@pi_AgendaId", agregarDocumento.AgendaId));
        parametros.Add(new SqlParameter("@pi_ResultadoId", agregarDocumento.ResultadoId));


        SqlParameter pi_PromocionesDeterminacion = new SqlParameter("@pi_PromocionesDeterminacion", agregarDocumento.PromocionesDeterminacion?.Select(x => x.toSqlDataRecord()));
        pi_PromocionesDeterminacion.SqlDbType = SqlDbType.Structured;
        pi_PromocionesDeterminacion.TypeName = "SISE3.PromocionesAcuerdo_type";
        parametros.Add(pi_PromocionesDeterminacion);

        var partePromoNotificacion = new List<PromocionAcuerdoPersonasDto>();
        if (agregarDocumento.PersonasNotificacionIndividual?.Count > 0)
        {
            partePromoNotificacion = agregarDocumento.PersonasNotificacionIndividual;
        }
        else
        {
            var noti = new PromocionAcuerdoPersonasDto();
            noti.PersonaId = null;
            noti.PromoventeId = null;
            noti.TipoNotificacionId = null;
            noti.TipoConstanciaId = null;
            noti.DescripcionConstancia = null;
            noti.TipoPromovente = null;
            noti.NumIntentosNotificacion = null;
            noti.TextoOficioLibre = null;
            noti.NombreParte = null;
            noti.DescripcionPromovente = null;
            noti.TipoAnexoId = null;
            partePromoNotificacion.Add(noti);
        }

        SqlParameter pi_PartePromoventeNotificacion = new SqlParameter("@pi_PartePromoventeNotificacion", partePromoNotificacion.Select(x => x.toSqlDataRecord()));
        pi_PartePromoventeNotificacion.SqlDbType = SqlDbType.Structured;
        pi_PartePromoventeNotificacion.TypeName = "SISE3.PersonaPromoventeNotificacion_type";
        parametros.Add(pi_PartePromoventeNotificacion);


        // Parámetros de salida
        SqlParameter po_NombreArchivo = new SqlParameter("@po_NombreArchivo", SqlDbType.VarChar, 50);
        po_NombreArchivo.Direction = ParameterDirection.Output;
        parametros.Add(po_NombreArchivo);

        SqlParameter po_AsuntoDocumentoId = new SqlParameter("@po_AsuntoDocumentoId", SqlDbType.Int);

        if (agregarDocumento.AsuntoDocumentoId == null)
        {
            po_AsuntoDocumentoId.Value = DBNull.Value;
        }
        else
        {
            po_AsuntoDocumentoId.Value = agregarDocumento.AsuntoDocumentoId;
        }

        po_AsuntoDocumentoId.Direction = ParameterDirection.InputOutput;
        parametros.Add(po_AsuntoDocumentoId);

        string sqlQuery = "[SISE3].[piInsertaActualizaDocumentoAcuerdo]";
        resultado = (await _dbContext.ExecuteStoredProc<DatosDocumento>(sqlQuery, parametros.ToArray())).FirstOrDefault();

        return resultado;
    }

    public Task<bool> ActualizarArchivo(AgregarDocumento agregarDocumento)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ActualizaEstadoAcuerdo(EstadoAcuerdo acuerdo)
    {
        var resultado = false;
        List<SqlParameter> parametrosActualizarEstado = PreparaParametrosActualizarAcuerdo(acuerdo);

        string sqlQuery = "[SISE3].[paActualizaEstadoAcuerdo]";
        await _dbContext.ExecuteStoredProcObj<object?>(sqlQuery, parametrosActualizarEstado.ToArray());
        resultado = true;

        return resultado;
    }
    public async Task<bool> ActualizaEstadoAcuerdoBre(string nombreSp, EstadoAcuerdo acuerdo)
    {
        var resultado = false;

        string sqlQuery = nombreSp;
        List<SqlParameter> parametrosActualizarEstado = new List<SqlParameter>();

        var pi_AsuntoNeunId = new SqlParameter("@pi_AsuntoNeunId", acuerdo.AsuntoNeunId);
        var pi_AsuntoDocumentoId = new SqlParameter("@pi_AsuntoDocumentoId", acuerdo.AsuntoDocumentoId);
        var pi_EmpleadoId = new SqlParameter("@pi_EmpleadoId", acuerdo.Valor);
        var pi_NombreDocumento = new SqlParameter("@pi_NombreDocumento", acuerdo.NombreDocumento);
        parametrosActualizarEstado.Add(pi_AsuntoNeunId);
        parametrosActualizarEstado.Add(pi_AsuntoDocumentoId);
        parametrosActualizarEstado.Add(pi_EmpleadoId);
        parametrosActualizarEstado.Add(pi_NombreDocumento);

        await _dbContext.ExecuteStoredProcObj<object?>(sqlQuery, parametrosActualizarEstado.ToArray());
        resultado = true;

        return resultado;
    }

    private static List<SqlParameter> PreparaParametrosActualizarAcuerdo(EstadoAcuerdo acuerdo)
    {
        List<SqlParameter> parametrosActualizarEstado = new List<SqlParameter>();

        var pi_AsuntoNeunId = new SqlParameter("@pi_AsuntoNeunId", acuerdo.AsuntoNeunId);
        var pi_AsuntoDocumentoId = new SqlParameter("@pi_AsuntoDocumentoId", acuerdo.AsuntoDocumentoId);
        var pi_TipoUpdate = new SqlParameter("@pi_TipoUpdate", acuerdo.TipoUpdate);
        var pi_Valor = new SqlParameter("@pi_Valor", acuerdo.Valor);
        var pi_NombreDocumento = new SqlParameter("@pi_NombreDocumento", acuerdo.NombreDocumento);
        parametrosActualizarEstado.Add(pi_AsuntoNeunId);
        parametrosActualizarEstado.Add(pi_AsuntoDocumentoId);
        parametrosActualizarEstado.Add(pi_TipoUpdate);
        parametrosActualizarEstado.Add(pi_Valor);
        parametrosActualizarEstado.Add(pi_NombreDocumento);
        return parametrosActualizarEstado;
    }

    public async Task<bool> EliminarAcuerdo(EliminarAcuerdo acuerdo)
    {
        var resultado = false;
        List<SqlParameter> parametros = new List<SqlParameter>();
        var pi_AsuntoNeunId = new SqlParameter("@pi_AsuntoNeunId", acuerdo.AsuntoNeunId);
        var pi_AsuntoDocumentoId = new SqlParameter("@pi_AsuntoDocumentoId", acuerdo.AsuntoDocumentoId);
        var pi_catIdOrganismo = new SqlParameter("@pi_catIdOrganismo", acuerdo.catIdOrganismo);
        parametros.Add(pi_AsuntoNeunId);
        parametros.Add(pi_AsuntoDocumentoId);
        parametros.Add(pi_catIdOrganismo);

        string sqlQuery = "[SISE3].[peEliminaAcuerdo]";
        await _dbContext.ExecuteStoredProcObj<string>(sqlQuery, parametros.ToArray());
        resultado = true;

        return resultado;
    }

    public async Task<(List<CabeceraTramite>, List<Promociones>, List<Partes>, List<Oficio>)> ObtenerDetalleTramiteAsync(ObtieneDetalleTramiteConsulta consultaPaginada)
    {
        List<SqlParameter> listaTableroSql = new List<SqlParameter>();

        var pi_CatOrganismoId = new SqlParameter("@pi_CatOrganismoId", consultaPaginada.catOrganismoId);
        var pi_TamanoPagina = new SqlParameter("@pi_AsuntoNeunId", consultaPaginada.asuntoNeunId);
        var pi_NumeroPagina = new SqlParameter("@pi_SintesisOrden", consultaPaginada.sintesisOrden);
        var pi_AsuntoNeunId = new SqlParameter("@pi_AsuntoDocumentoId", consultaPaginada.asuntoDocumentoId);

        listaTableroSql.Add(pi_CatOrganismoId);
        listaTableroSql.Add(pi_TamanoPagina);
        listaTableroSql.Add(pi_NumeroPagina);
        listaTableroSql.Add(pi_AsuntoNeunId);

        var (res1, res2, res3, res4) =
            await _dbContext.ExecuteStoredProcFour<CabeceraTramite, Promociones, Partes, Oficio>("[SISE3].[pcDetalleTableroTramite]", listaTableroSql.ToArray());

        return await Task.FromResult((res1, res2, res3, res4));
    }


    public async Task<List<ArchivosAnexos>> ObtenerArchivosyAnexos(long asuntoNeunId, int numeroOrden, int yearPromocion, int catIdOrganismo, int origen, int modulo, int asuntoDocumentoId)
    {
        var parametros = new[]
{
                new SqlParameter("@pi_AsuntoNeunId", asuntoNeunId),
                new SqlParameter("@pi_YearPromocion", yearPromocion),
                new SqlParameter("@pi_NumeroOrden", numeroOrden),
                new SqlParameter("@pi_catIdOrganismo", catIdOrganismo),
                new SqlParameter("@pi_Origen", origen),
                new SqlParameter("@pi_TipoModulo", modulo),
                new SqlParameter("@pi_AsuntoDocumentoId", asuntoDocumentoId),


            };

        string sqlQuery = "SISE3.pcConsultaArchivosyRutaXModulo";

        var archivos = await _dbContext.ExecuteStoredProc<ArchivosAnexos?>(sqlQuery, parametros);

        return archivos;
    }

    public async Task<(List<ObtieneFiltroSecretario>, List<ObtieneFiltroOrigen>, List<ObtieneFiltroTipoAsunto>, List<ObtieneFiltroCapturo>,
        List<ObtieneFiltroPreautorizo>, List<ObtieneFiltroAutorizo>, List<ObtieneFiltroCancelo>)> ObtenerFiltroTramite(ObtieneFiltroTramiteConsulta request)
    {
        List<SqlParameter> listaTableroSql = new List<SqlParameter>();

        var pi_catIdOrganismo = new SqlParameter("@pi_catIdOrganismo", request.CatOrganismoId);

        listaTableroSql.Add(pi_catIdOrganismo);

        var (result1, result2, result3, result4, result5, result6, result7) =
            await _dbContext.ExecuteStoredProcSeven<ObtieneFiltroSecretario, ObtieneFiltroOrigen, ObtieneFiltroTipoAsunto, ObtieneFiltroCapturo,
            ObtieneFiltroPreautorizo, ObtieneFiltroAutorizo, ObtieneFiltroCancelo>("[SISE3].[pcObtieneFiltrosTramite]", listaTableroSql.ToArray());

        return await Task.FromResult((result1, result2, result3, result4, result5, result6, result7));
    }

    public async Task<bool> GetStatusUNC(int catIdOrganismo)
    {
        var poUncStatus = new SqlParameter("@po_UNCEstado", SqlDbType.Int);
        var piCatOrganismoId = new SqlParameter("@pi_CatOrganismoId", SqlDbType.Int);

        poUncStatus.Direction = ParameterDirection.Output;

        piCatOrganismoId.Direction = ParameterDirection.Input;
        piCatOrganismoId.Value = catIdOrganismo;

        var parametros = new[] { piCatOrganismoId, poUncStatus, };

        string sqlQuery = "SISE3.pcValidaUNC";
        await _dbContext.ExecuteStoredProcObj<object?>(sqlQuery, parametros);
        return ((int)poUncStatus.Value) == 1;
    }

    public async Task<string> ObtenerNumeroOficio(int catOrganismoId, long asuntoNeunId, int asuntoDocumentoId, int anexoParteId)
    {
        string respuestaNumeroOficio; 
        List<SqlParameter> parametros = new List<SqlParameter>();
        var pi_CatOrganismo = new SqlParameter("@pi_CatOrganismo", catOrganismoId);
        var pi_AsuntoNeunId = new SqlParameter("@pi_AsuntoNeunId", asuntoNeunId);
        var pi_AsuntoDocumentoId = new SqlParameter("@pi_AsuntoDocumentoId", asuntoDocumentoId);
        var pi_AnexoParteId = new SqlParameter("@pi_AnexoParteId", anexoParteId);
        parametros.Add(pi_CatOrganismo);
        parametros.Add(pi_AsuntoNeunId);
        parametros.Add(pi_AsuntoDocumentoId);
        parametros.Add(pi_AnexoParteId);

        string sqlQuery = "[SISE3].[pcObtieneOficio]";
        respuestaNumeroOficio = await _dbContext.ExecuteStoredProcObj<string>(sqlQuery, parametros.ToArray());
        if (respuestaNumeroOficio == null)
            respuestaNumeroOficio = "";
        return respuestaNumeroOficio;
    }

    public async Task<IEnumerable<Application.Common.Models.Tramite>> ObtenerTramitePorIdModuloTipoAsync(Guid id, int modulo, string tipoArchivo)
    {
        var parametros = new List<SqlParameter>()
        {
            new SqlParameter("@pi_GuidDocumento", id.ToString()),
            new SqlParameter("@pi_TipoModulo", modulo.ToString()),
            new SqlParameter("@pi_TipoArchivo", tipoArchivo)
        };

        var sqlQuery = "[SISE3].[pcConsultaRutaXModuloGUID]";

        return await _dbContext.ExecuteStoredProc<Application.Common.Models.Tramite>(sqlQuery, parametros.ToArray());
    }
    public async Task<bool> ActualizaEstadoOficio(EstadoOficioRoot oficioRoot)
    {
        var parametros = new List<SqlParameter>()
        {
            new SqlParameter("@pi_GuidDocumento", oficioRoot.EstadoOficio.GuidDocumento),
            new SqlParameter("@pi_IdRuta", oficioRoot.EstadoOficio.RutaId),
            new SqlParameter("@pi_Nombre", oficioRoot.EstadoOficio.Nombre),
            new SqlParameter("@pi_Extension", oficioRoot.EstadoOficio.Extension),
            new SqlParameter("@pi_Firmado", oficioRoot.EstadoOficio.Firmado),
            new SqlParameter("@pi_AsuntoNeunId", oficioRoot.EstadoOficio.AsuntoNeunId),
            new SqlParameter("@pi_AsuntoDocumentoId", oficioRoot.EstadoOficio.AsuntoDocumentoId),
            new SqlParameter("@pi_AnexoParteId", oficioRoot.EstadoOficio.AnexoParteId),
            new SqlParameter("@pi_CatOrganismoId", oficioRoot.EstadoOficio.CatOrganismoId)
        };

        string sqlQuery = "[SISE3].[paActualizaEstadoOficio]";
        await _dbContext.ExecuteStoredProcObj<string>(sqlQuery, parametros.ToArray());
        return true;
    }

    public async Task<bool> ActualizaDeterminacion(DeterminacionAcuerdoRoot determinacionRoot)
    {
        var parametros = new List<SqlParameter>()
        {
            new SqlParameter("@pi_GUID", determinacionRoot.DeterminacionAcuerdo.GUID),
            new SqlParameter("@pi_Firmado", determinacionRoot.DeterminacionAcuerdo.Firmado),
            new SqlParameter("@pi_IdRuta", determinacionRoot.DeterminacionAcuerdo.IdRuta),
            new SqlParameter("@pi_Nombre", determinacionRoot.DeterminacionAcuerdo.Nombre),
            new SqlParameter("@pi_Extension", determinacionRoot.DeterminacionAcuerdo.Extension)
        };

        string sqlQuery = "[SISE3].[piInsertaActualizaDeterminacion]";
        await _dbContext.ExecuteStoredProcObj<string>(sqlQuery, parametros.ToArray());
        return true;
    }

    public async Task<List<InfoDocumentos>> ObtenerAcuerdosOficios(Guid acuerdoGuid)
    {
        var parametros = new List<SqlParameter>()
        {
            new SqlParameter("@pi_GuidAcuerdo", acuerdoGuid)
        };

        string sqlQuery = "[SISE3].[pcConsultaAcuerdoYOficios]";
        return await _dbContext.ExecuteStoredProc<InfoDocumentos>(sqlQuery, parametros.ToArray());

    }
    public async Task<(List<TiposAsuntoDTO>, List<TiposAudienciaDTO>, List<ResultadosAudienciaDTO>, List<AudienciasDTO>)> ObtenerDatosAudiencia(ObtenerDatosAudienciaFiltro request)
    {
        var parametros = new List<SqlParameter>()
        {
            new SqlParameter("@pi_AsuntoNeunId", request.AsuntoNeunId),
            new SqlParameter("@pi_IdTipoAgenda", request.TipoAgendaId),
        };

        string sqlQuery = "SISE3.pcTramiteObtenerDatosAudiencia";
        var(result1, result2, result3, result4) =  await _dbContext.ExecuteStoredProcFour<TiposAsuntoDTO,TiposAudienciaDTO, ResultadosAudienciaDTO, AudienciasDTO >(sqlQuery, parametros.ToArray());
        return await Task.FromResult((result1, result2, result3, result4));
    }
}
