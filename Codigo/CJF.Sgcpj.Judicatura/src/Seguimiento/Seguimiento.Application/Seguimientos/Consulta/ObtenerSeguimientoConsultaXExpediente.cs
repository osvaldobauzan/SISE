using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Repositories;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Seguimientos.Consulta;
public record class ObtenerSeguimientoConsultaXExpediente : IRequest<List<Common.Models.Seguimiento>>
{
    public Common.Models.Seguimiento seguimiento { get; set; }
}

public class ObtieneSeguimientoXExpHandler : IRequestHandler<ObtenerSeguimientoConsultaXExpediente, List<Common.Models.Seguimiento>>
{
    private readonly ISeguimientoRepository _seguimientoRepository;
    private readonly ISesionService _sesionService;

    public ObtieneSeguimientoXExpHandler(ISeguimientoRepository seguimientoRepository, ISesionService sesionService)
    
    {
        _seguimientoRepository = seguimientoRepository;
        _sesionService = sesionService;
    }

    public async Task<List<Common.Models.Seguimiento>> Handle(ObtenerSeguimientoConsultaXExpediente seguimiento, CancellationToken cancellationToken)
    {
        seguimiento.seguimiento.CatOrganismoId = _sesionService.SesionActual.CatOrganismoId;
        return await _seguimientoRepository.getBusca(seguimiento.seguimiento);
    }

}

