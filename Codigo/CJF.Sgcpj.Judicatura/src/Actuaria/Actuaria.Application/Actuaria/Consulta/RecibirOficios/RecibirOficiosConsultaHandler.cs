
using System.Collections.Generic;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.RecibirOficios;
public class RecibirOficiosConsultaHandler : IRequestHandler<RecibirOficiosConsulta, List<RecibirOficiosDto>>
{
    private readonly IActuariaRepository _actuariaRepository;
    private readonly ISesionService _sesionService;
    private readonly IMapper _mapper;

    public RecibirOficiosConsultaHandler(IActuariaRepository actuariaRepository, ISesionService sesionService, IMapper mapper)
    {
        _actuariaRepository = actuariaRepository;
        _sesionService = sesionService;
        _mapper = mapper;
    }
    public async Task<List<RecibirOficiosDto>> Handle(RecibirOficiosConsulta request, CancellationToken cancellationToken)
    {
        List <RecibirOficiosM> oficios = await _actuariaRepository.ObtenerOficiosParaRecibir(_sesionService.SesionActual.CatOrganismoId, request.Folio, _sesionService.SesionActual.EmpleadoId);
        var resultado = _mapper.Map<List<RecibirOficiosDto>>(oficios);
        return resultado;
    }
}
