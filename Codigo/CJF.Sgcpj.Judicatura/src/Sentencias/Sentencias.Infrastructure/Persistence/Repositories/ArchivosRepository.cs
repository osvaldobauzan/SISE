using Aspose.Pdf.Operators;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Persistence;
using CJF.Sgcpj.Judicatura.Sentencias.Application.Sentencias.Consultas.ObtenerSentencias;
using CJF.Sgcpj.Judicatura.Sentencias.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Sentencias.Application.Common.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Sentencias.Infrastructure.Persistence.Repositories;
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

    public async Task<List<ArchivoSentenciaDto>> ObtenerArchivoDTO(long AsuntoNeunId, long AsuntoDocumentoId)
    {
        try
        {
            var parametros = new List<SqlParameter>()
        {
            new SqlParameter("@pi_AsuntoNeunId", AsuntoNeunId),
            new SqlParameter("@pi_AsuntoDocumentoId", AsuntoDocumentoId),
        };
            var sqlQuery = "[SISE3].[pcTableroSentenciasObtieneInfoDocumento]";

            return await _dbContext.ExecuteStoredProc<ArchivoSentenciaDto>(sqlQuery, parametros.ToArray());
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

    public async Task<IEnumerable<SentenciaArchivo>> ObtenerSentenciaPorIdModuloTipoAsync(Guid id, int modulo, string tipoArchivo)
    {
        try
        {
            var parametros = new List<SqlParameter>()
            {
                new SqlParameter("@pi_GuidDocumento", id.ToString()),
                new SqlParameter("@pi_TipoModulo", modulo.ToString()),
                new SqlParameter("@pi_TipoArchivo", tipoArchivo)
            };

            var sqlQuery = "[SISE3].[pcConsultaRutaXModuloGUID]";

            return await _dbContext.ExecuteStoredProc<SentenciaArchivo>(sqlQuery, parametros.ToArray());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
