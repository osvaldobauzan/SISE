using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Persistence;
using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.Common.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PolyCache.Cache;
using ExpedienteElectronico.Application.Common.Models;
using System.Data;
using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.Sentencia;
using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.Audiencia;
using ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.InformacionParte;
using ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.DatosGenerales;

namespace CJF.Sgcpj.Judicatura.ExpedienteElectronico.Infrastructure.Persistence.Repositories;
public class ExpedienteElectronicoRepository : IExpedienteElectronicoRepository
{
    private readonly IStaticCacheManager _staticCacheManager;
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    public ExpedienteElectronicoRepository(IStaticCacheManager staticCacheManager, IConfiguration configuration, ApplicationDbContext dbContext, IMapper mapper)
    {
        _staticCacheManager = staticCacheManager;
        _configuration = configuration;
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<List<DetalleExpedienteElectronico>> ObtenerExpedienteElectronico(ExpedienteElectronicoFiltro param)
    {
        var parametros = new[]
        {
            new SqlParameter("@pi_AsuntoNeunId", (param.AsuntoNeunId != 0)?param.AsuntoNeunId:DBNull.Value),
        };

        string sqlQuery = "[dbo].[uspx_ee_getExpedienteElectronicoPortal]";
        var expElectronico = await this._dbContext.ExecuteStoredProc<DetalleExpedienteElectronico>(sqlQuery, parametros);
        return expElectronico;

    }
    public async Task<List<FichaTecnicaExpedienteElectronico>> ObtenerFichaTecnicaExpediente(FichaTecnicaExpedienteElectronicoFiltro param)
    {
        var parametros = new[]
        {
            new SqlParameter("@pi_AsuntoNeunId", (param.AsuntoNeunId != 0)?param.AsuntoNeunId:DBNull.Value),
        };

        string sqlQuery = "[dbo].[uspx_ee_getFichaTecnica]";
        var ficha = await this._dbContext.ExecuteStoredProc<FichaTecnicaExpedienteElectronico>(sqlQuery, parametros);
        return ficha;

    }
    public async Task<Int64> InsertarPersonaAsunto(PersonaAsuntoInsert param)
    {
        var po_PersonaId = new SqlParameter("@po_PersonaId", param.PersonaId);
        po_PersonaId.Direction = ParameterDirection.Output;
        SqlParameter[] parametros = {
            new SqlParameter("@pi_AsuntoNeunId", param.AsuntoNeunId),
            new SqlParameter("@pi_UsuarioCaptura", param.UsuarioCaptura),
            new SqlParameter("@pi_PersonaAsunto", param.PersonaAsuntoJson),
            new SqlParameter("@pi_IdOrganoPlenos", param.IdOrganoPlenos),
            po_PersonaId
        };

        string sqlQuery = "SISE3.piPersonaAsuntoExpedienteElectronico";

        var rowsAffected = await _dbContext.ExecuteStoredProcObj<object?>(sqlQuery, parametros);

        return Convert.ToInt64(po_PersonaId.Value);
    }
    public async Task<bool> EliminarPersonaAsunto(PersonaAsuntoDelete param)
    {
        SqlParameter[] parametros = {
            new SqlParameter("@pi_PersonaId", param.PersonaId),
            new SqlParameter("@pi_UsuarioElimna", param.UsuarioElimina),
        };

        string sqlQuery = "SISE3.pePersonaAsuntoExpedienteElectronico";

        var rowsAffected = await _dbContext.ExecuteStoredProcObj<object?>(sqlQuery, parametros);

        return true;

    }
    public async Task<bool> ActualizarPersonaAsunto(PersonaAsuntoUpdate param)
    {
        SqlParameter[] parametros = {
            new SqlParameter("@pi_UsuarioCaptura", param.UsuarioCaptura),
            new SqlParameter("@pi_PersonaAsunto", param.PersonaAsuntoJson),
            new SqlParameter("@pi_PersonaId", param.PersonaId),
            new SqlParameter("@pi_AsuntoNeunId", param.AsuntoNeunId),
            new SqlParameter("@pi_IdOrganoPlenos", param.IdOrganoPlenos),
        };

        string sqlQuery = "SISE3.paPersonaAsuntoExpedienteElectronico";

        var rowsAffected = await _dbContext.ExecuteStoredProcObj<object?>(sqlQuery, parametros);

        return true;
    }

    public async Task<PersonaAsuntoDTO> ObtenerPersonaAsunto(PersonaAsuntoFiltro param)
    {
        SqlParameter[] parametros = {
            new SqlParameter("@pi_PersonaId", param.PersonaId),
        };
        string sqlQuery = "SISE3.pcPersonaAsuntoExpedienteElectronico";
        var persona = await this._dbContext.ExecuteStoredProc<PersonaAsuntoDTO>(sqlQuery, parametros);        
        return persona.FirstOrDefault();
    }

    public async Task<EstadoSentencia> ObtenerEstadoSentencia(EstadoSentenciaConsulta request)
    {
        var estadoSentencica = new EstadoSentencia();
        var parametros = new List<SqlParameter>()
        {
            new SqlParameter("@pi_AsuntoNeunId", request.AsuntoNeunId)
        };

        string sqlQuery = "[SISE3].[pcEstadoSentencia]";
        var resultado = await _dbContext.ExecuteStoredProc<EstadoSentencia>(sqlQuery, parametros.ToArray());

        estadoSentencica.FechaSentencia = resultado.FirstOrDefault()?.FechaSentencia ?? string.Empty;
        estadoSentencica.Estado = resultado.FirstOrDefault()?.Estado ?? string.Empty;
        estadoSentencica.Ejecucion = resultado.FirstOrDefault()?.Ejecucion ?? string.Empty;
        return estadoSentencica;
    }

    public async Task<DetalleAudiencia> ObtenerDetalleAudiencia(AudienciaConsulta request)
    {
        var audiencia = new DetalleAudiencia();
        var parametros = new List<SqlParameter>()
        {
            new SqlParameter("@pi_AsuntoNeunId", request.AsuntoNeunId),
            new SqlParameter("@pi_CuadernoId", request.CuadernoId)
        };

        string sqlQuery = "[SISE3].[pcAudiencia]";
        var resultado = await _dbContext.ExecuteStoredProc<DetalleAudiencia>(sqlQuery, parametros.ToArray());
        audiencia.Fecha = resultado.FirstOrDefault()?.Fecha ?? string.Empty;
        audiencia.Hora = resultado.FirstOrDefault()?.Hora ?? string.Empty;
        audiencia.Resultado = resultado.FirstOrDefault()?.Resultado ?? string.Empty;
        audiencia.TipoAudiencia = resultado.FirstOrDefault()?.TipoAudiencia ?? string.Empty;
        return audiencia;
    }

    public async Task<List<InformacionParteM>> ObtenerInformacionParte(InformacionParteConsulta request)
    {
        var parametros = new List<SqlParameter>()
        {
            new SqlParameter("@AsuntoNeunId", request.AsuntoNeunId),
            new SqlParameter("@PersonaId", request.PersonaId)
        };

        string sqlQuery = "[SISE3].[pcVerCaptura]";
        var resultado = await _dbContext.ExecuteStoredProc<InformacionParteM>(sqlQuery, parametros.ToArray());

        return resultado;
    }

    public async Task<DatosGeneralesM?> ObtenerDatosGenerales(DatosGeneralesConsulta request)
    {
        var parametros = new List<SqlParameter>()
        {
            new SqlParameter("@pi_AsuntoNeunId", request.AsuntoNeunId)
        };

        string sqlQuery = "[SISE3].[pcDatosGenerales]";
        var resultado = await _dbContext.ExecuteStoredProc<DatosGeneralesM>(sqlQuery, parametros.ToArray());

        return resultado.FirstOrDefault();
    }
}

