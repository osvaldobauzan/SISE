using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Generico.Consulta;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.TipoSentidoProyecto.Consulta;

public record ObtieneCatalogoTipoSentido : IRequest<List<CatalogoGenericoDTO>>
{
    public int CatTipoAsuntoId { get; set; }

    public int TipoCatalogo { get; set; }
}
