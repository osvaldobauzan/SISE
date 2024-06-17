using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Repositories;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Comandos.EliminarAcuerdoComando;
public class EliminarAcuerdoComando : IRequest<bool>
{
    public EliminarAcuerdoDto Acuerdo { get; set; }
}
public class EliminarAcuerdoComandoHandler : IRequestHandler<EliminarAcuerdoComando, bool>
{
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;
    private readonly ITramitesRepository _tramitesRepository;
    public EliminarAcuerdoComandoHandler(IMapper mapper, ITramitesRepository tramitesRepository, ISesionService sesionService)
    {
        _mapper = mapper;
        _tramitesRepository = tramitesRepository;
        _sesionService = sesionService;
    }
    public async Task<bool> Handle(EliminarAcuerdoComando request, CancellationToken cancellationToken)
    {
        request.Acuerdo.catIdOrganismo = _sesionService.SesionActual.CatOrganismoId;
        var acuerdo = _mapper.Map<EliminarAcuerdo>(request.Acuerdo);

        return await _tramitesRepository.EliminarAcuerdo(acuerdo);
    }
}