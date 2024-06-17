using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Anexo.Consulta;

public record ObtieneCatalogosAnexo : IRequest<List<CatalogosAnexoDto>>
{
    public int? CatTipoCatalogoAsuntoId { get; set; }
    public int? CatOrganismoId { get; set; }
    public int? CatTipoAsuntoId { get; set; }

}
