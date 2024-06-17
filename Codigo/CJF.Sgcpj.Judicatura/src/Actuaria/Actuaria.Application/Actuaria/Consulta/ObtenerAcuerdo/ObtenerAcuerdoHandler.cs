using System.Linq;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;
using Microsoft.Extensions.Azure;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerAcuerdo;
public class ObtenerAcuerdoHandler : IRequestHandler<ObtenerAcuerdoRequest, List<ObtenerAcuerdoM>>
{
    private readonly IMapper _mapper;
    private readonly IActuariaRepository _actuariaRepository;
    private readonly ISesionService _sesionService;
    
    public ObtenerAcuerdoHandler(IMapper mapper, ISesionService sesionService, IActuariaRepository actuariaRepository)
    {
        _mapper = mapper;
        _actuariaRepository = actuariaRepository;
        _sesionService = sesionService;
    }

    async Task <List<ObtenerAcuerdoM>> IRequestHandler<ObtenerAcuerdoRequest, List<ObtenerAcuerdoM>>.Handle(ObtenerAcuerdoRequest request, CancellationToken cancellationToken)
    {
        var results = await _actuariaRepository.ObtenerAcuerdos(_sesionService.SesionActual.CatOrganismoId, Convert.ToDateTime(request.FechaInicial), Convert.ToDateTime(request.FechaFinal));
        return results; 
    }
}
