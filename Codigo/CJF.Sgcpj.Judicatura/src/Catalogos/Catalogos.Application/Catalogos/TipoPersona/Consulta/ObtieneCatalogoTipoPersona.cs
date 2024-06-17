using MediatR;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.TipoPersona.Consulta;
public class ObtieneCatalogoTipoPersona : IRequest<List<CatalogoTipoPersonaDto>>
{
    public int CatTipoAsuntoId { get; set; }
}
