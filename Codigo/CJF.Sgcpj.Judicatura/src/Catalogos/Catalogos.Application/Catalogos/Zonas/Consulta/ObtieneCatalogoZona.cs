using MediatR;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Zonas.Consulta;
public record ObtieneCatalogoZona : IRequest<List<CatalogoZonaDto>>
{
    public int CatOrganismoId { get; set; }
}
