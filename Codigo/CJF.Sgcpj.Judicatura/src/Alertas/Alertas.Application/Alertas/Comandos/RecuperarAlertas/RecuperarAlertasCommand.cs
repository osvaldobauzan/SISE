using Azure.Data.Tables;
using Azure.Identity;
using CJF.Sgcpj.Judicatura.Alertas.Application.Alertas.Comandos.PlugIns.TableStorage.Entities;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Alertas.Application.Alertas.Comandos.RecuperarAlertas;

public class RecuperarAlertasCommand : IRequest<IEnumerable<AlertQueryDTO>?>
{
}

public class RecuperarAlertasCommandHandler : IRequestHandler<RecuperarAlertasCommand, IEnumerable<AlertQueryDTO>?>
{
    private readonly ILogger<RecuperarAlertasCommandHandler> _logger;
    private readonly ISesionService _sesionService;
    private readonly IConfiguration _configuration;

    public RecuperarAlertasCommandHandler(ILogger<RecuperarAlertasCommandHandler> logger, ISesionService sesionService,
                                          IConfiguration configuration)
    {
        _logger = logger;
        _sesionService = sesionService;
        _configuration = configuration;
    }

    public async Task<IEnumerable<AlertQueryDTO>?> Handle(RecuperarAlertasCommand request, CancellationToken cancellationToken)
    {
        var setting = _configuration["SISE3:BackEnd:AlertasUrlTabla"];
        var tableClient = new TableClient(new Uri(setting), "Alertas", new DefaultAzureCredential());

        var userId = _sesionService.SesionActual.EmpleadoId;
        var organismoId = _sesionService.SesionActual.CatOrganismoId;

        var alertsResult = tableClient.Query<AlertEntity>($"PartitionKey eq '{userId}' and OrganismoId eq'{organismoId}'");
        if (alertsResult is null || !alertsResult.Any())
        {
            return null;
        }

        var result = new List<AlertQueryDTO>();
        foreach (var alert in alertsResult)
        {
            result.Add(new AlertQueryDTO()
            {
                Id = Guid.Parse(alert.RowKey),
                TipoDeAlerta = alert.TipoDeAlerta,
                HoraDeLaAlerta = alert.CreatedOn,
                Mensaje = alert.Mensaje,
                Emisor = alert.Emisor,
                Receptor = alert.Receptor,
                Estado = alert.Estado,
                Parte = alert.Parte
            });
        }

        return result;
    }
}


