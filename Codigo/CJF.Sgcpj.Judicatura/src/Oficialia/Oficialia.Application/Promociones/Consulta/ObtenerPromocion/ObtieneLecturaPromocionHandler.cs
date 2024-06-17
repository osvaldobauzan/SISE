using AutoMapper;
using CJF.Sgcpj.Judicatura.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Oficialia.Domain.Entities;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerPromocion;
public class ObtieneLecturaPromocionHandler : IRequestHandler<ObtieneLecturaPromocion, Promocion>
{
    private readonly IPromocionesRepository _repository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;

    public ObtieneLecturaPromocionHandler(IPromocionesRepository repository, IMapper mapper, ISesionService sesionService)
    {
        _repository = repository;
        _mapper = mapper;
        _sesionService = sesionService;
    }

    public async Task<Promocion> Handle(ObtieneLecturaPromocion request, CancellationToken cancellationToken)
    {

        int asuntoNeunId = 0;

        var datos2 = await _repository.ObtenerLecturaPromocion(request.AsuntoNeunId, request.AsuntoID, request.YearPromocion, 
            request.NumeroOrden, _sesionService.SesionActual.CatOrganismoId, request.NumeroRegistro, request.OrigenPromocion);


        return new Promocion() ; // Devolver objeto con datos
    }


}
