namespace CJF.Sgcpj.Judicatura.Application.Common.Models;
public class TableroPromocionItem
{
    public int NumeroOrden { get; set; }
    public int YearPromocion { get; set; }
    public int OrigenPromocion { get; set; }
    public string OrigenPromocionDescripcion { get; set; }
    public int NumeroRegistro { get; set; }
    public DateTime FechaPresentacion { get; set; }
    public DateTime FechaPresentacionFin { get; set; }
    public string Mesa { get; set; }
    public int ClasePromocion { get; set; }
    public string ClasePromocionDescripcion { get; set; }
    public int Copias { get; set; }
    public int Anexos { get; set; }
    public string Color { get; set; }
    public int CuadernoId { get; set; }
    public string CuadernoNombreCorto { get; set; }
    public string CuadernoNombre { get; set; }
    public string SecretarioDescripcion { get; set; }
    public string SecretarioUserName { get; set; }
    public string SecretarioNombres { get; set; }
    public string SecretarioPaterno { get; set; }
    public string SecretarioMaterno { get; set; }
    public string TipoContenidoDescripcion { get; set; }
    public int TipoContenidoId { get; set; }
    public string ParteDescripcion { get; set; }
    public int TipoPromovente { get; set; }
    public int ClasePromovente { get; set; }
    public string ClasePromoventeDescripcion { get; set; }
    public bool EsDemandaElectronica { get; set; }
    public bool EsPromocionElectronica { get; set; }
    public bool EsPromocionElectronicaIOJ { get; set; }
    public bool EsPromocionElectronicaICOIJ { get; set; }
    public bool EsDemandaElectronicaICOIJ { get; set; }
    public bool CambioDemandaPromocion { get; set; }
    public bool ConAcuerdo { get; set; }
    public int Estado { get; set; }
}
