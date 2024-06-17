using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.DetalleSintesis;
public class DetalleSintesisDTO
{
    public DateTime FechaAcuerdo { get; set; }
    public DateTime FechaPublicacion { get; set; }
    public long Titular { get; set; }
    public long Parte1 { get; set; }
    public long Parte2 { get; set; }
    public long ActuarioId { get; set; }
}
