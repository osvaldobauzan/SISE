
using MediatR;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerTiempTurnos;
public record DetalleIntervalosRequest : IRequest<List<DetalleIntervalosDto>>
{
    public DateTime FechaInicioBusqueda { get; set; }
    public DateTime FechaFinBusqueda { get; set; }
    public int EmpleadoId { get; set; }
}
