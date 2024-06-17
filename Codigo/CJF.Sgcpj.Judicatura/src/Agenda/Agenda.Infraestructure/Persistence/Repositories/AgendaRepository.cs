using AutoMapper;
using CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Comandos.InsertarAgenda;
using CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Comandos.ModificarEstado;
using CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Consulta.ObtenerAgendaFecha;
using CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Consulta.ObtenerDetalleCaracter;
using CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Consulta.ObtenerParametros;
using CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Consulta.ValidarAudiencia;
using CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Consulta.ValidarNeun;
using CJF.Sgcpj.Judicatura.Agenda.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Agenda.Infrastructure.Persistence.Repositories;
public class AgendaRepository : IAgendaRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private object _applicationDbContext;
    private readonly ISesionService _sesionService;

    public AgendaRepository(ApplicationDbContext dbContext, IMapper mapper, ISesionService sesionService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _sesionService = sesionService;
    }


    public async Task<ModificarEstadoDto> ModificarEstadoAudiencia(ModificarEstadoRequest requestAudiencia)
    {
        ModificarEstadoDto resultEstado = new ModificarEstadoDto();
        var lstUpdAudienciaSql = new List<SqlParameter>()
        {
            new SqlParameter("@pi_IdAgenda", requestAudiencia.IdAgenda),
            new SqlParameter("@pi_IdResultado", requestAudiencia.IdResultado)
        };
        var resultUpd = await _dbContext.ExecuteStoredProc<ModificarEstadoDto>("[SISE3].[paActualizarResultadoAudiencia]", lstUpdAudienciaSql.ToArray());
        resultEstado = resultUpd.FirstOrDefault();

        return resultEstado;
    }

    public async Task<bool> InsertarAudienciaAgenda(InsertarAgendaRequest requestAgenda)
    {
        AsuntosDetalleFechaDto _AsuntosDetalleFecha = new AsuntosDetalleFechaDto();
        AsuntoPersonasTypeDto _AsuntoPersonas = new AsuntoPersonasTypeDto();

        var lstAgendaFechaSql = new List<SqlParameter>()
        {
            new SqlParameter("@pi_AsuntoNeunId",requestAgenda.NumeroNeun),
            new SqlParameter("@pi_AudienciaId",requestAgenda.IdTipoAudiencia),
            new SqlParameter("@pi_AsuntoId",requestAgenda.IdTipoAsunto),
            new SqlParameter("@pi_AsuntoPersonas_type",_AsuntoPersonas),
            new SqlParameter("@pi_AsuntosDetalleFechas",_AsuntosDetalleFecha),
            new SqlParameter("@pi_Empleado",_sesionService.SesionActual.EmpleadoId),
            new SqlParameter("@pi_NoCaptura",requestAgenda.NumeroCaptura),
            new SqlParameter("@pi_EsAudienciaOraltis",requestAgenda.AudienciaOraltis),
            new SqlParameter("@pi_IdAudienciaOraltis",requestAgenda.IdAudienciaOraltis),
            new SqlParameter("@pi_ParteSel",requestAgenda.PartSel),
            new SqlParameter("@pi_SecretarioId",requestAgenda.SecretarioId),
            new SqlParameter("@pi_NombreSolicitante",requestAgenda.NombreSolicitante),
            new SqlParameter("@pi_MotivoConsulta",requestAgenda.MotivoConsulta),
            new SqlParameter("@pi_FechaSolicitudAudiencia",requestAgenda.FechaAudiencia),
            new SqlParameter("@pi_FechaAcuerdoSolicitud",requestAgenda.FechaAcuerdoSolicitud)
        };
        await _dbContext.ExecuteStoredProcObj<string>("[SISE3].[piInsertarDetallesFechasAgenda]", lstAgendaFechaSql.ToArray());

        return true;
    }

    public async Task<List<ObtenerAgendaFechaDto>> ObtenerAgendaFecha(ObtenerAgendaFechaRequest request)
    {

        var lstAgendaFechaSql = new List<SqlParameter>()
         {
            new SqlParameter("@pi_CatOrganismoId", request.CatIdOrganismo),
            new SqlParameter("@pi_FAudIni",request.FechaIni),
            new SqlParameter("@pi_FAudFin", request.FechaFin),
            new SqlParameter("@pi_Expediente",request.Expediente),
            new SqlParameter("@pi_Persona",request.Persona)
        };        
        var result = await _dbContext.ExecuteStoredProc<ObtenerAgendaFechaDto>("[SISE3].[pcAgendaDaAudienciasPorDia]", lstAgendaFechaSql.ToArray());
        return result;
    }

    public async Task<List<ObtenerDetalleCaracterDto>> ObtenerDetalleCaracter(ObtenerDetalleCaracterRequest request)
    {
        var lstCaracterSql = new List<SqlParameter>()
         {
            new SqlParameter("@pi_IdAsunto", request.IdTipoAsunto),
            new SqlParameter("@pi_IdNeun", request.IdNeun)
        };

        return await _dbContext.ExecuteStoredProc<ObtenerDetalleCaracterDto>("[SISE3].[pcPersonaCaracterXAsunto]", lstCaracterSql.ToArray());

    }

    public async Task<int> ValidarExisteNeun(ValidarNeunRequest request)
    {
        var paramsNeunSql = new List<SqlParameter>()
        {
            new SqlParameter("@pi_CatOrganismoId", _sesionService.SesionActual.CatOrganismoId),
            new SqlParameter("@pi_Neun", request.IdNeun)
        };

        var resultNeun = await _dbContext.ExecuteStoredProcObj<int>("[SISE3].[pcAgendaVerificaNeun]", paramsNeunSql.ToArray());
        return resultNeun;
    }

    public async Task<List<ValidarDisponibilidadDto>> ValidarDisponibilidadAudienciaFecha(ValidarDisponibilidadRequest request)
    {
        var lstAudienciaFecha = new List<ValidarDisponibilidadDto>();

        var lstAudienciaFechaSql = new List<SqlParameter>()
        {
            new SqlParameter("@pi_CatOrganismoId", _sesionService.SesionActual.CatOrganismoId),
            new SqlParameter("@pi_FechaAud", request.FechaAudiencia),
            new SqlParameter("@pi_HoraAud", request.HoraAudiencia)
        };

        lstAudienciaFecha = await _dbContext.ExecuteStoredProc<ValidarDisponibilidadDto>("[SISE3].[pcAgendaObtineEmpalmes]", lstAudienciaFechaSql.ToArray());
        return lstAudienciaFecha;
    }

    public async Task<List<ObtieneResultadoDto>> ObtieneAudienciaResultado(ObtieneResultadoRequest request)
    {
        List<ObtieneResultadoDto> lstAudienciaResultado = new List<ObtieneResultadoDto>();

        var lstAudienciaResultadoSql = new List<SqlParameter>()
        {
            new SqlParameter("@pi_Neun", request.Neun),
            new SqlParameter("@pi_IdTipoAudiencia", request.IdTipoAudiencia),
            new SqlParameter("@pi_UsaPartes", request.UsaPartes)
        };
        lstAudienciaResultado = await _dbContext.ExecuteStoredProc<ObtieneResultadoDto>("[SISE3].[pcAgendaObtieneAudienciaSinResultado]", lstAudienciaResultadoSql.ToArray());

        return lstAudienciaResultado;
    }



}
