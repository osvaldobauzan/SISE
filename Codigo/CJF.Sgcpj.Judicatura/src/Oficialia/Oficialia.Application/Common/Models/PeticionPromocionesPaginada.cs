using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtienePromocionesConFiltro;

namespace CJF.Sgcpj.Judicatura.Application.Common.Models;
public class PeticionPromocionesPaginada : PaginacionBase
{
    public DateTime FechaInicial { get; init; }
    public DateTime FechaFinal { get; init; }

    public int Estado { get; set; }
    public string Texto { get; set; }

}
