using MediatR;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerPromocionXExpediente;
public record class ObtienePromocionXExpediente : IRequest<List<ObtienePromocionXExpedienteDto>>
{
    public long AsuntoNeunId { get; set; }
    public string NoExpediente { get; set; }
}
