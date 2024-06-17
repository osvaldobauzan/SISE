using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.Filtros;
public class FiltroRootDto
{
    public List<FiltroEstadoDto> FiltroEstado { get; set; }
    public List<FiltroContenidoDto> FiltroContenido { get; set; }
}
