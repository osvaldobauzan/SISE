using MediatR;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Contenido.Consulta;
public record ObtieneCatalogoContenido : IRequest<List<ContenidoDto>>
{
    public int CatTipoOrganismoId { get; set; }
    public int CatTipoAsuntoId { get; set; }
}
