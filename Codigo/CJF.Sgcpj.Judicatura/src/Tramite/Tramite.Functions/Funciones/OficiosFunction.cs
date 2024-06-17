using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Comandos.CreacionOficiosComando;
using MediatR;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;

namespace Tramite.Functions.Funciones;

public class OficiosFunction
{
    private readonly IMediator _mediator;

    public OficiosFunction(IMediator mediator)
    {
        _mediator = mediator;
    }

    [FunctionName("OficiosFunction")]
    public async Task Run([QueueTrigger("processesqueue")] string myQueueItem)
    {
        var message = JsonConvert.DeserializeObject<CrearOficiosDTO>(myQueueItem);

        var comando = new CrearOficiosCommand();
        comando.CrearOficiosDTO = message;

        await _mediator.Send(comando);
    }


}
