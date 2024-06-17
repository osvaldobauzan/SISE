using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.Actuario.GenerarAcuseOficio.Actuario;
public class GenerarAcuseOficioHandler : IRequestHandler<GenerarAcuseOficioRequestDto, GenerarAcuseOficioResponseDto>
{
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;
    private readonly IGenerarAcuseOficioService _acuseOficioService;

    public GenerarAcuseOficioHandler(IMapper mapper, ISesionService sesionService, IGenerarAcuseOficioService acuseOficioService)
    {
        _mapper = mapper;
        _sesionService = sesionService;
        _acuseOficioService = acuseOficioService;
    }

    public async Task<GenerarAcuseOficioResponseDto> Handle(GenerarAcuseOficioRequestDto request, CancellationToken cancellationToken)
    {
        request.CatOrganismoId = _sesionService.SesionActual.CatOrganismoId;
        return await _acuseOficioService.GenerarAcuseOficioCodigoQr(request);
    }
}
