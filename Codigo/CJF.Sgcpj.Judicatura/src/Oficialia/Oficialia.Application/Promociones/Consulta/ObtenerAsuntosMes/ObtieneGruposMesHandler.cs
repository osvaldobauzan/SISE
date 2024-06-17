using AutoMapper;
using CJF.Sgcpj.Judicatura.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerNumeroPromocion;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerTiempTurnos;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerAsuntosMes;
public class ObtieneGruposMesHandler : IRequestHandler<DetalleGruposMesRequest, List<DetalleGruposMesDto>>
{
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;
    private readonly IPromocionesRepository _repository;

    public ObtieneGruposMesHandler(IPromocionesRepository repository, IMapper mapper, ISesionService sesionService)
    {
       _mapper = mapper;
        _repository = repository;
       _sesionService = sesionService;
    }

    async Task<List<DetalleGruposMesDto>> IRequestHandler<DetalleGruposMesRequest,List<DetalleGruposMesDto>>.Handle(DetalleGruposMesRequest request, CancellationToken cancellationToken)
    {
        var listaCatalogoOficiales = new List<DetalleGruposMesDto>();
        var result = await _repository.ObtenerAsuntosMesPromocion(request.EmpleadoId);
        if (result != null && result.Any())
        {
            var datos = result.ToList();
            listaCatalogoOficiales = _mapper.Map<List<DetalleGruposMesDto>>(datos);
        }
        
        return await Task.FromResult(listaCatalogoOficiales);
    }
}
