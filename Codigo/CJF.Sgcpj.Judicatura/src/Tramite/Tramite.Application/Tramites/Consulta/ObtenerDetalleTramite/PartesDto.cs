using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerDetalleTramite;

public class PartesDto : IMapFrom<Partes>
{
    public int? AsuntoId { get; set; }
    public long? AsuntoNeunId { get; set; }
    public int? SintesisOrden { get; set; }
    public int? PersonaId { get; set; }
    public int? PromoventeId { get; set; }
    public long? NotElecId { get; set; }
    public short? NotificacionElectronicaJL { get; set; }
    public int? TipoNotificacion { get; set; }
    public string? Texto { get; set; }
}
