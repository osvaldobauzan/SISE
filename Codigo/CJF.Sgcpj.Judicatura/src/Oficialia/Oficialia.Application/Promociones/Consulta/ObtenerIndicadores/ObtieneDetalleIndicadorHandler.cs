using AutoMapper;
using CJF.Sgcpj.Judicatura.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerNumeroPromocion;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerIndicadores;
public class ObtieneDetalleIndicadorHandler : IRequestHandler<DetalleIndicadoresRequest, List<DetalleIndicadoresDto>>
{
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;
    private readonly IPromocionesRepository _repository;

    public ObtieneDetalleIndicadorHandler(IPromocionesRepository repository, IMapper mapper, ISesionService sesionService)
    {
       _mapper = mapper;
        _repository = repository;
       _sesionService = sesionService;
    }

    async Task<List<DetalleIndicadoresDto>> IRequestHandler<DetalleIndicadoresRequest, List<DetalleIndicadoresDto>>.Handle(DetalleIndicadoresRequest request, CancellationToken cancellationToken)
    {
        var listaCatalogoOficiales = new List<DetalleIndicadoresDto>();
        var result = await _repository.ObtenerIndicadoresPromocion(request.FechaInicioBusqueda, request.FechaFinBusqueda, _sesionService.SesionActual.CatOrganismoId);
        if (result != null && result.Any())
        {
            var datos = result.ToList();
            listaCatalogoOficiales = _mapper.Map<List<DetalleIndicadoresDto>>(datos);
        }
        
        return await Task.FromResult(listaCatalogoOficiales);
    }
}
