using MediatR;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Cuadernos.Consulta;
public record ObtieneCatalogoCuaderno : IRequest<List<CuadernoDto>>
{
    public int? TipoAsuntoId { get; set; }
    public int? CuadernoId { get; set; }
    public long? AsuntoNeunId { get; set; }
}

