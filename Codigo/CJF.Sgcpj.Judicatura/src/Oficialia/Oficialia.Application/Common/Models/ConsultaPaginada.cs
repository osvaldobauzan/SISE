using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtienePromocionesConFiltro;

namespace CJF.Sgcpj.Judicatura.Application.Common.Models;
public class ConsultaPaginada { 
    public DateTime FechaInicial { get; init; }
    public DateTime FechaFinal { get; init; }

    public int Estado { get; set; }
    public string Texto { get; set; }
    public string OrdenarPor { get; set; }
    public bool Descendente { get; set; }
    public int Pagina { get; set; }
    public int RegistrosPorPagina { get; set; }
    public int OrganismoID { get; set; }
    public int TotalRegistros { get; set; }
    public long AsuntoNeunId { get; set; }
    public string Origen { get; set; }
    public long Asignado { get; set; }
    public string Capturo { get; set; }
}
