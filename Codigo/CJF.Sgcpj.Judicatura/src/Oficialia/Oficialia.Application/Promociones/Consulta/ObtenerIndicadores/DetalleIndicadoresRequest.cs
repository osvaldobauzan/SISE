using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerNumeroPromocion;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerIndicadores;
public record DetalleIndicadoresRequest : IRequest<List<DetalleIndicadoresDto>>
{
    public DateTime FechaInicioBusqueda { get; set; }
    public DateTime FechaFinBusqueda { get; set; }
    public int Organizacion { get; set; }
}
