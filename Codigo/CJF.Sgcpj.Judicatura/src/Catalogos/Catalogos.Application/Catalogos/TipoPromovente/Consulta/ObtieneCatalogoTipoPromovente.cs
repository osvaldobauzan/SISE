using MediatR;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.TipoPromovente.Consulta;
public record ObtieneCatalogoTipoPromovente : IRequest<List<CatalogoTipoPromoventeDto>>
{
    public int? CatTipoOrganismoId { get; set; }
    public int CatTipoAsuntoId { get; set; }
    public int? CatOrganismoId { get; set; }
}
