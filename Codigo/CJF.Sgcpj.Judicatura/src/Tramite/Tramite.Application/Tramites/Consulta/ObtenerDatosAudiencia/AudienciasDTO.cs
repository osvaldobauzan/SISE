using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerDatosAudiencia;
public class AudienciasDTO
{
    public string Expediente { get; set; }
    public DateTime FechaAudiencia { get; set; }
    public long IdAgenda { get; set; }
    public int TipoAudienciaId { get; set; }
    public bool TieneResultado { get; set; }
    public string Resultado { get; set; }
    public string FechaAudienciaCadena
    {
        get
        {
            return FechaAudiencia.ToString("dd/MM/yyyy HH:ss") + " - " + Resultado;
        }
    }
}
