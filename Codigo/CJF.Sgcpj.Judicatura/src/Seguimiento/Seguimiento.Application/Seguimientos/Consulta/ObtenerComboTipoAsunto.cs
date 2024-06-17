using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Repositories;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Seguimientos.Consulta;
public record class ObtenerComboTipoAsunto : IRequest<List<Common.Models.Seguimiento>>
{
    public Common.Models.Seguimiento seguimiento { get; set; }
}

public class ObtieneComboTipoAsuntoHandler : IRequestHandler<ObtenerComboTipoAsunto, List<Common.Models.Seguimiento>>
{
    private readonly ISeguimientoRepository _seguimientoRepository;
    private readonly ISesionService _sesionService;

    public ObtieneComboTipoAsuntoHandler(ISeguimientoRepository seguimientoRepository, ISesionService sesionService)
    {
        _seguimientoRepository = seguimientoRepository;
        _sesionService = sesionService;
    }

    public async Task<List<Common.Models.Seguimiento>> Handle(ObtenerComboTipoAsunto seguimiento, CancellationToken cancellationToken)
    {
        seguimiento.seguimiento.CatOrganismoId = _sesionService.SesionActual.CatOrganismoId;
        return await _seguimientoRepository.getTipoAsuntos(seguimiento.seguimiento);
    }
}

