namespace CJF.Sgcpj.Judicatura.Actuaria.Domain.Entities;
public class ActuarioNotificacion
{
    public string No_Exp { get; set; }
    public int TipoParte { get; set; }
    public string Parte { get; set; }
    public long ParteId { get; set; }
    public string DomicilioParte { get; set; }
    public string Caracter { get; set; }
    public int EstadoId { get; set; }
    public int TipoDeAcuse { get; set; }
    public string Estado { get; set; }
    public DateTime EstadoFecha { get; set; }
    public string Tipo { get; set; }
    public int TipoId { get; set; }
    public string AsignadoActuario { get; set; }
    public string AsignadoZona { get; set; }
    public string archivoAcuse { get; set; }
    public long NotElecId { get; set; }
    public int Folio { get; set; }
    public string NombreArchivo { get; set; }
    public Guid Guid { get; set; }
}
