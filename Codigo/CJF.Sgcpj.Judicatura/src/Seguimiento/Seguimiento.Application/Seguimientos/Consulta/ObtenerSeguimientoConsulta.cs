using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Repositories;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Seguimientos.Consulta;
public record class ObtenerSeguimientoConsulta : IRequest<List<Common.Models.Seguimiento>>
{
    public Common.Models.Seguimiento Seguimiento { get; set; }
}

public class ObtieneSeguimientoHandler : IRequestHandler<ObtenerSeguimientoConsulta, List<Common.Models.Seguimiento>>
{
    private readonly ISeguimientoRepository _seguimientoRepository;
    private readonly ISesionService _sesionService;

    public ObtieneSeguimientoHandler(ISeguimientoRepository seguimientoRepository, ISesionService sesionService)
    {
        _seguimientoRepository = seguimientoRepository;
        _sesionService = sesionService;
    }

    public async Task<List<Common.Models.Seguimiento>> Handle(ObtenerSeguimientoConsulta request, CancellationToken cancellationToken)
    {
        request.Seguimiento.CatOrganismoId = _sesionService.SesionActual.CatOrganismoId;
        return await _seguimientoRepository.getByList(request.Seguimiento);
    }
}

