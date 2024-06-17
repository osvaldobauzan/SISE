using System.Text;
using Azure.Identity;
using Azure.Storage.Queues;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.GenerarOficio;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Procesos;
public class ProcessQueueService : IProcessQueueService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<ProcessQueueService> _logger;

    public ProcessQueueService(IConfiguration configuration, ILogger<ProcessQueueService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<(bool IsSuccess, string? ErrorMessage)> TriggerProcessAsync(CrearOficiosDTO oficiosMessage)
    {
        try
        {
            var queueClient = new QueueClient(
               new Uri($"{_configuration["SISE3:BackEnd:AlertasUrlCola"]}/processesqueue"),
               new DefaultAzureCredential());

            if (!await queueClient.ExistsAsync())
            {
                _logger?.LogWarning($"La queue de procesos no existe, intentando crearla...");
                var queueCreation = await queueClient.CreateAsync();
                if (queueCreation.IsError)
                {
                    _logger?.LogError($"No fue posible crear la queue processesqueue. Error: {queueCreation.Content} - {queueCreation.ReasonPhrase}");
                    return (false, "La queue no existe");
                }
                return (false, "La queue no existe");
            }

            var jsonMessage = JsonConvert.SerializeObject(oficiosMessage);
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
