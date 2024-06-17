using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerCOE;
public class ObtenerCOERequest : IRequest<ObtenerCOEDto>
{
    public long NotificacionElectronicaId { get; set; }
}
