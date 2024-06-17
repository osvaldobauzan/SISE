using AutoMapper;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Actuaria.Domain.Entities;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.IndicadoresTipo
{
    public class ObtenerNotificacionesPorTipoYMesHandler : IRequestHandler<NotificacionesPorTipoYMesRequestDto, NotificacionesPorTipoYMesResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IActuariaRepository _notificacionesRepository;

        public ObtenerNotificacionesPorTipoYMesHandler(IActuariaRepository notificacionesRepository, IMapper mapper)
        {
            _notificacionesRepository = notificacionesRepository;
            _mapper = mapper;
        }

        public async Task<NotificacionesPorTipoYMesResponseDto> Handle(NotificacionesPorTipoYMesRequestDto request, CancellationToken cancellationToken)
        {
            var notificacionesEntidad = await _notificacionesRepository.ObtenerNotificacionesPorTipoYMes(
                request.CatOrganismoId, request.FiltroActuarioId, request.FechaInicial, request.FechaFinal);

            var notificaciones = notificacionesEntidad.Select(e => new NotificacionesPorTipoYMes
            {
                Mes = e.Mes,
                Tipo = e.Tipo,
                Total = e.Total,
                NumeroMes = e.NumeroMes
            }).ToList();

            return new NotificacionesPorTipoYMesResponseDto
            {
                Notificaciones = notificaciones
            };
        }
    }
}
