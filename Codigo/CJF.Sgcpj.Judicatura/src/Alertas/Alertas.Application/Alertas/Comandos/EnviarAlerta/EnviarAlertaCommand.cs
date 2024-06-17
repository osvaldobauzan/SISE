using Azure.Data.Tables;
using Azure.Identity;
using CJF.Sgcpj.Judicatura.Alertas.Application.Alertas.Comandos.PlugIns;
using CJF.Sgcpj.Judicatura.Alertas.Application.Alertas.Comandos.PlugIns.TableStorage.Entities;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CJF.Sgcpj.Judicatura.Alertas.Application.Alertas.Comandos.EnviarAlerta;

[SesionNoRequiredAttribute]
public class EnviarAlertasCommand : IRequest<(bool IsSuccess, string? ErrorMessage)>
{
    public string Alerts { get; set; }
    public List<object> PlugInsParams { get; set; } = new List<object>();
    public TipoAlertaDTO TipoDeAlerta { get; set; }
}

public class EnviarAlertaCommandHandler : IRequestHandler<EnviarAlertasCommand, (bool IsSuccess, string? ErrorMessage)>
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;

    public EnviarAlertaCommandHandler(IMediator mediator, ILogger logger, IConfiguration configuration)
    {
        _mediator = mediator;
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<(bool IsSuccess, string? ErrorMessage)> Handle(EnviarAlertasCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var alert = JsonConvert.DeserializeObject<AlertDTO<SignalRAlertDTO>>(request.Alerts);

            if (alert.PersistirAlerta)
            {
                var setting = _configuration["SISE3:BackEnd:AlertasUrlTabla"];
                var tableClient = new TableClient(new Uri(setting), "Alertas", new DefaultAzureCredential());

                foreach (var destinatario in alert.Destinatarios)
                {
                    await tableClient.UpsertEntityAsync<AlertEntity>(new AlertEntity()
                    {
                        PartitionKey = destinatario.UsuarioId,
                        RowKey = Guid.NewGuid().ToString(), 
                        TipoDeAlerta = AlertType.SignalR,
                        Emisor = alert.Alerta.Emisor,
                        Estado = alert.Alerta.Estado,
                        Mensaje = alert.Alerta.Mensaje,
                        OrganismoId = destinatario.OrganismoId,
                        Parte = alert.Alerta.Parte,
                        Receptor = alert.Alerta.Receptor,
                        CreatedOn = DateTime.UtcNow
                    });
                }
            }

            var mediatorMessage = new AlertPlugInsListDTO()
            {
                PlugInsParameters = request.PlugInsParams,
                Alerts = request.Alerts,
                TipoDeAlerta = request.TipoDeAlerta.TipoDeAlerta
            };

            await _mediator.Publish(mediatorMessage);
            return (true, null);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex.ToString());
            throw new Exception(ex.ToString());
        }
    }
}


