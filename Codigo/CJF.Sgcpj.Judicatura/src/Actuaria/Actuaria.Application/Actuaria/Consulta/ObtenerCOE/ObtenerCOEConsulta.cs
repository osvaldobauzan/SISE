using AutoMapper;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerCOE;
public class ObtenerCOEConsulta : IRequestHandler<ObtenerCOERequest, ObtenerCOEDto>
{
    private readonly IMapper _mapper;
    private readonly IActuariaRepository _repository;

    public ObtenerCOEConsulta(IMapper mapper, IActuariaRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    async Task<ObtenerCOEDto> IRequestHandler<ObtenerCOERequest, ObtenerCOEDto>.Handle(ObtenerCOERequest request, CancellationToken cancellationToken)
    {
        var consultaCOE = await _repository.ConsultaCOE(request.NotificacionElectronicaId);

        var response = _mapper.Map<ObtenerCOEDto>(consultaCOE);

        return await Task.FromResult(response);
    }
}