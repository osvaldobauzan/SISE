using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Common.Infrastructure.Persistence.Repositories;
public class RutasChunkRepository : IRutasChunkRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<RutasChunkRepository> _logger;

    public RutasChunkRepository(ApplicationDbContext dbContext, ILogger<RutasChunkRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IEnumerable<RutaHistorica>?> GetRutasHistoricasPorModulo(string modulo)
    {
        try
        {
            var listaParametros = new List<SqlParameter>() { new SqlParameter("@pi_Modulo", modulo) };
            var result = await _dbContext.ExecuteStoredProc<RutasChunk>("[SISE3].[pcRutasChunkHistoricasXModulo]", listaParametros.ToArray());

            return result is null ? null : result.Select(r => new RutaHistorica
            {
                sRuta = r.SRuta,
                kId = r.KId
            }).ToList();
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex.ToString());
            throw;
        }
    }

    public async Task<bool> ActualizarRutaHistorica(int idRuta, int modulo, int catOrganismoId, long? asuntoNeunId, int? yearPromocion, int? numeroOrden, int? asuntoDocumentoId)
    {
        try
        {
            var listaParametros = new List<SqlParameter>()
            {
                new SqlParameter("@pi_AsuntoNeunId", asuntoNeunId.HasValue ? asuntoNeunId : DBNull.Value),
                new SqlParameter("@pi_YearPromocion", yearPromocion.HasValue ? yearPromocion : DBNull.Value),
                new SqlParameter("@pi_NumeroOrden", numeroOrden.HasValue ? numeroOrden : DBNull.Value),
                new SqlParameter("@pi_catIdOrganismo", catOrganismoId),
                new SqlParameter("@pi_Origen", DBNull.Value),
                new SqlParameter("@pi_TipoModulo", modulo),
               new SqlParameter("@pi_AsuntoDocumentoId", asuntoDocumentoId.HasValue ? asuntoDocumentoId : DBNull.Value),
                new SqlParameter("@pi_kIdRuta", idRuta)
            };

            await _dbContext.ExecuteStoredProcObj<string>("[SISE3].[piActualizaRutaArchivo]", listaParametros.ToArray());

            return true;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex.ToString());
            throw;
        }
    }

    public async Task<IEnumerable<RutaNas>?> RutaArchivo(string modulo)
    {
        List<SqlParameter> parametros = new List<SqlParameter>();

        var pi_Modulo = new SqlParameter("@pi_Modulo", modulo);
        parametros.Add(pi_Modulo);

        var rutas = await _dbContext.ExecuteStoredProc<RutasNas>("[SISE3].[pcRutasChunkXModulo]", parametros.ToArray());

        var ruta = rutas.FirstOrDefault(s => s.Iescritura);

        if (ruta == null)
        {
            throw new Exception("No existe ruta configurada para guardar el documento");
        }
        return rutas is null ? null : rutas.Select(r => new RutaNas
        {
            RutaId = r.KId,
            Ruta = r.Sruta
        }).ToList();
    }
}
