using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Consulta.ValidarAudiencia;
public class ObtieneResultadoDto
{
    public string Expediente { get; set; }
    public DateTime FechaAudiencia { get; set; }
    public int IdAgenda { get; set; }
    public int Resultado { get; set; }
    public int IdAudiencia { get; set; }
}
