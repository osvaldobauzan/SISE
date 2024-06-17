using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Common.Repositories;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Oficialia.Application.Oficialia.Consultas.ObtenerPromociones;
public class ObtenerPromocionesHandler : IRequestHandler<OficialiaPartesFiltro, List<OficialiaPartesDTO>>
{
    private readonly IOficialiaPartesRepository _repository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;

    public ObtenerPromocionesHandler(IOficialiaPartesRepository repository, IMapper mapper, ISesionService sesionService)
    {
        _repository = repository;
        _mapper = mapper;
        _sesionService = sesionService;
    }

    public async Task<List<OficialiaPartesDTO>> Handle(OficialiaPartesFiltro request, CancellationToken cancellationToken)
    {
        request.IdOrganismo = _sesionService.SesionActual.CatOrganismoId;
        request.IdUsuario = request.IdUsuario == null ? _sesionService.SesionActual.EmpleadoId : request.IdUsuario;

        return await _repository.ObtenerPromociones(request);
    }
}