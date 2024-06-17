using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Promovente.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Promovente.Application.Common.Repositories;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Promovente.Application.Promoventes.Comandos.AgregarPromoventes;
public class AgregarPromoventesComando: IRequest<long>
{
    public AgregarPromoventesDto Promovente { get; set; }
}
public class AgregarPromoventesComandoHandler : IRequestHandler<AgregarPromoventesComando, long>
{
    private readonly IMapper _mapper;
    private readonly IPromoventesRepository _promoventesRepository;
    private readonly ISesionService _sesionService;

    public AgregarPromoventesComandoHandler(IMapper mapper, IPromoventesRepository promoventesRepository, ISesionService sesionService)
    {
        _mapper = mapper;
        _promoventesRepository = promoventesRepository;
        _sesionService = sesionService;
    }

    public async Task<long> Handle(AgregarPromoventesComando request, CancellationToken cancellationToken)
    {
        var promovente = _mapper.Map<AgregarPromovente>(request.Promovente);
        promovente.RegistroEmpleadoId = _sesionService.SesionActual.EmpleadoId;
        return await _promoventesRepository.AgregarPromovente(promovente);
    }
}