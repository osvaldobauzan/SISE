
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerNumeroPromocion;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerTiempTurnos;
public record DetalleGruposMesRequest : IRequest<List<DetalleGruposMesDto>>
{
    public int EmpleadoId { get; set; }
}
