
namespace CJF.Sgcpj.Judicatura.Actuaria.Domain.Entities;
public class ActuarioDetalleLista

{
    /// <summary>
    /// Lista de tipos individual
    /// </summary>
   public List<NotificacionesPorTipo> ListaTipos;

    /// <summary>
    /// Totales individuales
    /// </summary>
    public TotalNotificaciones TotalNotificaciones;

    /// <summary>
    /// Información propia del actuario para poder mostrar
    /// </summary>
    public Actuario Actuario;
}
