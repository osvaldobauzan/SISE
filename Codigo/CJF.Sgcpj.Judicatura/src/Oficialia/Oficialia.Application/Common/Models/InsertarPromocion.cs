namespace CJF.Sgcpj.Judicatura.Application.Common.Models;
public class InsertarPromocion
{
    public long AsuntoNeunId { get; set; }
    public int TipoCuaderno { get; set; }
    public DateTime FechaPresentacion { get; set; }
    public string HoraPresentacion { get; set; }
    public int? ClasePromocion { get; set; }
    public int? ClasePromovente { get; set; }
    public int TipoPromovente { get; set; }
    public int TipoContenido { get; set; }
    public int NumeroCopias { get; set; }
    public int NumeroAnexo { get; set; }
    public int Secretario { get; set; }
    public int RegistroEmpleadoId { get; set; }
    public string Observaciones { get; set; }
    public string IpUsuario { get; set; }
    public int? OrigenPromocion { get; set; }
    public int? NumeroRegistro { get; set; }
    public int? NumeroOrden { get; set; }
    public int? Fojas { get; set; }
    public int? Origen { get; set; }
    public long? Folio { get; set; }
}
