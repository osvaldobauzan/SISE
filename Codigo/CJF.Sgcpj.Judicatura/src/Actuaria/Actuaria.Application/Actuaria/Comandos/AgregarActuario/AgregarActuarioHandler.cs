using AutoMapper;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.AgregarActuario;
public class AgregarActuarioHandler : IRequestHandler<AgregarActuarioComando, bool>
{
    private readonly IMapper _mapper;
    private readonly IActuariaRepository _repository;
    private readonly ISesionService _sesionService;

    public AgregarActuarioHandler(IMapper mapper, IActuariaRepository repository, ISesionService sesionService)
    {
        _mapper = mapper;
        _repository = repository;
        _sesionService = sesionService;
    }
    public async Task<bool> Handle(AgregarActuarioComando request, CancellationToken cancellationToken)
    {
        var actuario = _mapper.Map<AgregarActuarioM>(request.Actuario);
        var result = await _repository.AgregarActuario(actuario, _sesionService.SesionActual.EmpleadoId);
        
        if (result && (actuario.TipoNotificacionId == 5 || actuario.TipoNotificacionId == 11))
        {   
        }
        return result;
    }
}
