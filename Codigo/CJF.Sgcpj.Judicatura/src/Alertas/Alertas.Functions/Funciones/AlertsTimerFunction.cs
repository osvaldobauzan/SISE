using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Alertas.Application.Alertas.Comandos.EliminarAlertasRoutine;
using MediatR;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Alertas.Functions.Funciones
{
    public class AlertsTimerFunction
    {
        private readonly ILogger<AlertsTimerFunction> _logger;
        private readonly IMediator _mediator;

        public AlertsTimerFunction(ILogger<AlertsTimerFunction> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [FunctionName("PurgadoAlertas")]
        public async Task PurgeAlertsAsync(
            [TimerTrigger("0 */30 * * * *")] TimerInfo timerInfo)
        {
            var alertsRequest = new EliminarAlertasRoutineCommand();

            var result = await _mediator.Send(alertsRequest);
            if (!result.IsSuccess)
            {
                _logger?.LogError(result.ErrorMessage);
            }
        }
    }
}
