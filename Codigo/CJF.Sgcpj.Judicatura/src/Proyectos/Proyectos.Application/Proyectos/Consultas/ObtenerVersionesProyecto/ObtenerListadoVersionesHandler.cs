using CJF.Sgcpj.Judicatura.Common.Application.Common.Exceptions;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerVersionesProyecto;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Proyectos.Application.Proyectos.Consultas.ObtenerVersionesProyecto;

public class ObtenerListadoVersionesHandler : IRequestHandler<ObtenerListadoVersiones, ListadoVersionesDto>
{
    private readonly IProyectosRepository _repository;
    private readonly ISesionService _sessionService;
    private readonly ILogger<ObtenerListadoVersionesHandler> _logger;

    public ObtenerListadoVersionesHandler(IProyectosRepository repository, ISesionService sesionService, ILogger<ObtenerListadoVersionesHandler> logger)
    {
        _repository = repository;
        _sessionService = sesionService;
        _logger = logger;
    }

    public async Task<ListadoVersionesDto> Handle(ObtenerListadoVersiones request, CancellationToken cancellationToken)
    {
        try
        {
            request.CatOrganismoId = _sessionService.SesionActual.CatOrganismoId;
            var listadoVersiones = await _repository.ObtenerVersionesProyecto(request);

            return listadoVersiones is null ? throw new NotFoundException("El proyecto no cuenta con versiones") : listadoVersiones;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
