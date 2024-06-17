using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Consulta.ValidarAudiencia;
public class ValidarDisponibilidadRequest
{
    public DateTime FechaAudiencia { get; set; }
    public string HoraAudiencia { get; set; }
}
