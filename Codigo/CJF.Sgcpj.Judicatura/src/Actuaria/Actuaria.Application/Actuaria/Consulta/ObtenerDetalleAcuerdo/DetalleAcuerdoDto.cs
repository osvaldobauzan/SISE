using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerDetalleAcuerdo;
public class DetalleAcuerdoDto : IMapFrom<DetalleAcuerdo>
{
    public string Expediente { get; set; }
    public string TipoAsuntoDescripcion { get; set; }
    public string NombreTipoCuaderno { get; set; }
    public int TipoCuaderno { get; set; }
    public string ContenidoAcuerdo { get; set; }
    public DateTime FechaRecepcionAcuerdo { get; set; }
    public int DiasTranscurridos { get; set; }
    public string Sintesis { get; set; }
}
