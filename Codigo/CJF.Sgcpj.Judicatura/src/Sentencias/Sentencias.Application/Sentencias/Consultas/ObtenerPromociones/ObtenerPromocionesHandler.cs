using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Sentencias.Application.Common.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;


namespace CJF.Sgcpj.Judicatura.Sentencias.Application.Sentencias.Consultas.ObtenerPromociones;
public class ObtenerPromocionesHandler : IRequestHandler<ObtenerPromocionesExpediente, ListadoPromocionesDTO>
{
    private readonly IPromocionesRepository _promocionesRepository;
    public readonly ISesionService _sessionService;
    public readonly ILogger<ObtenerPromocionesHandler> _logger;

    public ObtenerPromocionesHandler(IPromocionesRepository promocionesRepository, ISesionService sesionService, ILogger<ObtenerPromocionesHandler> logger)
    {
        _promocionesRepository = promocionesRepository;
        _sessionService = sesionService;
        _logger = logger;
    }
    public async Task<ListadoPromocionesDTO> Handle(ObtenerPromocionesExpediente request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _promocionesRepository.ObtenerPromocionesCuaderno(request.AsuntoId, request.AsuntoNeunId, request.TipoCuaderno, request.SintesisOrden);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
        
    }
}
