using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Alertas.Application.Alertas.Comandos.EnviarAlerta;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using MediatR;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Alertas.Functions.Funciones
{
    public class AlertsQueueFunction
    {
        private readonly IMediator _mediator;

        public AlertsQueueFunction(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("AlertasFunction")]
        [FixedDelayRetry(5,"00:8:00")]
        public async Task Run([QueueTrigger("alertsqueue")] string myQueueItem,
                    [SignalR(HubName = "AlertsHub")] IAsyncCollector<SignalRMessage> signalRMessages, ILogger _logger)
        {
            var tipoDeAlerta = JsonConvert.DeserializeObject<TipoAlertaDTO>(myQueueItem);

            var emailCommand = new EnviarAlertasCommand();
            emailCommand.Alerts = myQueueItem;
            emailCommand.PlugInsParams.Add(signalRMessages);
            emailCommand.TipoDeAlerta = tipoDeAlerta;

            var result = await _mediator.Send(emailCommand);
            if (!result.IsSuccess)
            {
                _logger?.LogError(result.ErrorMessage);
            }
        }
    }
}
