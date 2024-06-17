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
/// ///////                INTERFASE  DE PROMOCION  UNICAMENTE PARA SEGUIMIENTO
/// </summary>
public class PromocionRepository : IPromocionRepository
{

    private readonly IStaticCacheManager _staticCacheManager;
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public PromocionRepository(
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

    public async Task<Seguimientos.Promociones> getOrdenPromocion(Seguimientos.Promociones promocion)
    {

        Seguimientos.Promociones result = new Seguimientos.Promociones();
        SqlParameter[] parametros = {
            new SqlParameter("@pi_asuntoNeun", promocion.Neun),
            new SqlParameter("@pi_organismoId", promocion.OrganismoId)
        };
        string sqlQuery = "[SISE3].[uspx_getPromocion]";
        result = await this._dbContext.ExecuteStoredProcObj<Seguimientos.Promociones>(sqlQuery, parametros);

        return result;



    }

    public async Task<Seguimientos.Promociones> getPromocion(Seguimientos.Promociones promocion)
    {
        Seguimientos.Promociones result = new Seguimientos.Promociones();

        SqlParameter[] parametros = {
            new SqlParameter("@pi_catOrganismoId", promocion.OrganismoId),
            new SqlParameter("@pi_yearPromocion", promocion.YearPromocion),
            new SqlParameter("@pi_numeroOrden", promocion.Numero)
    };
        string sqlQuery = "[dbo].[uspx_getPromocion]";

        result = await this._dbContext.ExecuteStoredProcObj<Seguimientos.Promociones>(sqlQuery, parametros);

        return result;


    }

}
