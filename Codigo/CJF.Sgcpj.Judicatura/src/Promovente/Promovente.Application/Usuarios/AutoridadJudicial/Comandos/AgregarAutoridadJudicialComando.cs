using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Promovente.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Promovente.Application.Common.Repositories;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Promovente.Application.Usuarios.AutoridadJudicial.Comandos;

public class AgregarAutoridadJudicialComando : IRequest<long>
{
    public AgregarAutoridadJudicialDto AutoridadJudicial { get; set; }
}
public class AgregarAutoridadJudicialComandoHandler : IRequestHandler<AgregarAutoridadJudicialComando, long>
{
    private readonly IMapper _mapper;
    private readonly IPromoventesRepository _promocionesRepository;
    private readonly ISesionService _sesionService;

    public AgregarAutoridadJudicialComandoHandler(IMapper mapper, IPromoventesRepository promocionesRepository, ISesionService sesionService)
    {
        _mapper = mapper;
        _promocionesRepository = promocionesRepository;
        _sesionService = sesionService;
    }

    public async Task<long> Handle(AgregarAutoridadJudicialComando request, CancellationToken cancellationToken)
    {
        var autoridadjudicioal = _mapper.Map<AgregarAutoridadJudicial>(request.AutoridadJudicial);
        autoridadjudicioal.RegistroEmpleadoId = _sesionService.SesionActual.EmpleadoId;
        autoridadjudicioal.catIdOrganismo = request.AutoridadJudicial.catIdOrganismo;
        return await _promocionesRepository.AgregarAutoridadJudicial(autoridadjudicioal);
    }
}
