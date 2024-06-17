using AutoMapper;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models.Actuario;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Exceptions;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PolyCache.Cache;

namespace CJF.Sgcpj.Judicatura.Actuaria.Infrastructure.Persistence.Repositories;
public class ActuarioRepository : IActuarioRepository
{
    private readonly IConfiguration _configuration;
    private readonly IStaticCacheManager _staticCacheManager;
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public ActuarioRepository(IConfiguration configuration, IStaticCacheManager staticCacheManager, ApplicationDbContext dbContext, IMapper mapper)
    {
        _configuration = configuration;
        _staticCacheManager = staticCacheManager;
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<(List<ActuarioNotificaciones> datos, ActuarioNotificacionesMetaDataEstados metadatos)> ObtenerActuarioNotificacionesConFiltro(ConsultaPaginadaNotificaciones consultaPaginada)
    {
        consultaPaginada.Texto = consultaPaginada?.Texto?.ToLower();
        var metadata = new ActuarioNotificacionesMetaDataEstados();
        var listaTablero = new List<ActuarioNotificaciones>();

        List<SqlParameter> listaTableroSql = new List<SqlParameter>();

        var pi_CatOrganismoId = new SqlParameter("@pi_CatOrganismoId", consultaPaginada.CatOrganismoId);
        var pi_FechaInicial = new SqlParameter("@pi_FechaInicial", consultaPaginada.FechaInicial);
        var pi_FechaFinal = new SqlParameter("@pi_FechaFinal", consultaPaginada.FechaFinal);
        var pi_TamanoPagina = new SqlParameter("@pi_TamanoPagina", consultaPaginada.TamanioPagina);
        var pi_NumeroPagina = new SqlParameter("@pi_NumeroPagina", consultaPaginada.NumeroPagina);
        var pi_Texto = new SqlParameter("@pi_Texto", consultaPaginada.Texto);
        var pi_OrdenarPor = new SqlParameter("@pi_OrdenarPor", consultaPaginada.OrdenarPor);
        var pi_TipoOrden = new SqlParameter("@pi_TipoOrden", consultaPaginada.TipoOrden);
        var pi_FiltroTipo = new SqlParameter("@pi_FiltroTipo", consultaPaginada.FiltroTipo);
        var pi_FiltroTipoParteID = new SqlParameter("@pi_FiltroTipoParteID", consultaPaginada.FiltroTipoParteID);
        var pi_FiltroTipoNotificacionID = new SqlParameter("@pi_FiltroTipoNotificacionID", consultaPaginada.FiltroTipoNotificacionID);
        var pi_FiltroActuarioID = new SqlParameter("@pi_FiltroActuarioID", consultaPaginada.FiltroActuarioID);



        listaTableroSql.Add(pi_CatOrganismoId);
        listaTableroSql.Add(pi_FiltroActuarioID);
        listaTableroSql.Add(pi_FechaInicial);
        listaTableroSql.Add(pi_FechaFinal);
        listaTableroSql.Add(pi_TamanoPagina);
        listaTableroSql.Add(pi_NumeroPagina);
        listaTableroSql.Add(pi_Texto);
        listaTableroSql.Add(pi_OrdenarPor);
        listaTableroSql.Add(pi_TipoOrden);
        listaTableroSql.Add(pi_FiltroTipo);
        listaTableroSql.Add(pi_FiltroTipoParteID);
        listaTableroSql.Add(pi_FiltroTipoNotificacionID);



        var (metadatos, datos) =
            await _dbContext.ExecuteStoredProcTwo<ActuarioNotificacionesMetaDataEstados, ActuarioNotificaciones>("SISE3.pcTableroActuarioNotificaciones", listaTableroSql.ToArray());

        listaTablero = datos.Select(x => _mapper.Map<ActuarioNotificaciones>(x)).ToList();

        metadata.VerTodo = metadatos.FirstOrDefault()?.VerTodo ?? 0;
        metadata.Pendiente = metadatos.FirstOrDefault()?.Pendiente ?? 0;
        metadata.EnProceso = metadatos.FirstOrDefault()?.EnProceso ?? 0;
        metadata.Notificados = metadatos.FirstOrDefault()?.Notificados ?? 0;


        return await Task.FromResult((listaTablero, metadatos.FirstOrDefault()));
    }

    public async Task<List<ConsultaOficioActuario>> ListaConsultaOficioPorActuario(long asuntoNeunId, int asuntoDocumentoId, long actuarioId, int catOrganismoId)
    {
        List<SqlParameter> parametros = new List<SqlParameter>();

        var pi_AsuntoNeunId = new SqlParameter("@pi_AsuntoNeunId", asuntoNeunId);
        var pi_AsuntoDocumentoId = new SqlParameter("@pi_AsuntoDocumentoId", asuntoDocumentoId);
        var pi_ActuarioId = new SqlParameter("@pi_ActuarioId", actuarioId);
        var pi_CatOrganismoId = new SqlParameter("@pi_CatOrganismoId", catOrganismoId);

        parametros.Add(pi_AsuntoNeunId);
        parametros.Add(pi_AsuntoDocumentoId);
        parametros.Add(pi_ActuarioId);
        parametros.Add(pi_CatOrganismoId);

        var response = await _dbContext.ExecuteStoredProc<ConsultaOficioActuario>("[SISE3].[pcConsultaOficios]", parametros.ToArray());

        if (response == null || !response.Any())
        {
            throw new Exception("Sin informacion para la consulta de oficio por actuario");
        }

        return response;
    }

    public async Task<List<ConsultaOficioActuario>> ListaConsultaOficioPorActuarioPorFecha(long actuarioId, string fechaInicio, string fechaFin, int catOrganismoId)
    {
        List<SqlParameter> parametros = new List<SqlParameter>();

        var pi_ActuarioId = new SqlParameter("@pi_ActuarioId", actuarioId);
        var pi_FechaInicio = new SqlParameter("@pi_FechaInicio", fechaInicio);
        var pi_FechaFin = new SqlParameter("@pi_FechaFin", fechaFin);
        var pi_CatOrganismoId = new SqlParameter("@pi_CatOrganismoId", catOrganismoId);


        parametros.Add(pi_ActuarioId);
        parametros.Add(pi_FechaInicio);
        parametros.Add(pi_FechaFin);
        parametros.Add(pi_CatOrganismoId);

        var response = await _dbContext.ExecuteStoredProc<ConsultaOficioActuario>("[SISE3].[pcConsultaOficiosPorFecha]", parametros.ToArray());

        if (response == null || !response.Any())
        {
            throw new ValidationException(new List<FluentValidation.Results.ValidationFailure> { new FluentValidation.Results.ValidationFailure
            { ErrorMessage = "Sin informacion para la consulta de oficio de actuario por fecha " + fechaInicio + " a " + fechaFin} });
        }


        return response;
    }
}
