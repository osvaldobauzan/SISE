using AutoMapper;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.AgregarActuarioMasivo;
public class AgregarActuarioMasivoComandoHandler : IRequestHandler<AgregarActuarioMasivoComando, bool>
{
    private readonly IActuariaRepository _repository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;

    public AgregarActuarioMasivoComandoHandler(IActuariaRepository repository, IMapper mapper, ISesionService sesionService)
    {
        _repository = repository;
        _mapper = mapper;
        _sesionService = sesionService;
    }

    public async Task<bool> Handle(AgregarActuarioMasivoComando request, CancellationToken cancellationToken)
    {
        
        var actuario = _mapper.Map<AgregarActuarioMasivoM>(request.Actuario);

        return await _repository.AgregarActuarioMasivo(actuario, _sesionService.SesionActual.EmpleadoId);
    }
}
