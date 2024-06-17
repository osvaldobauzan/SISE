using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Generico.Consulta;
/// <summary>
/// Representa la clase que provee del catálogo a consultar 
/// </summary>
public class CatalogoGenericoFiltro : IRequest<List<CatalogoGenericoDTO>>
{
    /// <summary>
    /// Identificador del catálogo a consultar
    /// </summary>
    public int CatalogoId { get; set; }
}
