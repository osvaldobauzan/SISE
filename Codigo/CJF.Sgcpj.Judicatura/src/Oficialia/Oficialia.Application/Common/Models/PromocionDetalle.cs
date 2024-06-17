namespace CJF.Sgcpj.Judicatura.Application.Common.Models;

public class PromocionDetalle
{
    public int CatOrganismoId { get; set; }
    public int YearPromocion { get; set; }
    public int NumeroOrden { get; set; }
    public long AsuntoNeunId { get; set; }
    public int AsuntoId { get; set; }
    public string AsuntoAlias { get; set; }
    public int SintesisOrden { get; set; }
    public int UsuidFESE { get; set; }
    public int OrigenPromocion { get; set; }
    public int TipoCuaderno { get; set; }
    public string NombreTipoCuaderno { get; set; }
    public int NumeroRegistro { get; set; }
    public DateTime FechaPresentacion { get; set; }
    public string HoraPresentacion { get; set; }
    public int ClasePromocion { get; set; }
    public string NombreClase { get; set; }
    public int ClasePromovente { get; set; }
    public int TipoPromovente { get; set; }
    public string NombreParte { get; set; }
    public int TipoContenido { get; set; }
    public string Contenido { get; set; }
    public int NumeroCopias { get; set; }
    public int NumeroAnexos { get; set; }
    public DateTime FechaEntrega { get; set; }
    public int PersonaRecibe { get; set; }
    public int Secretario { get; set; }
    public DateTime FechaAlta { get; set; }
    public int RegistroEmpleadoId { get; set; }
    public int StatusReg { get; set; }
    public DateTime FechaAcuerdo { get; set; }
    public int PromocionVisible { get; set; }
    public int PromocionAutorizaVisible { get; set; }
    public int EstadoPromocion { get; set; }
    public string ObservacionesArchivo { get; set; }
    public string FechaRecepcionDocumento { get; set; }
    public string HoraRecepcionDocumento { get; set; }
    public int ClaseAnexo { get; set; }
    public int DescripcionAnexo { get; set; }
    public int CaracterAnexo { get; set; }
    public int Consecutivo { get; set; }
    public int EstatusArchivo { get; set; }
    public int NumeroTomos { get; set; }
    public string NumeroFojasTomos { get; set; }
    public string Observaciones { get; set; }
    public bool Audiencia { get; set; }
    public int AmbosCuadernos { get; set; }
    public string Mesa { get; set; }
    public int ArchivoInmodificable { get; set; }
}
