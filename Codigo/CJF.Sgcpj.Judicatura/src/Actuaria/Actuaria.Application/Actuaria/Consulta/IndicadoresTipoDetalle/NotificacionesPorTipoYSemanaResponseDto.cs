namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.IndicadoresTipoDetalle;
public class NotificacionesPorTipoYSemanaResponseDto
{
    public List<NotificacionesPorTipoYSemana> Notificaciones { get; set; }
}

public class NotificacionesPorTipoYSemana
{
    public int Semana { get; set; }
    public string Tipo { get; set; }
    public int Total { get; set; }
}