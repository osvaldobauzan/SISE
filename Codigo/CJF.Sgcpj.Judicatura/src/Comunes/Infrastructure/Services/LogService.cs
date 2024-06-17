using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;

public class LogService : ILogService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<LogService> _logger;

    public LogService(ApplicationDbContext dbContext, ILogger<LogService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task RegistrarEvento(DatosLog log)
    {
        try
        {
            var parametros = new List<SqlParameter>()
            {
                new("@iTipoMovimiento", (int)log.TipoMovimiento),
                new("@iUsuario", log.IdUsuario),
                new("@sDTOEntrada", log.DatosEntrada),
                new("@sDTOSalida", log.DatosSalida),
                new("@fkIdModuloOrigen", log.ModuloOrigen)
            };

            await _dbContext.ExecuteStoredProcNonQuery("SISE3.InsertBitCRUD", parametros.ToArray());
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Ocurrió un error al registrar el log de eventos");
        }
    }
}
