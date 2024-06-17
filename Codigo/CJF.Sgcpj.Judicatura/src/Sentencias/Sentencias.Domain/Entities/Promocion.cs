namespace CJF.Sgcpj.Judicatura.Sentencias.Domain.Entities;
public class Promocion
{
    public int NumeroRegistro { get; set; }
    public DateTime FechaPresentacion { get; set; }
    public string? Promovente { get; set; }
    public string Contenido { get; set; }
    public int NumeroOrden { get; set; }
    public int ClasePromovente { get; set; }
    public int TipoPromovente { get; set; }
    public int SintesisOrden { get; set; }
    public DateTime FechaAcuerdo { get; set; }
    public string? PromocionSeleccionada { get; set; }
    public int EstadoPromocion { get; set; }
    public int YearPromocion { get; set; }
    public int TipoCuaderno { get; set; }
}
