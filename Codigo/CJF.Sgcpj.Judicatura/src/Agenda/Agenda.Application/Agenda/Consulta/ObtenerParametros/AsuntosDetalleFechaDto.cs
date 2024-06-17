using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Consulta.ObtenerParametros;
public class AsuntosDetalleFechaDto
{
    public int TipoAsuntoId { get; set; }
    public DateTime ValorCampoAsunto { get; set; }
    public int NumeroCaptura { get; set; }
    public int NumeroBloque { get; set; }
    public int NumeroBloquePadre { get; set; }
    public int Consecutivo { get; set; }
    public bool Eliminar { get; set; }

}
