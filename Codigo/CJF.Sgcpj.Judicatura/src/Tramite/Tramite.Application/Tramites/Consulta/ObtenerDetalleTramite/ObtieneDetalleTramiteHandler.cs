using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Repositories;

using MediatR;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerDetalleTramite;
public class ObtieneDetalleTramiteHandler : IRequestHandler<ObtieneDetalleTramiteConsulta,
    DetalleTramiteDto<CabeceraTramiteDto, PromocionesDto, PartesDto, OficioDto>>
{
    private readonly IMapper _mapper;
    private readonly ISesionService _sessionService;
    private readonly ITramitesRepository _repository;

    public ObtieneDetalleTramiteHandler(IMapper mapper, ISesionService sessionService, ITramitesRepository repository) =>
        (_mapper, _repository, _sessionService) = (mapper, repository, sessionService);

    public async Task<DetalleTramiteDto<CabeceraTramiteDto, PromocionesDto, PartesDto, OficioDto>> Handle(ObtieneDetalleTramiteConsulta request,
        CancellationToken cancellationToken)
    {
        var gt =
            new DetalleTramiteDto<CabeceraTramiteDto, PromocionesDto, PartesDto, OficioDto>();

        request.catOrganismoId = _sessionService.SesionActual.CatOrganismoId;

        var (dat1, dat2, dat3, dat4) = await _repository.ObtenerDetalleTramiteAsync(request);

        gt.CabeceraTramite = _mapper.Map<List<CabeceraTramiteDto>>(dat1);
        gt.Promociones = _mapper.Map<List<PromocionesDto>>(dat2);
        gt.Partes = _mapper.Map<List<PartesDto>>(dat3);
        gt.Oficio = _mapper.Map<List<OficioDto>>(dat4);

        return gt;
    }
}
