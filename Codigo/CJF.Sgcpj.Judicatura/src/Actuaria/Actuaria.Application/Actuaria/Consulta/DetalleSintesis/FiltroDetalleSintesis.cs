using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.DetalleSintesis;
public class FiltroDetalleSintesis : IRequest<List<DetalleSintesisDTO>>
{
    public long AsuntoNeunId { get; set; }
    public int SintesisOrden {  get; set; }

}
