using MediatR;

namespace CJF.Sgcpj.Judicatura.Application.Cuadernos.Consulta;
public record ObtieneCatalogoCuaderno : IRequest<List<CuadernoDto>>
{
    public int? TipoAsuntoId { get; set; }
    public int? CuadernoId { get; set; }
}

