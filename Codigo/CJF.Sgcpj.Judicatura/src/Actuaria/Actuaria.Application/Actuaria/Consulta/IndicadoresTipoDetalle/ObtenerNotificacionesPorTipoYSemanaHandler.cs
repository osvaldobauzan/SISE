using AutoMapper;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.IndicadoresTipoDetalle;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.IndicadoresTipoDetalle
{
    public class ObtenerNotificacionesPorTipoYSemanaHandler : IRequestHandler<NotificacionesPorTipoYSemanaRequestDto, NotificacionesPorTipoYSemanaResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IActuariaRepository _notificacionesRepository;

        public ObtenerNotificacionesPorTipoYSemanaHandler(IActuariaRepository notificacionesRepository, IMapper mapper)
        {
            _notificacionesRepository = notificacionesRepository;
            _mapper = mapper;
        }

        public async Task<NotificacionesPorTipoYSemanaResponseDto> Handle(NotificacionesPorTipoYSemanaRequestDto request, CancellationToken cancellationToken)
        {
            var notificacionesData = await _notificacionesRepository.ObtenerNotificacionesPorTipoYSemana(
                request.CatOrganismoId, request.FiltroActuarioId, request.FechaInicial, request.FechaFinal, request.MesSeleccionado);

            var notificaciones = notificacionesData.Select(x => new NotificacionesPorTipoYSemana
            {
                Semana = x.Semana,
                Tipo = x.Tipo,
                Total = x.Total
            }).ToList();

            return new NotificacionesPorTipoYSemanaResponseDto
            {
                Notificaciones = notificaciones
            };
        }
    }
}
