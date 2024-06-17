using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.DetalleNotificaciones;
public class NotificacionDetalleMetaDataEstadosDto : IMapFrom<NotificacionDetalleMetaDataEstados>
{
    public int VerTodo { get; set; }
    public int Pendiente { get; set; }
    public int EnProceso { get; set; }
    public int Notificados { get; set; }


    public DatosAsuntoDto DatosAsunto { get; set; }
}
