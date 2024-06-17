using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Audiencia.Consulta;
public class CatalogAudienciaRequest : IRequest<List<CatalogoAudienciaDto>>
{
    public int TipoAsuntoId { get; set; }
}
