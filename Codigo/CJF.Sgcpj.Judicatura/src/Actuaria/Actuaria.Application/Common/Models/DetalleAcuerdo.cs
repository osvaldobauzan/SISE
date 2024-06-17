namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
public class DetalleAcuerdo
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
