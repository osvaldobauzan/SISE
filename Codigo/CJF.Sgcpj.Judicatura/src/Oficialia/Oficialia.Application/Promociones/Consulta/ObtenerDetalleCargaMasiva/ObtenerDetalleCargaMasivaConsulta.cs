using AutoMapper;
using CJF.Sgcpj.Judicatura.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Oficialia.Application.Promociones.Consulta.ObtenerDetalleCargaMasiva;
public class ObtenerDetalleCargaMasivaConsulta : IRequestHandler<ObtenerDetalleCargaMasivaRequest, ObtenerDetalleAlertaDto>
{
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;
    private readonly IPromocionesRepository _promocionesRepository;
    public ObtenerDetalleCargaMasivaConsulta(IMapper mapper, IPromocionesRepository promocionesRepository, ISesionService sesionService)
    {
        _mapper = mapper;
        _promocionesRepository = promocionesRepository;
        _sesionService = sesionService;
    }

    async Task<ObtenerDetalleAlertaDto> IRequestHandler<ObtenerDetalleCargaMasivaRequest, ObtenerDetalleAlertaDto>.Handle(ObtenerDetalleCargaMasivaRequest request, CancellationToken cancellationToken)
    {
        request.CatOrganismoId = _sesionService.SesionActual.CatOrganismoId;

        var detalleAlerta = _mapper.Map<ObtenerDetalleCargaMasivaRequest>(request);
        var datosAlerta = await _promocionesRepository.ObtenerDetalleAlerta(detalleAlerta);

        return await Task.FromResult(datosAlerta);
    }
}
