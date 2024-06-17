using AutoMapper;
using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.Common.Repositories;
using MediatR;

namespace ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.InformacionParte;
public class InformacionParteConsultaHandler : IRequestHandler<InformacionParteConsulta, List<InformacionParteDto>>
{
    private readonly IExpedienteElectronicoRepository _repository;
    private readonly IMapper _mapper;

    public InformacionParteConsultaHandler(IExpedienteElectronicoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<List<InformacionParteDto>> Handle(InformacionParteConsulta request, CancellationToken cancellationToken)
    {
        var datos = await _repository.ObtenerInformacionParte(request);
        var informacionParte = _mapper.Map<List<InformacionParteDto>>(datos);
        return informacionParte;
    }
}
