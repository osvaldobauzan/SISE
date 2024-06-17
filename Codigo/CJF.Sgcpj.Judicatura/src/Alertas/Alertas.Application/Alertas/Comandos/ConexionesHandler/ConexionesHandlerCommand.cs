using CJF.Sgcpj.Judicatura.Alertas.Application.Alertas.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Alertas.Application.Alertas.Comandos.ConexionesHandler;
public class ConexionesHandlerCommand : IRequest<bool>
{
    public UserConnectionsDTO UserConnections { get; set; }
}
public class ConexionesHandlerCommandHandler : IRequestHandler<ConexionesHandlerCommand, bool>
{
    private readonly ILogger<ConexionesHandlerCommandHandler> _logger;
    private readonly IUserConnectionsHandler _connectionsHandler;
    private readonly ISesionService _sesionService;

    public ConexionesHandlerCommandHandler(ILogger<ConexionesHandlerCommandHandler> logger,
                                           IUserConnectionsHandler connectionsHandler,
                                           ISesionService sesionService)
    {
        _logger = logger;
        _connectionsHandler = connectionsHandler;
        _sesionService = sesionService;
    }

    public async Task<bool> Handle(ConexionesHandlerCommand request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(request.UserConnections.PastConnection))
        {
            await _connectionsHandler.RemoveConnectionAsync(
                request.UserConnections.PastConnection,
                _sesionService.SesionActual.EmpleadoId.ToString());

            _logger?.LogInformation("Removed connection");
        }

        await _connectionsHandler.RegisterConnectionAsync(
              _sesionService.SesionActual.EmpleadoId.ToString(),
              request.UserConnections.CurrentConnection,
              _sesionService.SesionActual.CatOrganismoId.ToString());

        _logger?.LogInformation($"Connection added {_sesionService.SesionActual.EmpleadoId} - {request.UserConnections.CurrentConnection}");

        return true;
    }
}
