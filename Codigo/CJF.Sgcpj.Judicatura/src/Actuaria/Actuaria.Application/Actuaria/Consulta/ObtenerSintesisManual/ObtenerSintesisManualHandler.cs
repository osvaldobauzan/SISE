using AutoMapper;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerPersonaAsunto;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerSintesisManual;
public class ObtenerSintesisManualHandler : IRequestHandler<ObtenerSintesisManualRequest, List<ObtenerSintesisManualDTO>>
{
    private readonly IMapper _mapper;
    private readonly IActuariaRepository _actuariaRepository;
    private readonly ISesionService _sesionService;
    private readonly IApplicationDbContext _applicationDbContext;

    public ObtenerSintesisManualHandler(IMapper mapper, ISesionService sesionService, IActuariaRepository actuariaRepository, IApplicationDbContext applicationDbContext)
    {
        _mapper = mapper;
        _actuariaRepository = actuariaRepository;
        _sesionService = sesionService;
        _applicationDbContext = applicationDbContext;

    }

    async Task<List<ObtenerSintesisManualDTO>> IRequestHandler<ObtenerSintesisManualRequest, List<ObtenerSintesisManualDTO>>.Handle(ObtenerSintesisManualRequest request, CancellationToken cancellationToken)
    {
       var results = await  _actuariaRepository.ObtenerSintesisManual(Convert.ToDateTime(request.FechaPublicacion), _sesionService.SesionActual.CatOrganismoId);

        foreach (var item in results)
        {
            item.Quejoso = await _actuariaRepository.ObtenerPersonaAsuntoXidEmpleado(item.AsuntoNeunId, item.Parte1);
            item.Autoridad = await _actuariaRepository.ObtenerPersonaAsuntoXidEmpleado(item.AsuntoNeunId, item.Parte2);
        }
        return results;
    }
}
