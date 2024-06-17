using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;

namespace CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Consulta.ValidarAudiencia;
public class ValidarDisponibilidadDto
{
    public string Expediente { get; set; }
    public string FechaAudiencia { get; set; }
    public string HoraAudiencia { get; set; }
    public string Empleado { get; set; }
    public string Descipcion { get; set; }
}


