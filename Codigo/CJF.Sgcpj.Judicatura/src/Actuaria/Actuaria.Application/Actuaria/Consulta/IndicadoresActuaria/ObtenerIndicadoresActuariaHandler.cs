using AutoMapper;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerPersonaAsunto;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.IndicadoresActuaria;
public class ObtenerIndicadoresActuariaHandler : IRequestHandler<ObtenerNotificacionesRequestDto, ObtenerNotificacionesResponseDto>
{
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;
    private readonly IActuariaRepository _notificacionesRepository;

    public ObtenerIndicadoresActuariaHandler(IActuariaRepository notificacionesRepository, IMapper mapper, ISesionService sesionService)
    {
        _notificacionesRepository = notificacionesRepository;
        _mapper = mapper;
        _sesionService = sesionService;
    }

    public async Task<ObtenerNotificacionesResponseDto> Handle(ObtenerNotificacionesRequestDto request, CancellationToken cancellationToken)
    {
        var (notificacionesPendientesPorDias, totalNotificaciones, notificacionesPorTipo) = await _notificacionesRepository.ObtenerNotificacionesPorTipoYPeriodo(
            request.CatOrganismoId, request.FiltroActuarioId, request.FechaInicial, request.FechaFinal);

        return new ObtenerNotificacionesResponseDto
        {
            NotificacionesPendientesPorDias = notificacionesPendientesPorDias,
            TotalNotificaciones = totalNotificaciones,
            NotificacionesPorTipo = notificacionesPorTipo
        };
    }
}