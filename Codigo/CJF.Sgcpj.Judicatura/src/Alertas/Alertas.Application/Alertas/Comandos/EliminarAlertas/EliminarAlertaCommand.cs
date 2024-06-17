using Azure.Data.Tables;
using Azure.Identity;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Alertas.Application.Alertas.Comandos.EliminarAlertas;

public class EliminarAlertasCommand : IRequest<bool>
{
    public string AlertId { get; set; }
}

public class EliminarAlertasCommandHandler : IRequestHandler<EliminarAlertasCommand, bool>
{
    private readonly ILogger<EliminarAlertasCommandHandler> _logger;
    private readonly ISesionService _sesionService;
    private readonly IConfiguration _configuration;

    public EliminarAlertasCommandHandler(ILogger<EliminarAlertasCommandHandler> logger,
                                         ISesionService sesionService,
                                         IConfiguration configuration)
    {
        _logger = logger;
        _sesionService = sesionService;
        _configuration = configuration;
    }

    public async Task<bool> Handle(EliminarAlertasCommand request, CancellationToken cancellationToken)
    {
        var userId = _sesionService.SesionActual.EmpleadoId;
        var setting = _configuration["SISE3:BackEnd:AlertasUrlTabla"];
        var tableClient = new TableClient(new Uri(setting), "Alertas", new DefaultAzureCredential());

        await tableClient.DeleteEntityAsync(userId.ToString(), request.AlertId);
        _logger.LogInformation($"Entity deleted {userId} - {request.AlertId}");

        return true;
    }
}


