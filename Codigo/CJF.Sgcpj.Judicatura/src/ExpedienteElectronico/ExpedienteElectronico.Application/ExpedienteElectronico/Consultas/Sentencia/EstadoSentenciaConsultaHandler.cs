using AutoMapper;
using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.Common.Repositories;
using MediatR;

namespace CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.Sentencia;

public class EstadoSentenciaConsultaHandler : IRequestHandler<EstadoSentenciaConsulta, EstadoSentenciaDto>
{
    private readonly IExpedienteElectronicoRepository _repository;
    private readonly IMapper _mapper;

    public EstadoSentenciaConsultaHandler(IExpedienteElectronicoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<EstadoSentenciaDto> Handle(EstadoSentenciaConsulta request, CancellationToken cancellationToken)
    {
        var datos = await _repository.ObtenerEstadoSentencia(request);
        var estadoSentencias = _mapper.Map<EstadoSentenciaDto>(datos);
        return estadoSentencias;
    }
}
