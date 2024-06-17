using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerMotivosPartes;

public class ObtenerMotivosPartesHandler : IRequestHandler<ObtenerMotivosPartes, ListadoMotivosPartesDto>
{
    private readonly IProyectosRepository _proyectosRepository;
    public readonly ISesionService _sessionService;
    public readonly ILogger<ObtenerMotivosPartesHandler> _logger;

    public ObtenerMotivosPartesHandler(IProyectosRepository proyectosRepository, ISesionService sesionService, ILogger<ObtenerMotivosPartesHandler> logger)
    {
        _proyectosRepository = proyectosRepository;
        _sessionService = sesionService;
        _logger = logger;
    }

    public async Task<ListadoMotivosPartesDto> Handle(ObtenerMotivosPartes request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _proyectosRepository.ObtenerMotivosPartes(request.IdProyecto);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
