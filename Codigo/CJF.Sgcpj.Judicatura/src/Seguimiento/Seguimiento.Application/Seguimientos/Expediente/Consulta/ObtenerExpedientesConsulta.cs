using AutoMapper;
using MediatR;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Repositories;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Seguimientos.Expediente.Consulta;
public record class ObtenerExpedientesConsulta : IRequest<List<ExpedienteObtener>>
{
    public ExpedienteObtener expediente { get; set; }
}


public class ObtieneExpedientesHandler : IRequestHandler<ObtenerExpedientesConsulta, List<ExpedienteObtener>>
{
    private readonly IExpedienteRepository _expedienteRepository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;

    public ObtieneExpedientesHandler(IExpedienteRepository expedienteRepository, IMapper mapper, ISesionService sesionService)
    {
        _expedienteRepository = expedienteRepository;
        _mapper = mapper;
        _sesionService = sesionService;
    }

    public async Task<List<ExpedienteObtener>> Handle(ObtenerExpedientesConsulta request, CancellationToken cancellationToken)
    {

        var Result = await _expedienteRepository.getExpedientes(request.expediente);
        return Result;
    }

}

