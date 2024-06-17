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
/// /////////               INTERFASE PARA LA CAPA DE OFICIO  UNICAMENTE EN SEGUIMIENTO 
/// </summary>
public class OficioRepository : IOficioRepository
{

    private readonly IStaticCacheManager _staticCacheManager;
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public OficioRepository(
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

    public async Task<Seguimientos.Oficio> getOficio(Seguimientos.Oficio oficio)
    {

        Seguimientos.Oficio result = new Seguimientos.Oficio();

        SqlParameter[] parametros = {
            new SqlParameter("@pi_CatOrganismoId", oficio.CatOrganismoId),
            new SqlParameter("@pi_Año", oficio.Anio),
            new SqlParameter("@pi_Folio", oficio.Folio),
            new SqlParameter("@pi_TipoAnexoId", oficio.TipoAnexo)
        };
        string sqlQuery = "[dbo].[uspx_getOficio]";

        result = await this._dbContext.ExecuteStoredProcObj<Seguimientos.Oficio>(sqlQuery, parametros);

        return result;


    }

    public async Task<Seguimientos.Oficio> getFolioOficio(Seguimientos.Oficio oficio)
    {

        Seguimientos.Oficio result = new Seguimientos.Oficio();

        SqlParameter[] parametros = {
            new SqlParameter("@pi_asuntoNeun", oficio.CatOrganismoId),
            new SqlParameter("@pi_organismoId", oficio.Anio)

    };

        string sqlQuery = "[SISE3].[uspx_getFolioOficio]";

        result = await this._dbContext.ExecuteStoredProcObj<Seguimientos.Oficio>(sqlQuery, parametros);

        return result;
    }



}

