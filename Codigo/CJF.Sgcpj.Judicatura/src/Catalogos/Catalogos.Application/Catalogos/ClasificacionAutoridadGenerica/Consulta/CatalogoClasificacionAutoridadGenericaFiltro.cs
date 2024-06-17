using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Generico.Consulta;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.ClasificacionAutoridadGenerica.Consulta;
public class CatalogoClasificacionAutoridadGenericaFiltro : IRequest<List<CatalogoClasificacionAutoridadGenericaDTO>>
{
    /// <summary>
    /// Identificador del catálogo a consultar
    /// </summary>
    public int CatTipoAsuntoId { get; set; }
}
