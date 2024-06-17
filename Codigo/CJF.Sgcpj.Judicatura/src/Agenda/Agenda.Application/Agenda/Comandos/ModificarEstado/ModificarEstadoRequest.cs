using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Comandos.InsertarAgenda;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Comandos.ModificarEstado;
public class ModificarEstadoRequest : IRequest<ResultadoModificarEstadoDto>
{
    public int IdAgenda  { get; set; }
    public int IdResultado { get; set; }
}
