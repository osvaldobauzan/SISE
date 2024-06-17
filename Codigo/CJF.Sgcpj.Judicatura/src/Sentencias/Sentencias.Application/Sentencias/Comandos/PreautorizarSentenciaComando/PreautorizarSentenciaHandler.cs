using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Sentencias.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Sentencias.Application.Common.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Sentencias.Application.Sentencias.Comandos.PreautorizarSentenciaComando;
public class PreautorizarSentenciaHandler : IRequestHandler<PreautorizarSentenciaComando, string>
{
    private readonly ISentenciasRepository _repository;
    private readonly ISesionService _sessionService;
    private readonly ILogger<PreautorizarSentenciaComando> _logger;

    public PreautorizarSentenciaHandler(ISentenciasRepository repository, ISesionService sessionService, ILogger<PreautorizarSentenciaComando> logger)
    {
        _repository = repository;
        _sessionService = sessionService;
        _logger = logger;
    }

    public async Task<string> Handle(PreautorizarSentenciaComando request, CancellationToken cancellationToken)
    {
        var respuesta = "Las sentencias enviadas fueron preaprobadas correctamente.";
        foreach (var sentencia in request.Sentencias ?? new List<PreautorizarSentenciaDto>())
        {
            try
            {
                var parametrosUsuarioAsuntosDocumentos = new UsuarioAsuntosDocumentos
                {
                    AsuntoDocumentoId = sentencia.AsuntoDocumentoId,
                    AsuntoNeunId = sentencia.AsuntoNeunId,
                    TipoUpdate = (int)TipoActualizaUsuario.EmpleadoPreautoriza,
                    Valor = _sessionService.SesionActual.EmpleadoId
                };
                await _repository.ActualizaUsuarioAsuntosDocumentos(parametrosUsuarioAsuntosDocumentos);

                var parametrosAutorizacionDocumentos = new AutorizacionDocumentos
                {
                    AsuntoDocumentoId = sentencia.AsuntoDocumentoId,
                    AsuntoNeunId = sentencia.AsuntoNeunId,
                    AutorizacionDocumentosId = (int)CatAutorizacionDocumentos.Preautorizacion
                };
                await _repository.ActualizaAutorizacionDocumentos(parametrosAutorizacionDocumentos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                respuesta = "Una o más de las sentencias enviadas no fueron preaprobadas correctamente, intente nuevamente por favor.";
            }
        }

        return respuesta;
    }
}
