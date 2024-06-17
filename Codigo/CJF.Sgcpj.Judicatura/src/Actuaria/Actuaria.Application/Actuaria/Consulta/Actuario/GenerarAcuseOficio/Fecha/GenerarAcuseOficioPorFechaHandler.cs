using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.Actuario.GenerarAcuseOficio.Actuario;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.Actuario.GenerarAcuseOficio.Fecha;
public class GenerarAcuseOficioPorFechaHandler : IRequestHandler<GenerarAcuseOficioPorFechaRequestDto, GenerarAcuseOficioPorFechaResponseDto>
{
    private readonly ISesionService _sesionService;
    private readonly IGenerarAcuseOficioPorFechaService _generarAcuseOficioPorFechaService;

    public GenerarAcuseOficioPorFechaHandler(ISesionService sesionService, IGenerarAcuseOficioPorFechaService generarAcuseOficioPorFechaService)
    {
        _sesionService = sesionService;
        _generarAcuseOficioPorFechaService = generarAcuseOficioPorFechaService;
    }

    public async Task<GenerarAcuseOficioPorFechaResponseDto> Handle(GenerarAcuseOficioPorFechaRequestDto request, CancellationToken cancellationToken)
    {
        request.CatOrganismoId = _sesionService.SesionActual.CatOrganismoId;
        return await _generarAcuseOficioPorFechaService.GenerarAcuseOficioCodigoQr(request);
    }
}
