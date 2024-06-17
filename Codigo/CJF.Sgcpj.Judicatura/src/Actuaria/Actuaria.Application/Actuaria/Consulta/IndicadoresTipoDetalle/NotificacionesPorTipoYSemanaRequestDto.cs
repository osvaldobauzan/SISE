using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.IndicadoresTipoDetalle
{
    public class NotificacionesPorTipoYSemanaRequestDto : IRequest<NotificacionesPorTipoYSemanaResponseDto>
    {
        public int CatOrganismoId { get; set; }
        public long FiltroActuarioId { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public int MesSeleccionado { get; set; }
    }
}