using CJF.Sgcpj.Judicatura.Actuaria.Domain.Entities;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.IndicadoresActuaria;
public class ObtenerNotificacionesResponseDto
{
    public List<NotificacionesPendientesPorDias> NotificacionesPendientesPorDias { get; set; }
    public TotalNotificaciones TotalNotificaciones { get; set; }
    public List<NotificacionesPorTipo> NotificacionesPorTipo { get; set; }
}
