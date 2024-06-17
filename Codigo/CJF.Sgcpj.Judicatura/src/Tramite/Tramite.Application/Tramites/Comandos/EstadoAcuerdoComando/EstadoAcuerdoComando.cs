using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Repositories;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Comandos.EstadoAcuerdoComando;
public class EstadoAcuerdoComando : IRequest<bool>
{
    public EstadoAcuerdoDto Acuerdo { get; set; }
}
public class EstadoAcuerdoComandoHandler : IRequestHandler<EstadoAcuerdoComando, bool>
{
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;
    private readonly ITramitesRepository _tramitesRepository;
    public EstadoAcuerdoComandoHandler(IMapper mapper, ITramitesRepository tramitesRepository, ISesionService sesionService)
    {
        _mapper = mapper;
        _tramitesRepository = tramitesRepository;
        _sesionService = sesionService;
    }
    public async Task<bool> Handle(EstadoAcuerdoComando request, CancellationToken cancellationToken)
    {
        var acuerdo = _mapper.Map<EstadoAcuerdo>(request.Acuerdo);
        acuerdo.Valor = _sesionService.SesionActual.EmpleadoId;
        return await _tramitesRepository.ActualizaEstadoAcuerdo(acuerdo);
    }
}
