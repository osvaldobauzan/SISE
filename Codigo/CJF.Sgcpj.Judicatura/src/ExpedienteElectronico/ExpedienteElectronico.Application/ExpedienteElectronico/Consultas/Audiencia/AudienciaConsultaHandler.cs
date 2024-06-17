using AutoMapper;
using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.Common.Repositories;
using MediatR;

namespace CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.Audiencia;
public class AudienciaConsultaHandler : IRequestHandler<AudienciaConsulta, AudienciaDto>
{
    private readonly IExpedienteElectronicoRepository _repository;
    private readonly IMapper _mapper;

    public AudienciaConsultaHandler(IExpedienteElectronicoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<AudienciaDto> Handle(AudienciaConsulta request, CancellationToken cancellationToken)
    {
        var datos = await _repository.ObtenerDetalleAudiencia(request);
        var audiencia = _mapper.Map<AudienciaDto>(datos);
        return audiencia;

    }
}
