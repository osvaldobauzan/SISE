using AutoMapper;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerPersonaAsunto;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.DiferenciasTiempos;
public class ObtenerDiferenciasTiemposHandler : IRequestHandler<ObtenerDiferenciasTiemposRequestDto, ObtenerDiferenciasTiemposResponseDto>
{

    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;
    private readonly IActuariaRepository _notificacionesRepository;

    public ObtenerDiferenciasTiemposHandler(IActuariaRepository notificacionesRepository, IMapper mapper, ISesionService sesionService)
    {
        _notificacionesRepository = notificacionesRepository;
        _mapper = mapper;
        _sesionService = sesionService;
    }

    public async Task<ObtenerDiferenciasTiemposResponseDto> Handle(ObtenerDiferenciasTiemposRequestDto request, CancellationToken cancellationToken)
    {
        var response = await _notificacionesRepository.ObtenerDetalleIntervalosTiempos(
          request.FiltroActuarioId, request.FechaInicial, request.FechaFinal);

        return new ObtenerDiferenciasTiemposResponseDto
        {
            Diferencias = response
        };
    }
}