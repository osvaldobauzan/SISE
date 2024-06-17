using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.Actuario;
public class ActuarioNotificacionesDto : IMapFrom<Common.Models.Actuario.ActuarioNotificaciones>
{
    public long AsuntoNeunId { get; set; }
    public string No_Exp { get; set; }
    public string TipoAsunto { get; set; }
    public int TipoCuaderno { get; set; }
    public int AsuntoDocumentoId { get; set; }
    public int SintesisOrden { get; set; }
    public string? DocumentoAcuerdo { get; set; }
    public int TipoParte { get; set; }
    public string Parte { get; set; }
    public long ParteId { get; set; }
    public string DomicilioParte { get; set; }
    public string Caracter { get; set; }
    public int EstadoId { get; set; }
    public int TipoDeAcuse { get; set; }
    public string Estado { get; set; }
    public DateTime? EstadoFecha { get; set; }
    public string Tipo { get; set; }
    public int? TipoId { get; set; }
    public string AsignadoActuario { get; set; }
    public string AsignadoZona { get; set; }
    public string archivoAcuse { get; set; }
    public long NotElecId { get; set; }
    public int Folio { get; set; }
    public string? NombreArchivo { get; set; }
    public Guid? Guid { get; set; }
    public string? TipoCuadernoDesc { get; set; }
    public DateTime FechaAutoriza { get; set; }
    public string FechaAuto_F { get; set; }
    public int Transcurrido { get; set; }
}
