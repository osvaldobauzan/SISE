using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
public class CatalogoAsunto
{
    public int CatTipoAsuntoId { get; set; }
    public string TipoAsunto { get; set; }
    public int? CuadernoId { get; set; }
}
