using CJF.Sgcpj.Judicatura.Common.Application.Common.Exceptions;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Helpers;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Proyectos.Application.Common.Enums;
using Proyectos.Application.Common.Helpers;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Comandos.ActualizarEstadoProyectoComando;

public class ActualizarEstadoProyectoComandoHandler : IRequestHandler<ActualizarEstadoProyectoComando, EstadoProyectoActualizadoDto>
{
    private readonly IProyectosRepository _proyectoRepository;
    private readonly ISesionService _sessionService;
    private readonly INasArchivo _nasArchivo;
    private readonly IArchivosRepository _archivoRepository;
    private readonly string _moduloProyectos = "Proyectos";
    private readonly AlertsHelper _alertsHelper;
    private readonly IWordsUtil _wordsUtil;
    private readonly CryptographicHelper _cryptographicHelper;
    private readonly ILogger<ActualizarEstadoProyectoComandoHandler> _logger;

    public ActualizarEstadoProyectoComandoHandler(IProyectosRepository proyectoRepository, ISesionService sesionService, INasArchivo nasArchivo, IArchivosRepository archivoRepository, AlertsHelper alertsHelper, IWordsUtil wordsUtil, CryptographicHelper cryptographicHelper, ILogger<ActualizarEstadoProyectoComandoHandler> logger)
    {
        _proyectoRepository = proyectoRepository;
        _sessionService = sesionService;
        _nasArchivo = nasArchivo;
        _archivoRepository = archivoRepository;
        _alertsHelper = alertsHelper;
        _wordsUtil = wordsUtil;
        _cryptographicHelper = cryptographicHelper;
        _logger = logger;
    }

    public async Task<EstadoProyectoActualizadoDto> Handle(ActualizarEstadoProyectoComando request, CancellationToken cancellationToken)
    {
        try
        {
            var actualizarEstadoProyecto = new ActualizarEstadoProyecto
            {
                ProyectoId = request.ProyectoId,
                EstadoId = request.EstadoId,
                ArchivoCorrecciones = request.NombreArchivoCorrecciones,
                Correcciones = request.Correcciones,
                UsuarioId = _sessionService.SesionActual.EmpleadoId,
                IpUsuario = "127.0.0.1" //ToDo Obtener IP del cliente
            };

            var result = await _proyectoRepository.ActualizarEstadoProyecto(actualizarEstadoProyecto);
            if (request.EstadoId != (int)EstadoProyectoEnum.Aprobado && request.ArchivoCorreciones != null)
            {
                var rutaChunk = await _archivoRepository.RutaEscrituraPorModulo(_moduloProyectos);
                try
                {
                    var path = rutaChunk + "\\" + _sessionService.SesionActual.CatOrganismoId.ToString() + "\\" + result.NombreArchivo;

                    if (string.IsNullOrEmpty(result.Anio) || string.IsNullOrEmpty(result.NombreArchivo))
                    {
                        throw new RuleException("Ocurrió un error al actualizar el proyecto");
                    }

                    var metadata = new WordProperties
                    {
                        Creator = result.Titular,
                        Version = result.Version
                    };

                    var data = _wordsUtil.ModifyDocumentProperties(request.ArchivoCorreciones, metadata);

                    var keys = new ParametrosClave
                    {
                        Expediente = result.AsuntoNeunId.ToString(),
                        Fecha = result.FechaAlta.Date.ToString(),
                        Usuarios = new string[]
                        {
                        result.Secretario, result.Titular
                        }
                    };

                    var encryptedData = _cryptographicHelper.EncryptData(data, keys);

                    _nasArchivo.AlmacenarArchivo(path, encryptedData);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);

                    var rollback = new RollbackProyectoArchivo()
                    {
                        ProyectoId = result.ProyectoId,
                        EmpleadoId = _sessionService.SesionActual.EmpleadoId
                    };

                    _ = await _proyectoRepository.EliminarCorreccionesArchivo(rollback);
                    throw;
                }
            }

            var alerta = new EnviarAlerta
            {
                Destinatarios = new List<Destinatario>
                {
                    new() {
                        OrganismoId = result.CatOrganismoId.ToString(),
                        UsuarioId = result.SecretarioId.ToString(),
                    }
                },
                Mensaje = $"Se ha actualizado el estatus del proyecto vinculado al expediente: {result.NumeroExpediente} {result.TipoAsunto}"
            };

            _alertsHelper.SendAlert(alerta);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
