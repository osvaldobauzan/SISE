using System.Text;
using Azure.Identity;
using Azure.Storage.Queues;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Alertas;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;

public class AlertsMessageService : IAlertsMessageService
{
    private const string QueueDoesntExists = "La cola no existe";
    private readonly IConfiguration _configuration;
    private readonly ILogger<AlertsMessageService> _logger;

    public AlertsMessageService(IConfiguration configuration,
                               ILogger<AlertsMessageService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<(bool IsSuccess, string? ErrorMessage)> TriggerAlertAsync<T>(AlertDTO<T> alert) where T : IValidAlertMessages
    {
        try
        {
            var queueClient = new QueueClient(
                new Uri($"{_configuration["SISE3:BackEnd:AlertasUrlCola"]}/alertsqueue"),
                new DefaultAzureCredential());               
            if (!await queueClient.ExistsAsync())
            {
                _logger?.LogWarning($"La queue de alertas no existe, intentando crearla...");
                var queueCreation = await queueClient.CreateAsync();
                if (queueCreation.IsError)
                {
                    _logger?.LogError($"No fue posible crear la queue alertsqueue. Error: {queueCreation.Content} - {queueCreation.ReasonPhrase}");
                    return (false, QueueDoesntExists);
                }
            }

            var jsonMessage = JsonConvert.SerializeObject(alert);
            var bytes = Encoding.UTF8.GetBytes(jsonMessage);

            await queueClient.SendMessageAsync(Convert.ToBase64String(bytes));
            return (true, null);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex.ToString());
            return (false, ex.Message);
        }
    }
}
