
using AutoMapper;
using MediatR;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Repositories;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Seguimientos.Expediente.Consulta;
public record class ObtenerExpedienteConsulta : IRequest<ExpedienteObtener>
{
    public ExpedienteObtener expediente { get; set; }
}


public class ObtieneExpedienteHandler : IRequestHandler<ObtenerExpedienteConsulta, ExpedienteObtener>
{
    private readonly IExpedienteRepository _expedienteRepository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;

    public ObtieneExpedienteHandler(IExpedienteRepository expedienteRepository, IMapper mapper, ISesionService sesionService)
    {
        _expedienteRepository = expedienteRepository;
        _mapper = mapper;
        _sesionService = sesionService;
    }

    public async Task<ExpedienteObtener> Handle(ObtenerExpedienteConsulta request, CancellationToken cancellationToken)
    {

        var Result =await _expedienteRepository.getExpediente(request.expediente);
        return Result;
    }

}

