using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Comandos.ModificarEstado;
public class ResultadoModificarEstadoDto
{
    public string MessageResultado { get; set; }
    public bool EsExito { get; set; }
    public ModificarEstadoDto ResultadoAudiencia { get; set; } 
}
