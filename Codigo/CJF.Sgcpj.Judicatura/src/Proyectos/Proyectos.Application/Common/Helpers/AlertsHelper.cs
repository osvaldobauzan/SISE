using CJF.Sgcpj.Judicatura.Common.Application.Common.Alertas;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Models;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Helpers;

public class AlertsHelper
{
    private readonly IAlertsMessageService _alertsMessageService;
    private readonly ILogger<AlertsHelper> _logger;

    public AlertsHelper(IAlertsMessageService alertsMessageService, ILogger<AlertsHelper> logger)
    {
        _alertsMessageService = alertsMessageService;
        _logger = logger;
    }

    public void SendAlert(EnviarAlerta alerta)
    {
        try
        {
            var alert = new AlertDTO<SignalRAlertDTO>()
            {
                TipoDeAlerta = AlertType.SignalR,
                Destinatarios = alerta.Destinatarios,
                Alerta = new SignalRAlertDTO()
                {
                    Id = Guid.NewGuid(),
                    Emisor = "Proyectos",
                    Mensaje = alerta.Mensaje
                },
                PersistirAlerta = true
            };

            _alertsMessageService.TriggerAlertAsync(alert).ConfigureAwait(false).GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }
}
