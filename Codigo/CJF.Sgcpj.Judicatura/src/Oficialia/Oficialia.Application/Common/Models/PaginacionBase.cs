using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Application.Common.Models;
public class PaginacionBase
{
    public string OrdenarPor { get; set; }
    public bool Descendente { get; set; }
    public int Pagina { get; set; }
    public int RegistrosPorPagina { get; set; }
    public int TotalRegistros { get; set; }
}
