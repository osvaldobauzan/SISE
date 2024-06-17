using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Consulta.ObtenerDetalleCaracter;
public class ObtenerDetalleCaracterRequest : IRequest<List<ObtenerDetalleCaracterDto>>
{
    public int IdNeun { get; set; }
    public int IdTipoAsunto { get; set; }
}
