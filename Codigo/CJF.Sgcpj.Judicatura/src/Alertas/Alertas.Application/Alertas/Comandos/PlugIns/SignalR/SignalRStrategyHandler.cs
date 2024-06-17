using CJF.Sgcpj.Judicatura.Alertas.Application.Alertas.Comandos.PlugIns;
using CJF.Sgcpj.Judicatura.Alertas.Application.Alertas.Comandos.PlugIns.SignalR;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using MediatR;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CJF.Sgcpj.Judicatura.Application.Alertas.Comandos.PlugIns.SignalR;
public class SignalRStrategyHandler : INotificationHandler<AlertPlugInsListDTO>
{
    private readonly IUserConnectionsHandler _connectionsHandler;
    private readonly ILogger _logger;

    public SignalRStrategyHandler(IUserConnectionsHandler connectionsHandler, ILogger logger)
    {
        _connectionsHandler = connectionsHandler;
        _logger = logger;
    }

    public async Task Handle(AlertPlugInsListDTO alerts, CancellationToken cancellationToken)
    {
        try
        {
            if (alerts.TipoDeAlerta != AlertType.SignalR)
            {
                return;
            }

            var signalRHandler = alerts.PlugInsParameters
                                       .Where(x => x.GetType() == typeof(SignalRAsyncCollector<SignalRMessage>))
                                       .First() as IAsyncCollector<SignalRMessage>;

            var alertas = JsonConvert.DeserializeObject<SignalRAlert>(alerts.Alerts);

            var destinatarioTasks = alertas.Destinatarios.Select(async (destinatario) =>
            {
                var connectionsResult = await _connectionsHandler.QueryConnectionAsync(destinatario.UsuarioId);

                if (!connectionsResult.IsSuccess ||
                    connectionsResult.Results is null ||
                    !connectionsResult.Results.Any(x => x.OrganismoId == destinatario.OrganismoId))
                {
                    return;
                }

                var conexionesTasks = connectionsResult.Results
                                                       .Where(x => x.OrganismoId == destinatario.OrganismoId)
                                                       .Select(async (conexion) =>
                                                       {
                                                           await signalRHandler.AddAsync(new SignalRMessage
                                                           {
                                                               Target = "newMessage",
                                                               Arguments = new[] { alertas.Alerta },
                                                               ConnectionId = conexion.ConnectionId
                                                           });
                                                       });

                await Task.WhenAll(conexionesTasks);

                _logger?.LogInformation("SignalR message sent");
            });

            await Task.WhenAll(destinatarioTasks);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex.ToString());
        }
    }
}
