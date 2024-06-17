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
///////////   INTERFASE A BASE DE DATOS PARA ACUERDO DE SEGUIMIENTO ///////////////////////////////

public class AcuerdoRepository : IAcuerdoRepository
{

    private readonly IStaticCacheManager _staticCacheManager;
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public AcuerdoRepository(
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

    public async Task<Seguimientos.Acuerdo> getAcuerdo(Seguimientos.Acuerdo acuerdo)
    {
        int orden;
        Seguimientos.Acuerdo result = new Seguimientos.Acuerdo();


        orden = await getOrden(acuerdo);

        SqlParameter[] parametros = {
            new SqlParameter("@pi_asuntoNeun", acuerdo.Neun),
            new SqlParameter("@pi_organismoId", acuerdo.OrganismoId),
            new SqlParameter("@pi_sintesisOrden", orden)
        };
        string sqlQuery = "[dbo].[uspx_getAcuerdo]";

        result = await this._dbContext.ExecuteStoredProcObj<Seguimientos.Acuerdo>(sqlQuery, parametros);

        if (result == null)
            return null;
        else
        {
            Seguimientos.Acuerdo res = new Seguimientos.Acuerdo();
            res.Mensaje = "Sin resultado";
            return res;
        }
         
        



    }

    public async Task<int> getOrden(Seguimientos.Acuerdo acuerdo)
    {
        int result;

        SqlParameter[] parametros = {
            new SqlParameter("@pi_asuntoNeun", acuerdo.Neun),
            new SqlParameter("@pi_organismoId", acuerdo.OrganismoId)

    };
        string sqlQuery = "[SISE3].[uspx_getOrdenAcuerdo]";

        result = await this._dbContext.ExecuteStoredProcObj<int>(sqlQuery, parametros);

        return result;


    }

}
