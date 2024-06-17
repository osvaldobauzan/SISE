using AutoMapper;
using CJF.Sgcpj.Judicatura.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Comandos;
public class EliminarPromocionComando : IRequest<string>
{
    public EliminarPromocionDto Promocion { get; set; }
}
public class EliminarPromocionComandoHandler : IRequestHandler<EliminarPromocionComando, string>
{
    private readonly IMapper _mapper;
    private readonly IPromocionesRepository _promocionesRepository;
    private readonly ISesionService _sesionService;

    public EliminarPromocionComandoHandler(IMapper mapper, IPromocionesRepository promocionesRepository, ISesionService sesionService)
    {
        _mapper = mapper;
        _promocionesRepository = promocionesRepository;
        _sesionService = sesionService;
    }
    public async Task<string> Handle(EliminarPromocionComando request, CancellationToken cancellationToken)
    {
        request.Promocion.CatIdOrganismo = _sesionService.SesionActual.CatOrganismoId;
      
        var promocion = _mapper.Map<EliminarPromocion>(request.Promocion);
        return await _promocionesRepository.EliminarPromocion(promocion);
    }
}