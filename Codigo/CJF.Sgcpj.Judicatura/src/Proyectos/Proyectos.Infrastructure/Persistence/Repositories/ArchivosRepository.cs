using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Persistence;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerArchivos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Proyectos.Infrastructure.Persistence.Repositories;

public class ArchivosRepository : IArchivosRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<ArchivosRepository> _logger;

    public ArchivosRepository(ApplicationDbContext dbContext, ILogger<ArchivosRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<string> RutaEscrituraPorModulo(string modulo)
    {
        try
        {
            List<SqlParameter> parametros = new()
        {
            new("@pi_Modulo", modulo)
        };

            var rutas = await _dbContext.ExecuteStoredProc<RutasNas>("[SISE3].[pcRutasChunkXModulo]", parametros.ToArray());

            var ruta = rutas.FirstOrDefault(s => s.Iescritura);

            return ruta == null ? throw new Exception("No existe ruta configurada para guardar el documento") : ruta.Sruta;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<string> RutaEscrituraPorModuloHistorico(string modulo)
    {
        try
        {
            List<SqlParameter> parametros = new()
        {
            new("@pi_Modulo", modulo)
        };

            var rutas = await _dbContext.ExecuteStoredProc<RutasNas>("[SISE3].[pcRutasChunkHistoricasXModulo]", parametros.ToArray());

            var ruta = rutas.FirstOrDefault(s => s.Iescritura);

            return ruta == null ? throw new Exception("No existe ruta configurada para guardar el documento") : ruta.Sruta;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<List<RutasNas>> RutasPorModuloHistorico(string modulo)
    {
        try
        {
            List<SqlParameter> parametros = new()
        {
            new("@pi_Modulo", modulo)
        };

            var rutas = await _dbContext.ExecuteStoredProc<RutasNas>("[SISE3].[pcRutasChunkHistoricasXModulo]", parametros.ToArray());

            return !rutas.Any() ? throw new Exception("No existe ruta configurada para guardar el documento") : rutas;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<List<ArchivoDto>> ObtenerArchivoDTO(long id)
    {
        try
        {
            var parametros = new List<SqlParameter>()
        {
            new SqlParameter("@pi_pkProyectoArchivoId", id),
        };
            var sqlQuery = "[SISE3].[pcTableroProyectoObtieneInfoArchivo]";

            return await _dbContext.ExecuteStoredProc<ArchivoDto>(sqlQuery, parametros.ToArray());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
