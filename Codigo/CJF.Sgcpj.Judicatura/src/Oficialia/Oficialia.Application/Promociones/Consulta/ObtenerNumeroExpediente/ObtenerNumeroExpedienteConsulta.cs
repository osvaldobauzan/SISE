using AutoMapper;
using CJF.Sgcpj.Judicatura.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtienePromocionesConFiltro;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Oficialia.Application.Promociones.Consulta.ObtenerNumeroExpediente;
public class ObtenerNumeroExpedienteConsulta : IRequestHandler<ObtenerNumeroExpediente, ExpedienteDto>
{
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;
    private readonly IPromocionesRepository _promocionesRepository;
    public ObtenerNumeroExpedienteConsulta(IMapper mapper, IPromocionesRepository promocionesRepository, ISesionService sesionService)
    {
        _mapper = mapper;
        _promocionesRepository = promocionesRepository;
        _sesionService = sesionService;
    }
    async Task<ExpedienteDto> IRequestHandler<ObtenerNumeroExpediente, ExpedienteDto>.Handle(ObtenerNumeroExpediente request, CancellationToken cancellationToken)
    {        
        request.CatOrganismoId = _sesionService.SesionActual.CatOrganismoId;       
        var expediente = _mapper.Map<ObtenerNumeroExpediente>(request);
        var expedienteRes = await _promocionesRepository.ObtenerNumeroExpediente(expediente);   

        return await Task.FromResult(expedienteRes);
    }
}
