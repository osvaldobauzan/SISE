using MediatR;

namespace CJF.Sgcpj.Judicatura.Sentencias.Application.Sentencias.Consultas.ObtenerPromociones;
public record class ObtenerPromocionesExpediente : IRequest<ListadoPromocionesDTO>
{
    public int AsuntoId { get; set; }
    public long AsuntoNeunId { get; set; }
    public int TipoCuaderno { get; set; }
    public int SintesisOrden { get; set; }
}
