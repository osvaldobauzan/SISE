using AutoMapper;
using MediatR;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Repositories;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Seguimientos.Promocion.Consulta;
public record class ObtenerPromocionConsulta : IRequest<Common.Models.Promociones>
{
    public Common.Models.Promociones promocion { get; set; }
}


public class ObtienePromocionHandler : IRequestHandler<ObtenerPromocionConsulta, Common.Models.Promociones>
{
    private readonly IPromocionRepository _promocionRepository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;

    public ObtienePromocionHandler(IPromocionRepository promocionRepository, IMapper mapper, ISesionService sesionService)
    {
        _promocionRepository = promocionRepository;
        _mapper = mapper;
        _sesionService = sesionService;
    }

    public async Task<Common.Models.Promociones> Handle(ObtenerPromocionConsulta request, CancellationToken cancellationToken)
    {

        var Result = await _promocionRepository.getPromocion(request.promocion);
        return Result;
    }

}

