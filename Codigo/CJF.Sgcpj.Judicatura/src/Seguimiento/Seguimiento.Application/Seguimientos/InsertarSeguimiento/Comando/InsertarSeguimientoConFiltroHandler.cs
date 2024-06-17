using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Alertas;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Repositories;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Seguimientos.InsertarSeguimiento.Comando;
public class InsertarSeguimientoConFiltroHandler : IRequest<int>
{
    public Common.Models.Seguimiento seguimiento { get; set; }
}
public class EstadoSeguimientoComandoHandler : IRequestHandler<InsertarSeguimientoConFiltroHandler, int>
{
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;
    private readonly ISeguimientoRepository _seguimientoRepository;
    private readonly IAlertsMessageService _alertsMessageService;
    private readonly IMediator _mediator;
 


    public EstadoSeguimientoComandoHandler(IMapper mapper, ISeguimientoRepository seguimientoRepository, ISesionService sesionService, IAlertsMessageService alertsMessageService, IMediator mediator)
    {
        _mapper = mapper;
        _seguimientoRepository = seguimientoRepository;
        _sesionService = sesionService;
        _alertsMessageService = alertsMessageService;
        _mediator= mediator;
    }
    public async Task<int> Handle(InsertarSeguimientoConFiltroHandler request, CancellationToken cancellationToken)
    {

        request.seguimiento.CatOrganismoId = _sesionService.SesionActual.CatOrganismoId;
        request.seguimiento.EmpleadoId = _sesionService.SesionActual.EmpleadoId;

        var result = await _seguimientoRepository.InsertarSeguimientoConFiltro(request.seguimiento);

        return result;
    }
}
