using AutoMapper;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerPersonaAsunto;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.IndicadoresActuaria;
public class ObtenerIndicadoresZonasHandler : IRequestHandler<ObtenerNotificacionesZonasRequestDto, ObtenerNotificacionesZonasResponseDto>
{
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;
    private readonly IActuariaRepository _notificacionesRepository;

    public ObtenerIndicadoresZonasHandler(IActuariaRepository notificacionesRepository, IMapper mapper, ISesionService sesionService)
    {
        _notificacionesRepository = notificacionesRepository;
        _mapper = mapper;
        _sesionService = sesionService;
    }

    public async Task<ObtenerNotificacionesZonasResponseDto> Handle(ObtenerNotificacionesZonasRequestDto request, CancellationToken cancellationToken)
    {
        var listaDetalleResponse = await _notificacionesRepository.ObtenerDetalleConteos(
            request.CatOrganismoId, request.FechaInicial, request.FechaFinal);

        return new ObtenerNotificacionesZonasResponseDto() 
        {
            DetalleActuarios = listaDetalleResponse
        };
    }
}