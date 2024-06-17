using CJF.Sgcpj.Judicatura.Alertas.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CJF.Sgcpj.Judicatura.Alertas.Application.Alertas.Comandos.PlugIns.Email;

public class EmailAlertStrategyHandler : INotificationHandler<AlertPlugInsListDTO>
{
    private readonly IEmailMessage _emailService;
    private readonly ILogger _logger;

    public EmailAlertStrategyHandler(IEmailMessage emailService, ILogger logger)
    {
        _emailService = emailService;
        _logger = logger;
    }

    public async Task Handle(AlertPlugInsListDTO alerts, CancellationToken cancellationToken)
    {
        try
        {
            if (alerts.TipoDeAlerta != AlertType.Email)
            {
                return;
            }

            var deserializedAlert = JsonConvert.DeserializeObject<AlertDTO<EmailAlertDTO>>(alerts.Alerts);

            foreach (var destinatario in deserializedAlert.Destinatarios)
            {
                var emailResult = await _emailService.SendEmail
                    (destinatario.DireccionDestino, deserializedAlert.Alerta.Asunto, deserializedAlert.Alerta.BodyCorreo);

                if (!emailResult.IsSuccess)
                    _logger?.LogError($"Email error: {emailResult.ErrorMessage}");
                else
                    _logger?.LogInformation("Email sent");
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex.ToString());
        }
    }
}


