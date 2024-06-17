using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Anexo.Consulta;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.ContenidoTramite.Consulta;
public class ObtieneCatalogoContenidoTramite : IRequest<List<ContenidoTramiteDto>>
{
    public int? CatTipoCatalogoAsuntoId { get; set; }
    public int? CatOrganismoId { get; set; }
    public int? CatTipoAsuntoId { get; set; }

}
