using CJF.Sgcpj.Judicatura.Common.Application.Common.Exceptions;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerUltimaVersionProyecto;
using MediatR;
using Microsoft.Extensions.Logging;
using Proyectos.Application.Common.Models;


namespace Proyectos.Application.Proyectos.Consultas.ObtenerUltimaVersionProyecto;
public class ObtenerUltimaVersionHandler : IRequestHandler<ObtenerVersion, VersionDto>
{
    private readonly IProyectosRepository _repository;
    private readonly ISesionService _sessionService;
    private readonly ILogger<ObtenerUltimaVersionHandler> _logger;

    public ObtenerUltimaVersionHandler(IProyectosRepository repository, ISesionService sesionService, ILogger<ObtenerUltimaVersionHandler> logger)
    {
        _repository = repository;
        _sessionService = sesionService;
        _logger = logger;
    } 

    public async Task<VersionDto> Handle(ObtenerVersion request, CancellationToken cancellationToken)
    {
        try
        {
            request.CatOrganismoId = _sessionService.SesionActual.CatOrganismoId;
            var version = await _repository.ObtenerUltimaVersionProyecto(request);

            return version is null ? throw new NotFoundException("El proyecto no cuenta con versiones") : version;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
