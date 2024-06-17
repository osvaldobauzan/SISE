using Azure.Data.Tables;
using Azure.Identity;
using CJF.Sgcpj.Judicatura.Alertas.Application.Alertas.Comandos.PlugIns.TableStorage.Entities;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Alertas.Application.Alertas.Comandos.EliminarAlertasRoutine;

[SesionNoRequired]
public class EliminarAlertasRoutineCommand : IRequest<(bool IsSuccess, string? ErrorMessage)>
{
}

public class EliminarAlertasRoutineCommandHandler : IRequestHandler<EliminarAlertasRoutineCommand, (bool IsSuccess, string? ErrorMessage)>
{
    private readonly ILogger<EliminarAlertasRoutineCommandHandler> _logger;
    private readonly IConfiguration _configuration;

    public EliminarAlertasRoutineCommandHandler(ILogger<EliminarAlertasRoutineCommandHandler> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<(bool IsSuccess, string? ErrorMessage)> Handle(EliminarAlertasRoutineCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var setting = _configuration["SISE3:BackEnd:AlertasUrlTabla"];
            var tableClient = new TableClient(new Uri(setting), "Alertas", new DefaultAzureCredential());

            var now = DateTime.UtcNow.AddDays(-15);
            var queryResult = tableClient.Query<AlertEntity>(filter: $"CreatedOn le datetime'{now.ToString("yyyy-MM-ddTHH:mm:ss")}'");

            if (queryResult is null || !queryResult.Any())
            {
                _logger.LogInformation("No alerts found on delete routine");
                return (true, "No alerts found");
            }

            foreach (var row in queryResult)
            {
                await tableClient.DeleteEntityAsync(row.PartitionKey, row.RowKey);
            }

            return (true, null);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex.ToString());
            return (false, ex.Message);
        }
    }
}


