namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.IndicadoresTipo
{
    public class NotificacionesPorTipoYMesResponseDto
    {
        public List<NotificacionesPorTipoYMes> Notificaciones { get; set; }
    }

    public class NotificacionesPorTipoYMes
    {
        public string Mes { get; set; }
        public int NumeroMes { get; set; }
        public string Tipo { get; set; }
        public int Total { get; set; }
    }
}
