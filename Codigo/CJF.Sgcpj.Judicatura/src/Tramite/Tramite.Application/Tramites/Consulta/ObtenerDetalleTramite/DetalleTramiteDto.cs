using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerDetalleTramite;
public class DetalleTramiteDto<R, S, T, U>
{
    public List<R> CabeceraTramite { get; set; }
    public List<S> Promociones { get; set; }
    public List<T> Partes { get; set; }
    public List<U> Oficio { get; set; }
}
