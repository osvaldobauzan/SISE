using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.IndicadoresTipo
{
    public class NotificacionesPorTipoYMesRequestDto : IRequest<NotificacionesPorTipoYMesResponseDto>
    {
        public int CatOrganismoId { get; set; }
        public long FiltroActuarioId { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
    }
}
