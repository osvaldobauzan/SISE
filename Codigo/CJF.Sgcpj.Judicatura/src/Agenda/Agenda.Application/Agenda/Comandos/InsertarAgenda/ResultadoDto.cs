using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Comandos.InsertarAgenda;
public class ResultadoDto
{
    public int IdAgenda { get; set; }
    public int IdAudiencia { get; set; }
    public int IdResultado { get; set; }
    public string Expedinte { get; set; }
    public DateTime FechaAudiencia { get; set; }    
}
