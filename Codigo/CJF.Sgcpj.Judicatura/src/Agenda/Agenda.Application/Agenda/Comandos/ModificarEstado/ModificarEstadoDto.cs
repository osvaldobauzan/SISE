using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Comandos.ModificarEstado;
public class ModificarEstadoDto
{
    public int IdAgenda { get; set; }
    public int IdResultado { get; set; }
    public string Descripcion { get; set; }
}
