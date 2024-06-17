using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.DetalleNotificaciones;
public class NotificacionDetalleDto : IMapFrom<Common.Models.NotificacionDetalle>
{
    public string Parte { get; set; }
    public int TipoParte { get; set; }
    public int ParteId { get; set; }
    public long PromoventeId { get; set; }
    public string Caracter { get; set; }
    public int EstadoId { get; set; }
    public string Estado { get; set; }
    public DateTime EstadoFecha { get; set; }
    public string Tipo { get; set; }
    public int? TipoId { get; set; }
    public string AsignoPersona { get; set; }
    public string AsignoFecha { get; set; }
    public string AsignadoActuario { get; set; }
    public string AsignadoZona { get; set; }
    public string ArchivoAcuse { get; set; }
    public string NumeroOficio { get; set; }
    public int? notiElect { get; set; }
    public string? usuarioRegistro { get; set; }
    public string DomicilioParte { get; set; }
    public int TieneCOE { get; set; }
    public long NotElecId { get; set; }
    public long AsuntoNEUNCOE { get; set; }
    public int TipoComunicacionCOE { get; set; }
    public int Folio { get; set; }
    public string? NombreArchivo { get; set; }
    public Guid? Guid { get; set; }
}

