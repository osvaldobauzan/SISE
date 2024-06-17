using System.Data;
using AutoMapper.Configuration;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Persistence;
using CJF.Sgcpj.Judicatura.Promovente.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Promovente.Application.Common.Repositories;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Promovente.Infrastructure.Persistence.Repositories;
public class PromoventesRepository : IPromoventesRepository
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _dbContext;
    public PromoventesRepository(
        IConfiguration configuration,
        ApplicationDbContext dbContext)
    {

        _configuration = configuration;
        _dbContext = dbContext;
    }
    public async Task<long> AgregarAutoridadJudicial(AgregarAutoridadJudicial autoridadJudicial)
    {
        var po_AutoridadJudicialId = new SqlParameter("@po_AutoridadJudicialId", autoridadJudicial.AutoridadJudicialId);
        po_AutoridadJudicialId.Direction = ParameterDirection.Output;
        SqlParameter[] parametros = {
            new SqlParameter("@pi_AsuntoNeunId", autoridadJudicial.AsuntoNeunId),
            new SqlParameter("@pi_catIdOrganismo",autoridadJudicial.catIdOrganismo),
            new SqlParameter("@pi_EmpleadoId", autoridadJudicial.EmpleadoId),
            new SqlParameter("@pi_RegistroEmpleadoId", autoridadJudicial.RegistroEmpleadoId),
            new SqlParameter("@pi_NumeroOrden", autoridadJudicial.NumeroOrden),

        po_AutoridadJudicialId
        };

        string sqlQuery = "[SISE3].[piAutoridadJudicial]";

        var rowsAffected = await _dbContext.ExecuteStoredProcObj<object?>(sqlQuery, parametros);

        return Convert.ToInt32(po_AutoridadJudicialId.Value);
    }

    public async Task<long> AgregarPersona(AgregarPersonaAsunto personaAsunto)
    {

        var parametroPersonaId = new SqlParameter("@po_PersonaId", personaAsunto.PersonaId);
        parametroPersonaId.Direction = ParameterDirection.Output;

        SqlParameter[] parametros = {

            new SqlParameter("@pi_AsuntoNeunId", personaAsunto.AsuntoNeunId),
            new SqlParameter("@pi_UsuarioCaptura", 1/*personaAsunto.UsuarioCaptura*/),
            new SqlParameter("@pi_Nombre", personaAsunto.Nombre),

            new SqlParameter("@pi_APaterno", personaAsunto.APaterno),
            new SqlParameter("@pi_AMaterno", personaAsunto.AMaterno),
            new SqlParameter("@pi_CatTipoPersonaId", personaAsunto.CatTipoPersonaId),
            new SqlParameter("@pi_CatCaracterPersonaAsuntoId", personaAsunto.CatCaracterPersonaAsuntoId),
            new SqlParameter("@pi_DenominacionDeAutoridad", personaAsunto.DenominacionDeAutoridad),
            new SqlParameter("@pi_NumeroOrden", personaAsunto.NumeroOrden),

            parametroPersonaId

        };
        string sqlQuery = "[SISE3].[piPersonaAsunto]";
        var rowsAffected = await _dbContext.ExecuteStoredProcObj<object?>(sqlQuery, parametros);
        return Convert.ToInt32(parametroPersonaId.Value);

    }

    public async Task<long> AgregarPromovente(AgregarPromovente promovente)
    {
        var parametroPromoventeId = new SqlParameter("@po_PersonaId", promovente.PersonaId);
        parametroPromoventeId.Direction = ParameterDirection.Output;
        SqlParameter[] parametros = {
            new SqlParameter("@pi_AsuntoNeunId", promovente.AsuntoNeunId),
            new SqlParameter("@pi_Tipo", promovente.Tipo),
            new SqlParameter("@pi_Nombre", promovente.Nombre),
            new SqlParameter("@pi_APaterno", promovente.APaterno),
            new SqlParameter("@pi_AMaterno", promovente.AMaterno),
            new SqlParameter("@pi_PersonaId", promovente.PersonaId),
            new SqlParameter("@pi_RegistroEmpleadoId", promovente.RegistroEmpleadoId /*promovente.RegistroEmpleadoId*/),
            new SqlParameter("@pi_numeroOrden", promovente.NumeroOrden),

        };
        string sqlQuery = "[SISE3].[piPromovente]";
        var rowsAffected = await _dbContext.ExecuteStoredProcObj<object?>(sqlQuery, parametros);
        return Convert.ToInt32(parametroPromoventeId.Value);

    }
}
