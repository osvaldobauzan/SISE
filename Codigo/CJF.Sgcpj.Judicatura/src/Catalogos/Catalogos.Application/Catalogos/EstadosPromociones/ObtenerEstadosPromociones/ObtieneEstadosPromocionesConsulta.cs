using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.EstadosPromociones.ObtenerEstadosPromociones;


public record ObtieneEstadosPromocionesConsulta : IRequest<List<CatalogoEstadoPromocionesDto>>
{

}
public class ObtieneEstadosPromocionesConsultaHandler : IRequestHandler<ObtieneEstadosPromocionesConsulta, List<CatalogoEstadoPromocionesDto>>
{

    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ObtieneEstadosPromocionesConsultaHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }



    Task<List<CatalogoEstadoPromocionesDto>> IRequestHandler<ObtieneEstadosPromocionesConsulta, List<CatalogoEstadoPromocionesDto>>.Handle(ObtieneEstadosPromocionesConsulta request, CancellationToken cancellationToken)
    {
        var listaCatalogos = new List<CatalogoEstadoPromocionesDto>();

        listaCatalogos.Add(new CatalogoEstadoPromocionesDto()
        {
            Id = 1,
            NombreEstado = "Sin captura"
        });
        listaCatalogos.Add(new CatalogoEstadoPromocionesDto()
        {
            Id = 2,
            NombreEstado = "Captura"
        });
        listaCatalogos.Add(new CatalogoEstadoPromocionesDto()
        {
            Id = 3,
            NombreEstado = "Digitalización"
        });
        listaCatalogos.Add(new CatalogoEstadoPromocionesDto()
        {
            Id = 4,
            NombreEstado = "Asignación a mesa"
        });

        return Task.FromResult(listaCatalogos);
    }
}
