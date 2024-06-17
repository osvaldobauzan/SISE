using System.Data;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.AgregarCOE;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.AgregarSintesisManual;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.DetalleNotificacionesFiltros;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.DetalleSintesis;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerSintesisManual;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Actuaria.Domain.Entities;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PolyCache.Cache;
using ArchivosAnexos = CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models.ArchivosAnexos;
using ConsultaPaginada = CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models.ConsultaPaginada;

namespace CJF.Sgcpj.Judicatura.Actuaria.Infrastructure.Persistence.Repositories;
public class ActuariaRepository : IActuariaRepository
{
    private readonly IConfiguration _configuration;
    private readonly IStaticCacheManager _staticCacheManager;
    private readonly ApplicationDbContext _dbContext;
    private readonly ISesionService _sesionService;
    private readonly IMapper _mapper;

    public ActuariaRepository(IConfiguration configuration, IStaticCacheManager staticCacheManager, ApplicationDbContext dbContext, IMapper mapper, ISesionService sesionService)
    {
        _configuration = configuration;
        _staticCacheManager = staticCacheManager;
        _dbContext = dbContext;
        _mapper = mapper;
        _sesionService = sesionService;
    }

    /// <summary>
    /// Obtiene una lista de empleados por organismo y cargo.
    /// </summary>
    /// <param name="catOrganismoId"></param>
    /// <param name="cargos"></param>
    /// <returns></returns>
    public async Task<List<Actuario>> ObtenerDetalleActuarios(int catOrganismoId)
    {
        List<SqlParameter> parametros = new List<SqlParameter>
        {
            new SqlParameter("@pi_CatOrganismoId", catOrganismoId),
         };

        var result = await _dbContext.ExecuteStoredProc<Actuario>("[SISE3].[sp_ObtenerEmpleadosPorOrganismoYCargo]", parametros.ToArray());

        return result;
    }

    /// <summary>
    /// Obtiene el detalle de los tiempos de Notificación/Llegada vs Asignación
    /// </summary>
    /// <param name="catOrganismoId"></param>
    /// <param name="catOrganismoId"></param>
    /// <param name="cargos"></param>
    /// <returns></returns>
    public async Task<List<DiferenciasTiempos>> ObtenerDetalleIntervalosTiempos(long empleadoId, DateTime fechaInicial, DateTime fechaFinal)
    {
        List<SqlParameter> parametros = new List<SqlParameter>
        {
             new SqlParameter("@EmpleadoId", empleadoId),
             new SqlParameter("@fechaInicio", fechaInicial),
             new SqlParameter("@fechaFin", fechaFinal)
         };

        var result = await _dbContext.ExecuteStoredProc<DiferenciasTiempos>("[SISE3].[spDiferenciaTiemposNotificacion]", parametros.ToArray());

        return result;
    }

    /// <summary>
    /// Obtiene una lista de empleados por organismo y cargo.
    /// </summary>
    /// <param name="catOrganismoId"></param>
    /// <param name="cargos"></param>
    /// <returns></returns>
    public async Task<List<ActuarioDetalleLista>> ObtenerDetalleConteos(int catOrganismoId, DateTime fechaInicial, DateTime fechaFinal)
    {
        var listaActuarios = await ObtenerDetalleActuarios(catOrganismoId);
        var responseList = new List<ActuarioDetalleLista>();
        foreach (var item in listaActuarios)
        {
            List<SqlParameter> parametros = new List<SqlParameter>
            {
            new SqlParameter("@pi_CatOrganismoId", catOrganismoId),
            new SqlParameter("@pi_FechaInicial", fechaInicial),
            new SqlParameter("@pi_FiltroActuarioID", item.EmpleadoId),
            new SqlParameter("@pi_FechaFinal", fechaFinal)
            };

            var result = await _dbContext.ExecuteStoredProcThree<NotificacionesPendientesPorDias, TotalNotificaciones, NotificacionesPorTipo>("[SISE3].[sp_ObtenerKipsIncialActuaria]", parametros.ToArray());
            var (notificacionesPendientesPorDias, totalNotificacionesList, notificacionesPorTipo) = result;

            var totalNotificaciones = totalNotificacionesList.FirstOrDefault();
            responseList.Add(new ActuarioDetalleLista() 
            {
                ListaTipos = notificacionesPorTipo,
                TotalNotificaciones = totalNotificaciones,
                Actuario = item
            });
        }
        return responseList;
    }

/// <summary>
/// Obtiene los indicadores iniciales para actuaría.
/// </summary>
/// <param name="catOrganismoId"></param>
/// <param name="filtroActuarioId"></param>
/// <param name="fechaInicial"></param>
/// <param name="fechaFinal"></param>
/// <returns></returns>
public async Task<(List<NotificacionesPendientesPorDias>, TotalNotificaciones, List<NotificacionesPorTipo>)> ObtenerNotificacionesPorTipoYPeriodo(int catOrganismoId, long filtroActuarioId, DateTime fechaInicial, DateTime fechaFinal)
    {
        var empleado = _sesionService.SesionActual.EmpleadoId;
        List<SqlParameter> parametros = new List<SqlParameter>
        {
            new SqlParameter("@pi_CatOrganismoId", catOrganismoId),
            new SqlParameter("@pi_FiltroActuarioID", filtroActuarioId),
            new SqlParameter("@pi_FechaInicial", fechaInicial),
            new SqlParameter("@pi_FechaFinal", fechaFinal)
        };

        var result = await _dbContext.ExecuteStoredProcThree<NotificacionesPendientesPorDias, TotalNotificaciones, NotificacionesPorTipo>("[SISE3].[sp_ObtenerKipsIncialActuaria]", parametros.ToArray());

        // result is a tuple of three lists
        var (notificacionesPendientesPorDias, totalNotificacionesList, notificacionesPorTipo) = result;

        // Assuming that the totalNotificacionesList contains only one element as we expect a single row result for totalNotificaciones
        var totalNotificaciones = totalNotificacionesList.FirstOrDefault();

        return (notificacionesPendientesPorDias, totalNotificaciones, notificacionesPorTipo);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="catOrganismoId"></param>
    /// <param name="filtroActuarioId"></param>
    /// <param name="fechaInicial"></param>
    /// <param name="fechaFinal"></param>
    /// <returns></returns>
    public async Task<List<NotificacionesPorTipoYMes>> ObtenerNotificacionesPorTipoYMes(int catOrganismoId, long filtroActuarioId, DateTime fechaInicial, DateTime fechaFinal)
    {
        List<SqlParameter> parametros = new List<SqlParameter>
        {
            new SqlParameter("@pi_CatOrganismoId", catOrganismoId),
            new SqlParameter("@pi_FiltroActuarioID", filtroActuarioId),
            new SqlParameter("@pi_FechaInicial", fechaInicial),
            new SqlParameter("@pi_FechaFinal", fechaFinal)
        };

        var result = await _dbContext.ExecuteStoredProc<NotificacionesPorTipoYMes>("[SISE3].[sp_ObtenerActuariaTipoMes]", parametros.ToArray());

        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="catOrganismoId"></param>
    /// <param name="filtroActuarioId"></param>
    /// <param name="fechaInicial"></param>
    /// <param name="fechaFinal"></param>
    /// <param name="mesSeleccionado"></param>
    /// <returns></returns>
    public async Task<List<NotificacionesPorTipoYSemana>> ObtenerNotificacionesPorTipoYSemana(int catOrganismoId, long filtroActuarioId, DateTime fechaInicial, DateTime fechaFinal, int mesSeleccionado)
    {
        List<SqlParameter> parametros = new List<SqlParameter>
        {
            new SqlParameter("@pi_CatOrganismoId", catOrganismoId),
            new SqlParameter("@pi_FiltroActuarioID", filtroActuarioId),
            new SqlParameter("@pi_FechaInicial", fechaInicial),
            new SqlParameter("@pi_FechaFinal", fechaFinal),
            new SqlParameter("@MesSeleccionado", mesSeleccionado)
        };

        var result = await _dbContext.ExecuteStoredProc<NotificacionesPorTipoYSemana>("[SISE3].[sp_ObtenerActuariaDetalleMes]", parametros.ToArray());

        return result;
    }

    public async Task<(List<Notificacion>, MetaDataEstados)> ObtenerNotificacionesConFiltro(ConsultaPaginada consultaPaginada)
    {
        var resultado = new ListaPaginada<Notificacion, MetaDataEstados>();
        consultaPaginada.Texto = consultaPaginada?.Texto?.ToLower();

        string cachePrefix = "promociones_";
        int cacheExpiryTime = Convert.ToInt32(_configuration["duracionCache"]); //minutes
        if (Convert.ToBoolean(_configuration["habilitarCache"]))
        {
            return await _staticCacheManager.GetWithExpireTimeAsync(
                  new CacheKey(LlaveCache(consultaPaginada.FechaInicial, consultaPaginada.FechaFinal, consultaPaginada.TipoFiltro, consultaPaginada.Texto, cachePrefix)),
                  cacheExpiryTime,

                  async () => await ObtenerNotificacionesAsync(consultaPaginada));
        }
        else
        {
            return await ObtenerNotificacionesAsync(consultaPaginada);
        }

    }

    private async Task<(List<Notificacion>, MetaDataEstados)> ObtenerNotificacionesAsync(ConsultaPaginada consultaPaginada)
    {
        consultaPaginada.Texto = consultaPaginada?.Texto?.ToLower();
        var metadata = new MetaDataEstados();
        var listaTablero = new List<Notificacion>();

        List<SqlParameter> listaTableroSql = new List<SqlParameter>();

        var pi_CatOrganismoId = new SqlParameter("@pi_CatOrganismoId", consultaPaginada.CatOrganismoId);
        var pi_TamanoPagina = new SqlParameter("@pi_TamanoPagina", consultaPaginada.RegistrosPorPagina);
        var pi_NumeroPagina = new SqlParameter("@pi_NumeroPagina", consultaPaginada.Pagina);
        var pi_AsuntoNeunId = new SqlParameter("@pi_AsuntoNeunId", consultaPaginada.AsuntoNeunId);
        var pi_FechaPresentacionIni = new SqlParameter("@pi_FechaAutorizacionIni", consultaPaginada.FechaInicial);
        var pi_FechaPresentacionFin = new SqlParameter("@pi_FechaAutorizacionFin", consultaPaginada.FechaFinal);
        var pi_Texto = new SqlParameter("@pi_Texto", consultaPaginada.Texto);
        var pi_OrdenarPor = new SqlParameter("@pi_OrdenarPor", consultaPaginada.OrdenarPor);
        var pi_TipoOrden = new SqlParameter("@pi_TipoOrden", consultaPaginada.Descendente);
        var pi_FiltroTipo = new SqlParameter("@pi_FiltroTipo", consultaPaginada.TipoFiltro);
        var pi_Estado = new SqlParameter("@pi_Estado", consultaPaginada.Estado);
        var pi_Contenido = new SqlParameter("@pi_Contenido", consultaPaginada.Contenido);


        listaTableroSql.Add(pi_CatOrganismoId);
        listaTableroSql.Add(pi_TamanoPagina);
        listaTableroSql.Add(pi_NumeroPagina);
        listaTableroSql.Add(pi_AsuntoNeunId);
        listaTableroSql.Add(pi_FechaPresentacionIni);
        listaTableroSql.Add(pi_FechaPresentacionFin);
        listaTableroSql.Add(pi_Texto);
        listaTableroSql.Add(pi_OrdenarPor);
        listaTableroSql.Add(pi_TipoOrden);
        listaTableroSql.Add(pi_FiltroTipo);
        listaTableroSql.Add(pi_Estado);
        listaTableroSql.Add(pi_Contenido);


        var (metadatos, datos) = await _dbContext.ExecuteStoredProc<ContadoresEstadosTablero, ActuariaItemTablero>("SISE3.pcTableroActuaria", listaTableroSql.ToArray());
        listaTablero = datos.Select(x => _mapper.Map<Notificacion>(x)).ToList();

        metadata.TotalNotificaciones = metadatos.FirstOrDefault()?.Todos ?? 0;
        metadata.TotalMasTresDias = metadatos.FirstOrDefault()?.TresDias ?? 0;
        metadata.TotalDosDias = metadatos.FirstOrDefault()?.DosDias ?? 0;
        metadata.TotalUnDia = metadatos.FirstOrDefault()?.UnDia ?? 0;
        metadata.TotalNotificados = metadatos.FirstOrDefault()?.Notificados ?? 0;

        return await Task.FromResult((listaTablero, metadata));

    }

    private static string LlaveCache(DateTime fechaInicial, DateTime fechaFinal, int estado, string texto, string cachePrefix)
    {
        return cachePrefix + fechaInicial.ToString("yyyy-MM-dd") + fechaFinal.ToString("yyyy-MM-dd") + estado.ToString() + texto;
    }

    public async Task<bool> GuardarSintesisAcuerdo(GuardarSintesisAcuerdo guardarSintesisAcuerdo)
    {
        var parametros = new[]
        {
            new SqlParameter("@pi_AsuntoNeunId", guardarSintesisAcuerdo.AsuntoNeunId),
            new SqlParameter("@pi_TipoCuaderno", guardarSintesisAcuerdo.TipoCuaderno),
            new SqlParameter("@pi_NombreDocumento", guardarSintesisAcuerdo.NombreDocumento),
            new SqlParameter("@pi_ExtensionDocumento", guardarSintesisAcuerdo.ExtensionDocumento),
            new SqlParameter("@pi_Contenido", guardarSintesisAcuerdo.Contenido),
            new SqlParameter("@pi_FechaAcuerdo", guardarSintesisAcuerdo.FechaAcuerdo),
            new SqlParameter("@pi_UsuarioCaptura", guardarSintesisAcuerdo.UsuarioCaptura),
            new SqlParameter("@po_AsuntoDocumentoId", guardarSintesisAcuerdo.AsuntoDocumentoId),
            new SqlParameter("@pi_Sintesis", guardarSintesisAcuerdo.Sintesis),
            new SqlParameter("@po_SintesisOrden", guardarSintesisAcuerdo.SintesisOrden),
            new SqlParameter("@po_NombreArchivo", guardarSintesisAcuerdo.NombreArchivo),
            new SqlParameter("@pi_FechaPublicacion",guardarSintesisAcuerdo.FechaPublicacion),
            new SqlParameter("@pi_Titular",guardarSintesisAcuerdo.Titular),
            new SqlParameter("@pi_Parte1",guardarSintesisAcuerdo.Parte1),
            new SqlParameter("@pi_Parte2",guardarSintesisAcuerdo.Parte2),
            new SqlParameter("@pi_ActuarioId",guardarSintesisAcuerdo.ActuarioId)
        };

        await _dbContext.ExecuteStoredProc<bool?>("[SISE3].[piInsertaActualizaSintesisAcuerdo]", parametros.ToArray());

        return true;
    }
    public async Task<bool> EditarSintesisAcuerdo(EditarSintesisAcuerdo editarSintesisAcuerdo)
    {
        var parametros = new[]
        {
            new SqlParameter("@pi_AsuntoNeunId", editarSintesisAcuerdo.AsuntoNeunId),
            new SqlParameter("@pi_TipoCuaderno", editarSintesisAcuerdo.TipoCuaderno),
            new SqlParameter("@pi_NombreDocumento", editarSintesisAcuerdo.NombreDocumento),
            new SqlParameter("@pi_ExtensionDocumento", editarSintesisAcuerdo.ExtensionDocumento),
            new SqlParameter("@pi_Contenido", editarSintesisAcuerdo.Contenido),
            new SqlParameter("@pi_FechaAcuerdo", editarSintesisAcuerdo.FechaAcuerdo),
            new SqlParameter("@pi_UsuarioCaptura", editarSintesisAcuerdo.UsuarioCaptura),
            new SqlParameter("@po_AsuntoDocumentoId", editarSintesisAcuerdo.AsuntoDocumentoId),
            new SqlParameter("@pi_Sintesis", editarSintesisAcuerdo.Sintesis),
            new SqlParameter("@po_SintesisOrden", editarSintesisAcuerdo.SintesisOrden),
            new SqlParameter("@po_NombreArchivo", editarSintesisAcuerdo.NombreArchivo),
            new SqlParameter("@pi_FechaPublicacion",editarSintesisAcuerdo.FechaPublicacion),
            new SqlParameter("@pi_Titular",editarSintesisAcuerdo.Titular),
            new SqlParameter("@pi_Parte1",editarSintesisAcuerdo.Parte1),
            new SqlParameter("@pi_Parte2",editarSintesisAcuerdo.Parte2),
            new SqlParameter("@pi_ActuarioId",editarSintesisAcuerdo.ActuarioId)
        };
        await _dbContext.ExecuteStoredProc<bool?>("[SISE3].[piInsertaActualizaSintesisAcuerdo]", parametros.ToArray());

        return true;
    }

    public async Task<(List<FiltroEstado>, List<FiltroContenido>)> ObtenenerFiltrosTablero(int catOrganismoId)
    {
        List<SqlParameter> listaParametros = new List<SqlParameter>();

        var pi_CatOrganismoId = new SqlParameter("@pi_catIdOrganismo", catOrganismoId);
        listaParametros.Add(pi_CatOrganismoId);
        var (estados, contenidos) = await _dbContext.ExecuteStoredProc<FiltroEstado, FiltroContenido>("[SISE3].[pcObtieneFiltrosActuaria]", listaParametros.ToArray());
        List<FiltroEstado> listaEstados = estados.Select(x => _mapper.Map<FiltroEstado>(x)).ToList();
        List<FiltroContenido> listaContenidos = contenidos.Select(x => _mapper.Map<FiltroContenido>(x)).ToList();
        return await Task.FromResult((listaEstados, listaContenidos));
    }

    public async Task<List<RecibirOficiosM>> ObtenerOficiosParaRecibir(int catOrganismoId, string folio, long empleadoId)
    {
        List<SqlParameter> listaParametros = new List<SqlParameter>();

        var pi_CatOrganismo = new SqlParameter("@pi_CatOrganismo", catOrganismoId);
        listaParametros.Add(pi_CatOrganismo);
        var pi_FolioOficio = new SqlParameter("@pi_FolioOficio", folio);
        listaParametros.Add(pi_FolioOficio);
        var pi_EmpleadoId = new SqlParameter("@pi_EmpleadoId", empleadoId);
        listaParametros.Add(pi_EmpleadoId);

        var oficios = await _dbContext.ExecuteStoredProc<RecibirOficiosM>("[SISE3].[pcListadoOficio]", listaParametros.ToArray());

        return oficios;
    }

    public async Task<bool> RecibirOficios(int catOrganismoId, long empleadoId, OficiosType oficio)
    {
        List<SqlParameter> listaParametros = new List<SqlParameter>();

        var pi_CatOrganismo = new SqlParameter("@pi_CatOrganismoId", catOrganismoId);
        listaParametros.Add(pi_CatOrganismo);
        var pi_EmpleadoId = new SqlParameter("@pi_IdEmpleado", empleadoId);
        listaParametros.Add(pi_EmpleadoId);

        List<OficiosType> oficios = new List<OficiosType> { oficio };
        SqlParameter pi_Oficios = new SqlParameter("@pi_Oficios", oficios.Select(x => x.toSqlDataRecord()));
        pi_Oficios.SqlDbType = SqlDbType.Structured;
        pi_Oficios.TypeName = "SISE3.Oficios_type";
        listaParametros.Add(pi_Oficios);

        await _dbContext.ExecuteStoredProcNonQuery("[SISE3].[piRegistraOficio]", listaParametros.ToArray());

        return true;
    }

    public async Task<(List<DetalleAcuerdo>, List<Promociones>)> ObtenerDetalleAcuerdo(int CatOrganismoId, long AsuntoNeunId, int SintesisOrden, int AsuntoDocumentoId)
    {
        List<SqlParameter> listaTableroSql = new List<SqlParameter>();

        var pi_CatOrganismoId = new SqlParameter("@pi_CatOrganismoId", CatOrganismoId);
        var pi_AsuntoNeunId = new SqlParameter("@pi_AsuntoNeunId", AsuntoNeunId);
        var pi_SintesisOrden = new SqlParameter("@pi_SintesisOrden", SintesisOrden);
        var pi_AsuntoDocumentoId = new SqlParameter("@pi_AsuntoDocumentoId", AsuntoDocumentoId);

        listaTableroSql.Add(pi_CatOrganismoId);
        listaTableroSql.Add(pi_AsuntoNeunId);
        listaTableroSql.Add(pi_SintesisOrden);
        listaTableroSql.Add(pi_AsuntoDocumentoId);

        var (resultado1, resultado2) = await _dbContext.ExecuteStoredProc<DetalleAcuerdo, Promociones>("[SISE3].[pcDetalleAcuerdo]", listaTableroSql.ToArray());

        return await Task.FromResult((resultado1, resultado2));
    }

    public async Task<bool> AgregarActuario(AgregarActuarioM actuario, long empleadoId)
    {
        List<SqlParameter> parametros = new List<SqlParameter>();

        var pi_AsuntoNeunId = new SqlParameter("@pi_AsuntoNeunId", actuario.AsuntoNeunId);
        parametros.Add(pi_AsuntoNeunId);
        var pi_AsuntoId = new SqlParameter("@pi_AsuntoId", actuario.AsuntoId);
        parametros.Add(pi_AsuntoId);
        var pi_ActuarioId = new SqlParameter("@pi_ActuarioId", actuario.ActuarioId);
        parametros.Add(pi_ActuarioId);
        var pi_Parte = new SqlParameter("@pi_Parte", actuario.Parte);
        parametros.Add(pi_Parte);
        var pi_Promovente = new SqlParameter("@pi_Promovente", actuario.Promovente);
        parametros.Add(pi_Promovente);
        var pi_TipoNotificacionId = new SqlParameter("@pi_TipoNotificacionId", actuario.TipoNotificacionId);
        parametros.Add(pi_TipoNotificacionId);
        var pi_TieneCOE = new SqlParameter("@pi_TieneCOE", actuario.TieneCOE);
        parametros.Add(pi_TieneCOE);
        var pi_NotElecID = new SqlParameter("@pi_NotElecID", actuario.NotElecId);
        parametros.Add(pi_NotElecID);
        var pi_EmpleadoId = new SqlParameter("@pi_EmpleadoID", empleadoId);
        parametros.Add(pi_EmpleadoId);

        string sqlQuery = "[SISE3].[piInsertaActuario]";
        await _dbContext.ExecuteStoredProcObj<string>(sqlQuery, parametros.ToArray());

        return true;
    }

    public async Task<(DatosAsunto datosAsunto, List<NotificacionDetalle> datos, NotificacionDetalleMetaDataEstados metadatos)> ObtenerNotificacionesDetalleConFiltro(ConsultaPaginadaDetalle consultaPaginada)
    {
        consultaPaginada.Texto = consultaPaginada?.Texto?.ToLower();
        var metadata = new NotificacionDetalleMetaDataEstados();
        var listaTablero = new List<NotificacionDetalle>();

        List<SqlParameter> listaTableroSql = new List<SqlParameter>();

        var pi_CatOrganismoId = new SqlParameter("@pi_CatOrganismoId", consultaPaginada.CatOrganismoId);
        var pi_TamanoPagina = new SqlParameter("@pi_TamanoPagina", consultaPaginada.TamanioPagina);
        var pi_NumeroPagina = new SqlParameter("@pi_NumeroPagina", consultaPaginada.NumeroPagina);
        var pi_AsuntoNeunId = new SqlParameter("@pi_AsuntoNeunId", consultaPaginada.AsuntoNeunId);
        var pi_AsuntoDocumentoID = new SqlParameter("@pi_AsuntoDocumentoID", consultaPaginada.AsuntoDocumentoID);
        var pi_Texto = new SqlParameter("@pi_Texto", consultaPaginada.Texto);
        var pi_OrdenarPor = new SqlParameter("@pi_OrdenarPor", consultaPaginada.OrdenarPor);
        var pi_TipoOrden = new SqlParameter("@pi_TipoOrden", consultaPaginada.TipoOrden);
        var pi_FiltroTipo = new SqlParameter("@pi_FiltroTipo", consultaPaginada.FiltroTipo);
        var pi_FiltroTipoParteID = new SqlParameter("@pi_FiltroTipoParteID", consultaPaginada.FiltroTipoParteID);
        var pi_FiltroTipoNotificacionID = new SqlParameter("@pi_FiltroTipoNotificacionID", consultaPaginada.FiltroTipoNotificacionID);
        var pi_FiltroActuarioID = new SqlParameter("@pi_FiltroActuarioID", consultaPaginada.FiltroActuarioID);



        listaTableroSql.Add(pi_CatOrganismoId);
        listaTableroSql.Add(pi_TamanoPagina);
        listaTableroSql.Add(pi_NumeroPagina);
        listaTableroSql.Add(pi_AsuntoNeunId);
        listaTableroSql.Add(pi_AsuntoDocumentoID);
        listaTableroSql.Add(pi_Texto);
        listaTableroSql.Add(pi_OrdenarPor);
        listaTableroSql.Add(pi_TipoOrden);
        listaTableroSql.Add(pi_FiltroTipo);
        listaTableroSql.Add(pi_FiltroTipoParteID);
        listaTableroSql.Add(pi_FiltroTipoNotificacionID);
        listaTableroSql.Add(pi_FiltroActuarioID);


        var (datosAsunto, metadatos, datos) =
            await _dbContext.ExecuteStoredProcThree<DatosAsunto, NotificacionDetalleMetaDataEstados, NotificacionDetalle>("SISE3.pcTableroDetalleNotificaciones", listaTableroSql.ToArray());

        listaTablero = datos.Select(x => _mapper.Map<NotificacionDetalle>(x)).ToList();

        metadata.VerTodo = metadatos.FirstOrDefault()?.VerTodo ?? 0;
        metadata.Pendiente = metadatos.FirstOrDefault()?.Pendiente ?? 0;
        metadata.EnProceso = metadatos.FirstOrDefault()?.EnProceso ?? 0;
        metadata.Notificados = metadatos.FirstOrDefault()?.Notificados ?? 0;


        return await Task.FromResult((datosAsunto.FirstOrDefault(), listaTablero, metadatos.FirstOrDefault()));
    }

    public async Task<(List<FiltroTipoParte>, List<FiltroTipoNotificacion>, List<FiltroActuario>)> ObternerFiltroDetalleNotificaciones(DetalleNotificacionesFiltrosConsulta request)
    {
        List<SqlParameter> listaTableroSql = new List<SqlParameter>();

        var pi_catIdOrganismo = new SqlParameter("@pi_catIdOrganismo", request.CatOrganismoId);
        listaTableroSql.Add(pi_catIdOrganismo);

        var (result1, result2, result3) =
            await _dbContext.ExecuteStoredProcThree<FiltroTipoParte, FiltroTipoNotificacion, FiltroActuario>("[SISE3].[pcObtieneFiltrosNotificacion]", listaTableroSql.ToArray());

        return await Task.FromResult((result1, result2, result3));
    }

    public async Task<(string, long)> InsertarAcuse(SubirAcuseM subirAcuse)
    {
        List<SqlParameter> parametros = new List<SqlParameter>();
        var nombreArchivo = string.Empty;
        long notElecId;

        var pi_AsuntoNeunId = new SqlParameter("@pi_AsuntoNeunId", subirAcuse.AsuntoNeunId);
        parametros.Add(pi_AsuntoNeunId);
        var pi_SintesisOrden = new SqlParameter("@pi_SintesisOrden", subirAcuse.SintesisOrden);
        parametros.Add(pi_SintesisOrden);
        var pi_CatOrganismoId = new SqlParameter("@pi_CatOrganismoId", subirAcuse.CatOrganismoId);
        parametros.Add(pi_CatOrganismoId);
        var pi_FechaNotificacion = new SqlParameter("@pi_FechaNotificacion", subirAcuse.FechaNotificacion);
        parametros.Add(pi_FechaNotificacion);
        var pi_TipoAcuse = new SqlParameter("@pi_TipoAcuse", subirAcuse.TipoAcuse);
        parametros.Add(pi_TipoAcuse);
        var pi_PersonaId = new SqlParameter("@pi_PersonaId", subirAcuse.PersonaId);
        parametros.Add(pi_PersonaId);
        var pi_EmpleadoId = new SqlParameter("@pi_EmpleadoId", subirAcuse.EmpleadoId);
        parametros.Add(pi_EmpleadoId);
        var pi_SintesisCitatorio = new SqlParameter("@pi_SintesisCitatorio", subirAcuse.SintesisCitatorio);
        parametros.Add(pi_SintesisCitatorio);
        var pi_TipoNotificacion = new SqlParameter("@pi_TipoNotificacion", subirAcuse.TipoNotificacion.HasValue ? subirAcuse.TipoNotificacion.Value : DBNull.Value);
        parametros.Add(pi_TipoNotificacion);
        var pi_FechaNotificacionCitatorio = new SqlParameter("@pi_FechaNotificacionCitatorio", subirAcuse.FechaNotificacionCitatorio.HasValue ? subirAcuse.FechaNotificacionCitatorio.Value : DBNull.Value);
        parametros.Add(pi_FechaNotificacionCitatorio);
        var po_NombreArchivo = new SqlParameter("@po_NombreArchivo", SqlDbType.VarChar, 200);
        po_NombreArchivo.Direction = ParameterDirection.Output;
        parametros.Add(po_NombreArchivo);

        var po_NotElecId = new SqlParameter("@po_NotElecId", SqlDbType.BigInt);
        po_NotElecId.Direction = ParameterDirection.Output;
        parametros.Add(po_NotElecId);


        string sqlQuery = "[SISE3].[piInsertaAcuse]";
        await _dbContext.ExecuteStoredProcObj<object?>(sqlQuery, parametros.ToArray());
        nombreArchivo = po_NombreArchivo.Value.ToString();
        notElecId = long.Parse(po_NotElecId.Value.ToString());
        return (nombreArchivo, notElecId);
    }

    public async Task<bool> AgregarActuarioMasivo(AgregarActuarioMasivoM actuario, long empleadoId)
    {
        List<SqlParameter> parametros = new List<SqlParameter>();

        var pi_AsuntoNeunId = new SqlParameter("@pi_AsuntoNeunId", actuario.AsuntoNeunId);
        parametros.Add(pi_AsuntoNeunId);
        var pi_SintesisOrden = new SqlParameter("@pi_SintesisOrden", actuario.SintesisOrden);
        parametros.Add(pi_SintesisOrden);
        var pi_ActuarioId = new SqlParameter("@pi_ActuarioId", actuario.ActuarioId);
        parametros.Add(pi_ActuarioId);
        var pi_EmpleadoID = new SqlParameter("@pi_EmpleadoID", empleadoId);
        parametros.Add(pi_EmpleadoID);

        SqlParameter pi_PartesNotificaciones = new SqlParameter("@pi_PartesNotificaciones", actuario.PartesNotificaciones?.Select(x => x.toSqlDataRecord()));
        pi_PartesNotificaciones.SqlDbType = SqlDbType.Structured;
        pi_PartesNotificaciones.TypeName = "SISE3.ParteNotificacion_type";
        parametros.Add(pi_PartesNotificaciones);

        string sqlQuery = "[SISE3].[piInsertaActuarioMasivo]";
        await _dbContext.ExecuteStoredProcObj<string>(sqlQuery, parametros.ToArray());
        return true;
    }

    public async Task<RutasNas> RutaArchivo(string modulo)
    {

        List<SqlParameter> parametros = new List<SqlParameter>();

        // Parámetros de entrada
        var pi_Modulo = new SqlParameter("@pi_Modulo", modulo);
        parametros.Add(pi_Modulo);

        var rutas = await _dbContext.ExecuteStoredProc<RutasNas>("[SISE3].[pcRutasChunkXModulo]", parametros.ToArray());

        var ruta = rutas.FirstOrDefault(s => s.Iescritura);

        if (ruta == null)
        {
            throw new Exception("No existe ruta configurada para guardar el documento");
        }

        return ruta;
    }

    public async Task<string> InsertarArchivoAcuse(SubirAcuseArchivoM subirAcuse)
    {
        List<SqlParameter> parametros = new List<SqlParameter>();

        var pi_NotElecId = new SqlParameter("@pi_NotElecId", subirAcuse.NotElecId);
        parametros.Add(pi_NotElecId);
        var pi_NombreArchivo = new SqlParameter("@pi_NombreArchivo", subirAcuse.NombreArchivo);
        parametros.Add(pi_NombreArchivo);
        var pi_ExtensionDocumento = new SqlParameter("@pi_ExtensionDocumento", subirAcuse.ExtensionDocumento);
        parametros.Add(pi_ExtensionDocumento);
        var pi_Usuario = new SqlParameter("@pi_Usuario", subirAcuse.Usuario);
        parametros.Add(pi_Usuario);

        var pi_Origen = new SqlParameter("@pi_Origen", subirAcuse.Origen);
        parametros.Add(pi_Origen);

        var pi_TipoAcuse = new SqlParameter("@pi_TipoAcuse", subirAcuse.TipoAcuse);
        parametros.Add(pi_TipoAcuse);

        var pi_IdRuta = new SqlParameter("@pi_IdRuta", subirAcuse.IdRuta);
        parametros.Add(pi_IdRuta);

        string sqlQuery = "[SISE3].[piInsertaArchivoAcuse]";

        return await _dbContext.ExecuteStoredProcObj<string>(sqlQuery, parametros.ToArray());
    }

    public async Task<bool> AgregarCOE(AgregarCOEDto coem, long e)
    {
        List<SqlParameter> parametros = new List<SqlParameter>();
        var pi_NotElecId = new SqlParameter("@pi_NotElecId", coem.NotElecId);
        parametros.Add(pi_NotElecId);
        var pi_Expediente = new SqlParameter("@pi_Expediente", coem.Expediente);
        parametros.Add(pi_Expediente);
        var pi_TipoComunicacion = new SqlParameter("@pi_TipoComunicacion", coem.TipoComunicacion);
        parametros.Add(pi_TipoComunicacion);
        var pi_NumeroOrigen = new SqlParameter("@pi_NumeroOrigen", coem.NumeroOrigen);
        parametros.Add(pi_NumeroOrigen);
        var pi_FechaEnvio = new SqlParameter("@pi_FechaEnvio", coem.FechaEnvio);
        parametros.Add(pi_FechaEnvio);
        var pi_Secretario = new SqlParameter("@pi_Secretario", coem.Secretario);
        parametros.Add(pi_Secretario);
        var pi_Mesa = new SqlParameter("@pi_Mesa", coem.Mesa);
        parametros.Add(pi_Mesa);
        var pi_TipoAsunto = new SqlParameter("@pi_TipoAsunto", coem.TipoAsunto);
        parametros.Add(pi_TipoAsunto);
        var pi_NumeroExpedienteOrigen = new SqlParameter("@pi_NumeroExpedienteOrigen", coem.NumeroExpedienteOrigen);
        parametros.Add(pi_NumeroExpedienteOrigen);
        var pi_Destinatario = new SqlParameter("@pi_Destinatario", coem.Destinatario);
        parametros.Add(pi_Destinatario);
        var pi_Objetivo = new SqlParameter("@pi_Objetivo", coem.Objetivo);
        parametros.Add(pi_Objetivo);
        var pi_OficinaCorrespondenciaComun = new SqlParameter("@pi_OficinaCorrespondenciaComun", coem.OficinaCorrespondenciaComun);
        parametros.Add(pi_OficinaCorrespondenciaComun);
        var pi_EmpleadoId = new SqlParameter("@pi_EmpleadoId", e);
        parametros.Add(pi_EmpleadoId);

        string sqlQuery = "[SISE3].[piInsertaCOE]";
        try
        {

            await _dbContext.ExecuteStoredProcObj<string>(sqlQuery, parametros.ToArray());
            return true;
        }
        catch (Exception error)
        {
            throw new Exception(error.Message);
        }
    }

    public async Task<ObtenerCOE> ConsultaCOE(long NotificacionElectronicaId)
    {
        List<SqlParameter> parametros = new List<SqlParameter>();

        var pi_NotElecId = new SqlParameter("@pi_NotElecId", NotificacionElectronicaId);
        parametros.Add(pi_NotElecId);

        var consultaCOE = await _dbContext.ExecuteStoredProc<ObtenerCOE>("[SISE3].[pcConsultaCOE]", parametros.ToArray());

        if (consultaCOE == null || !consultaCOE.Any())
        {
            throw new Exception("No existe informacion para la COE consultada");
        }

        return consultaCOE.First();
    }

    public async Task<List<ArchivosAnexos>> ObtenerArchivosyAnexos(long asuntoNeunId, int numeroOrden, int yearPromocion, int catIdOrganismo, int origen, int modulo, int asuntoDocumentoId, int sintesisOrden)
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
                new SqlParameter("@pi_SintesisOrden", sintesisOrden)


            };

        string sqlQuery = "SISE3.pcConsultaArchivosyRutaXModulo";

        var archivos = await _dbContext.ExecuteStoredProc<ArchivosAnexos?>(sqlQuery, parametros);

        return archivos;
    }

    public async Task<List<ObtenerAcuerdoM>> ObtenerAcuerdos(int CatOrganismoId, DateTime fInicial, DateTime fFinal)
    {
        var parametros = new[]
        {
            new SqlParameter("pi_CatOrganismoId", CatOrganismoId),
            new SqlParameter("pi_FechaInicio", fInicial),
            new SqlParameter("pi_FechaFin", fFinal)
        };
        var listaObtenida = await _dbContext.ExecuteStoredProc<ObtenerAcuerdoM>("[SISE3].[spObtenerListaAcuerdos]", parametros);

        return listaObtenida;
    }

    public async Task<bool> AgregarSintesisManual(AgregarSintesisManualDto sintesis)
    {
        var parametros = new[]
        {
            new SqlParameter("pi_AsuntoNeunId", sintesis.AsuntoNeunId),
            new SqlParameter("pi_CatOrganismoId", sintesis.CatOrganismoId),
            new SqlParameter("pi_FechaAuto", sintesis.FechaAuto),
            new SqlParameter("pi_Sintesis", sintesis.Sintesis),
            new SqlParameter("pi_FechaPublicacion", sintesis.FechaPublicacion),
            new SqlParameter("pi_Titular", sintesis.Titular),
            new SqlParameter("pi_Actuario", sintesis.Actuario),
            new SqlParameter("pi_ClasificacionCuaderno", sintesis.ClasificacionCuaderno),
            new SqlParameter("pi_TipoCuaderno", sintesis.TipoCuaderno),
            new SqlParameter("pi_UsuarioCaptura", sintesis.UsuarioCaptura),
            new SqlParameter("pi_Parte1", sintesis.Parte1),
            new SqlParameter("pi_Parte2", sintesis.Parte2),

        };
        try
        {
            await _dbContext.ExecuteStoredProc<AgregarSintesisManualDto>("[SISE3].[piAgregarSintesisManual]", parametros);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }

    }


    public async Task<List<DetalleSintesisDTO>> ObtenerDetalleSintesis(FiltroDetalleSintesis sintesis)
    {
        var parametros = new[]
        {
            new SqlParameter("@pi_AsuntoNeunId", sintesis.AsuntoNeunId),
            new SqlParameter("@pi_SintesisOrden", sintesis.SintesisOrden)
        };

        return await _dbContext.ExecuteStoredProc<DetalleSintesisDTO>("SISE3.pcSintesisDetalle", parametros);

    }

    public async Task<List<ObtenerSintesisManualDTO>> ObtenerSintesisManual(DateTime fechaPublicacion, int CatOrganismoId)
    {
        var parametros = new[]
       {
            new SqlParameter("pi_CatOrganismoId", CatOrganismoId),
            new SqlParameter("pi_FechaPublicacion", fechaPublicacion)
        };

        var results = await _dbContext.ExecuteStoredProc<ObtenerSintesisManualDTO>("SISE3.pcConsultaSintesisManual", parametros);

        return results;

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

    public async Task<ResponseDatosGenerarFolioM> GenerarFoliosPartes(AgregarFoliosPartes info, long empleadoId)
    {
        List<SqlParameter> parametros = new List<SqlParameter>();

        var pi_AsuntoNeunId = new SqlParameter("@pi_AsuntoNeunId", info.AsuntoNeunId);
        parametros.Add(pi_AsuntoNeunId);
        var pi_AsuntoDocumentoId = new SqlParameter("@pi_AsuntoDocumentoId", info.AsuntoDocumentoId);
        parametros.Add(pi_AsuntoDocumentoId);
        var pi_EmpleadoID = new SqlParameter("@pi_EmpleadoID", empleadoId);
        parametros.Add(pi_EmpleadoID);

        SqlParameter pi_PartesNotificaciones = new SqlParameter("@pi_PartePromoventeNotificacion", info.PartesNotificaciones?.Select(x => x.toSqlDataRecord()));
        pi_PartesNotificaciones.SqlDbType = SqlDbType.Structured;
        pi_PartesNotificaciones.TypeName = "SISE3.PersonaPromoventeNotificacion_type";
        parametros.Add(pi_PartesNotificaciones);

        string sqlQuery = "[SISE3].[piInsertarOficio]";

        var (folios, datos) =  await _dbContext.ExecuteStoredProcTwo<FolioM, DatosGenerarFolioM>(sqlQuery, parametros.ToArray());

        return new ResponseDatosGenerarFolioM() { Datos = datos, Folios = folios };
    }

    public async Task<PersonasAsunto > ObtenerPersonaAsuntoXidEmpleado(long asuntoNeunId, long parte) {

        List<SqlParameter> parameters = new List<SqlParameter>() {
            new SqlParameter("@pi_AsuntoNeunId", asuntoNeunId),
            new SqlParameter("@pi_EmpleadoId", parte)
            };
        var result = await _dbContext.ExecuteStoredProc<PersonasAsunto>("[SISE3].[ObtenerPersonaAsuntoXidEmpleado]", parameters.ToArray());
        return result.Count() >= 1 ? result.First() : null; ;
    }


}