using AutoMapper;
using CJF.Sgcpj.Judicatura.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerTiempTurnos;
public class ObtieneIntervalosTiempoHandler : IRequestHandler<DetalleIntervalosRequest, List<DetalleIntervalosDto>>
{
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;
    private readonly IPromocionesRepository _repository;

    public ObtieneIntervalosTiempoHandler(IPromocionesRepository repository, IMapper mapper, ISesionService sesionService)
    {
       _mapper = mapper;
        _repository = repository;
       _sesionService = sesionService;
    }

    async Task<List<DetalleIntervalosDto>> IRequestHandler<DetalleIntervalosRequest, List<DetalleIntervalosDto>>.Handle(DetalleIntervalosRequest request, CancellationToken cancellationToken)
    {
        var listaCatalogoOficiales = new List<DetalleIntervalosDto>();
        var backUpEmp = _sesionService.SesionActual.EmpleadoId;
        var result = await _repository.ObtenerTiemposTurnos(request.FechaInicioBusqueda, request.FechaFinBusqueda, request.EmpleadoId);
        if (result != null && result.Any())
        {
            var datos = result.ToList();
            listaCatalogoOficiales = _mapper.Map<List<DetalleIntervalosDto>>(datos);
        }
        
        return await Task.FromResult(listaCatalogoOficiales);
    }
}
