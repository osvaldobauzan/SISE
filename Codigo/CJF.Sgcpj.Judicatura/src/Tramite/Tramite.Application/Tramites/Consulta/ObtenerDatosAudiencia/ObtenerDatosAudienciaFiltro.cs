using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerDatosAudiencia;
public class ObtenerDatosAudienciaFiltro : IRequest<DatosAudienciaDTO>
{
    public long AsuntoNeunId { get; set; }
    public int TipoAgendaId { get; set; }
}
