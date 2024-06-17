using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Oficialia.Application.Oficialia.Consultas.ObtenerPromociones;
public class OficialiaPartesFiltro:IRequest<List<OficialiaPartesDTO>>
{
    public int? IdOrganismo { get; set; }
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public int? IdUsuario { get; set; }
}
