using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Comandos.ReasignarSecretarioComando;

public class ReasignarSecretarioComandoHandler : IRequestHandler<ReasignarSecretarioComando, ReasignacionSecretarioDTO>
{
    private readonly IProyectosRepository _proyectoRepository;
    private readonly ISesionService _sessionService;
    private readonly ILogger<ReasignarSecretarioComandoHandler> _logger;

    public ReasignarSecretarioComandoHandler(IProyectosRepository proyectosRepository, ISesionService sesionService, ILogger<ReasignarSecretarioComandoHandler> logger)
    {
        _proyectoRepository = proyectosRepository;
        _sessionService = sesionService;
        _logger = logger;
    }

    public async Task<ReasignacionSecretarioDTO> Handle(ReasignarSecretarioComando request, CancellationToken cancellationToken)
    {
        try
        {
            var reasignar = new ReasignarSecretario
            {
                CatOrganismoId = _sessionService.SesionActual.CatOrganismoId,
                EmpleadoId = _sessionService.SesionActual.EmpleadoId,
                ProyectosId = request.ProyectosId,
                SecretarioNuevoId = request.SecretarioNuevoId
            };

            var result = await _proyectoRepository.ReasignarSecretario(reasignar);
            return new ReasignacionSecretarioDTO { Actualizado = result };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
