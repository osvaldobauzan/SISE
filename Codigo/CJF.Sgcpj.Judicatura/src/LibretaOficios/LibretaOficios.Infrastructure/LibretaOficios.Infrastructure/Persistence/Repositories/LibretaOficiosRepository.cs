using PolyCache.Cache;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Persistence;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.LibretaOficios.Application.Common.Models;
using CJF.Sgcpj.Judicatura.LibretaOficios.Application.Common.Repositories;

namespace CJF.Sgcpj.Judicatura.LibretaOficios.Infrastructure.Persistence.Repositories;
public class LibretaOficiosRepository : ILibretaOficiosRepository
{
    private readonly IStaticCacheManager _staticCacheManager;
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public LibretaOficiosRepository(
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
    public async Task<List<LibretaOficio>> ObtenerLibretaOficio(LibretaOficioFiltro param)
    {
        var parametros = new[]
        {
            new SqlParameter("@pi_CatOrganismoId", param.CatOrganismoId),
            new SqlParameter("@pi_NoRegistro", param.NoRegistros),
            new SqlParameter("@pi_CatidadRegistros", param.CantidadRegistros),
            new SqlParameter("@pi_FechaInicio", (param.FechaInicio.HasValue)?param.FechaInicio:DBNull.Value),
            new SqlParameter("@pi_FechaFin", (param.FechaFin.HasValue)?param.FechaFin:DBNull.Value),
            new SqlParameter("@pi_AsuntoNeunId", (param.AsuntoNeunId != 0)?param.AsuntoNeunId:DBNull.Value),
            new SqlParameter("@pi_Folio",(param.Folio!= 0)?param.Folio:DBNull.Value),
            new SqlParameter("@pi_Anio",(param.Anio != 0)? param.Anio:DBNull.Value)
        };

        string sqlQuery = "[dbo].[uspx_lo_getLibretaOficios]";
        var libretaOficios = await this._dbContext.ExecuteStoredProc<LibretaOficio>(sqlQuery, parametros);
        return libretaOficios;

    }
    public async Task<bool> CancelarOficio(CancelarOficioFiltro param)
    {
        var parametros = new[]
        {
            new SqlParameter("@pi_CatOrganismoId", param.CatOrganismoId),
            new SqlParameter("@pi_EmpleadoId", param.EmpleadoId),
            new SqlParameter("@pi_Folio",param.Folio),
            new SqlParameter("@pi_Anio",param.Anio)
        };

        string sqlQuery = "[dbo].[uspx_lo_updCancelaLibretaOficios]";
        await this._dbContext.ExecuteStoredProc<LibretaOficio>(sqlQuery, parametros);
        return true;

    }
    public async Task<bool> InsertarOficio(InsertarOficioFiltro param)
    {
        var parametros = new[]
        {
            new SqlParameter("@pi_AsuntoNeunId", param.AsuntoNeunId),
            new SqlParameter("@pi_AsuntoId", param.AsuntoId),
            new SqlParameter("@pi_AsuntoDocumentoId", param.AsuntoDocumentoId),
            new SqlParameter("@pi_AnexoTipoId", param.AnexoTipoId),
            new SqlParameter("@pi_NombreDocumento", param.NombreDocumento),
            new SqlParameter("@pi_RutaAnexo", param.RutaAnexo),
            new SqlParameter("@pi_NombreArchivo", param.NombreArchivo),
            new SqlParameter("@pi_ExtensionDocumento", param.ExtensionDocumento),
        };

        string sqlQuery = "[dbo].[usp_AnexosIns]";
        await this._dbContext.ExecuteStoredProc<LibretaOficio>(sqlQuery, parametros);
        return true;

    }
}

