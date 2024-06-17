using AutoMapper;
using Azure;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.DiferenciasTiempos;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerPersonaAsunto;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.DiferenciasNotificaciones;
public class ObtenerDiferenciasNotificacionHandler : IRequestHandler<ObtenerDiferenciasRequestDto, ObtenerDiferenciasResponseDto>
{
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;
    private readonly IActuariaRepository _notificacionesRepository;

    public ObtenerDiferenciasNotificacionHandler(IActuariaRepository notificacionesRepository, IMapper mapper, ISesionService sesionService)
    {
        _notificacionesRepository = notificacionesRepository;
        _mapper = mapper;
        _sesionService = sesionService;
    }

    public async Task<ObtenerDiferenciasResponseDto> Handle(ObtenerDiferenciasRequestDto request, CancellationToken cancellationToken)
    {
        var intervalosTiempos = await _notificacionesRepository.ObtenerDetalleIntervalosTiempos(
            request.EmpleadoId, request.FechaInicial, request.FechaFinal);

        return new ObtenerDiferenciasResponseDto
        {
            Diferencias = intervalosTiempos
        };
    }
}