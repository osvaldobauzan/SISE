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
/// ///////////////////     INTERFASE PARA LA CAPA DE EXPEDIENTE UNICAMENTE EN SEGUIMIENTO
/// </summary>
public class ExpedienteRepository : IExpedienteRepository
{

    private readonly IStaticCacheManager _staticCacheManager;
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public ExpedienteRepository(
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

    public async Task<Seguimientos.ExpedienteObtener> getExpediente(Seguimientos.ExpedienteObtener expediente)
    {
        try
        {
            

            SqlParameter[] parametros = {
        new SqlParameter("@pi_asuntoNeun", expediente.Neun)
        };
            string sqlQuery = "[dbo].[uspx_getExpediente]";
            var result = await this._dbContext.ExecuteStoredProc<Seguimientos.ExpedienteObtener>(sqlQuery, parametros);

            return result.First();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public async Task<List<Seguimientos.ExpedienteObtener>> getExpedientes(Seguimientos.ExpedienteObtener expediente)
    {

        int CatOrganismoId = string.IsNullOrEmpty(expediente.CatOrganismoId.ToString()) ? 0 : Convert.ToInt32(expediente.CatOrganismoId);
        int CatTipoAsuntoId = string.IsNullOrEmpty(expediente.CatTipoAsuntoId.ToString()) ? 0 : Convert.ToInt32(expediente.CatTipoAsuntoId);

        SqlParameter[] parametros = {
                new SqlParameter("@pi_AsuntoAlias",expediente.AsuntoAlias),
            new SqlParameter("@pi_CatOrganismoId",CatOrganismoId),
            new SqlParameter("@pio_CatTipoAsuntoId",CatTipoAsuntoId)
        };
        string sqlQuery = "[dbo].[uspx_getExpedientePorAsuntoAlias]";

        List<Seguimientos.ExpedienteObtener> result = await this._dbContext.ExecuteStoredProc<Seguimientos.ExpedienteObtener>(sqlQuery, parametros);

        return result;


    }

    public async Task<int> addAsunto(Seguimientos.ExpedienteObtener expediente)
    {
        int result = 0;



        SqlParameter[] parametros = {
                new SqlParameter("@pi_CatOrganismoId",expediente.CatOrganismoId),
                new SqlParameter("@pi_CatTipoAsuntoId",expediente.TipoAsuntoId),
                new SqlParameter("@pi_NumeroOCC",expediente.NumeroOCC),
                new SqlParameter("@pi_NoExpediente",expediente.Numero),
                new SqlParameter("@pi_EmpleadoId",expediente.EmpleadoExpedienteId),
                new SqlParameter("@pi_TipoProcedimiento",expediente.TipoProcedimientoId)

            };

        string sqlQuery = "[dbo].[uspx_addAsunto]";

        result = await this._dbContext.ExecuteStoredProcObj<int>(sqlQuery, parametros);

        return result;


    }


}