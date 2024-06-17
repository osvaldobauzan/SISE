using System.Text.Json;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Exceptions;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Helpers;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Proyectos.Application.Common.Helpers;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Comandos.SubirProyectoConAudienciaComando;

public class SubirProyectoConAudienciaComandoHandler : IRequestHandler<SubirProyectoConAudienciaComando, ProyectoConAudienciaDto>
{
    private readonly IProyectosRepository _proyectoRepository;
    private readonly ISesionService _sessionService;
    private readonly INasArchivo _nasArchivo;
    private readonly IArchivosRepository _archivoRepository;
    private readonly string _moduloProyectos = "Proyectos";
    private readonly AlertsHelper _alertsHelper;
    private readonly IWordsUtil _wordsUtil;
    private readonly CryptographicHelper _cryptographicHelper;
    private readonly ILogger<SubirProyectoConAudienciaComandoHandler> _logger;
    private readonly ILogService _logService;

    public SubirProyectoConAudienciaComandoHandler(IProyectosRepository proyectoRepository, ISesionService sesionService, INasArchivo nasArchivo, IArchivosRepository archivoRepository, AlertsHelper alertsHelper, IWordsUtil wordsUtil, CryptographicHelper cryptographicHelper, ILogger<SubirProyectoConAudienciaComandoHandler> logger, ILogService logService)
    {
        _proyectoRepository = proyectoRepository;
        _sessionService = sesionService;
        _nasArchivo = nasArchivo;
        _archivoRepository = archivoRepository;
        _alertsHelper = alertsHelper;
        _wordsUtil = wordsUtil;
        _cryptographicHelper = cryptographicHelper;
        _logger = logger;
        _logService = logService;
    }

    public async Task<ProyectoConAudienciaDto> Handle(SubirProyectoConAudienciaComando request, CancellationToken cancellationToken)
    {
        try
        {
            var newRequest = CopiarRequest(request);
            RegistrarLog(TipoMovimiento.Crear, JsonSerializer.Serialize(newRequest), "Inicio subir proyecto");

            var subirProyectoConAudiencia = new SubirProyectoConAudiencia
            {
                CatOrganismoId = _sessionService.SesionActual.CatOrganismoId,
                AsuntoNeunId = request.AsuntoNeunId,
                TitularId = request.TitularId,
                SecretarioId = request.SecretarioId,
                TipoSentenciaId = request.TipoSentenciaId,
                Sintesis = request.Sintesis,
                EmpleadoId = _sessionService.SesionActual.EmpleadoId,
                NombreArchivo = request.NombreArchivo,
                IpUsuario = "127.0.0.1", //ToDo: Obtener IP del cliente
                Motivos = request.Motivos
            };

            var result = await _proyectoRepository.SubirProyectoConAudiencia(subirProyectoConAudiencia);
            RegistrarLog(TipoMovimiento.Crear, JsonSerializer.Serialize(newRequest), "Registrar proyecto", JsonSerializer.Serialize(result));

            var rutaChunk = await _archivoRepository.RutaEscrituraPorModulo(_moduloProyectos);
            RegistrarLog(TipoMovimiento.Crear, JsonSerializer.Serialize(newRequest), "Ruta chunk", rutaChunk);
            try
            {
                var path = rutaChunk + "\\" + _sessionService.SesionActual.CatOrganismoId.ToString() + "\\" + result.NombreArchivo;
                RegistrarLog(TipoMovimiento.Crear, JsonSerializer.Serialize(newRequest), "Ruta chunk completa", path);

                if (string.IsNullOrEmpty(result.Anio) || string.IsNullOrEmpty(result.NombreArchivo))
                {
                    _logger.LogError(new Exception("Los datos del nombre del archivo no son válidos"), "Ocurrió un error al registrar el proyecto");
                    throw new RuleException("Ocurrió un error al registrar el proyecto");
                }

                var metadata = new WordProperties
                {
                    Creator = result.Secretario,
                    Version = result.Version
                };

                RegistrarLog(TipoMovimiento.Crear, JsonSerializer.Serialize(newRequest), "Modificar metadatos de archivo");
                var data = _wordsUtil.ModifyDocumentProperties(request.Archivo, metadata);
                RegistrarLog(TipoMovimiento.Crear, JsonSerializer.Serialize(newRequest), "Metadatos modificados");

                var keys = new ParametrosClave
                {
                    Expediente = result.AsuntoNeunId.ToString(),
                    Fecha = result.FechaCreacion.Date.ToString(),
                    Usuarios = new string[]
                    {
                    result.Secretario, result.Titular
                    }
                };

                RegistrarLog(TipoMovimiento.Crear, JsonSerializer.Serialize(newRequest), "Cifrar archivo");
                var encryptedData = _cryptographicHelper.EncryptData(data, keys);
                RegistrarLog(TipoMovimiento.Crear, JsonSerializer.Serialize(newRequest), "Archivo cifrado");

                RegistrarLog(TipoMovimiento.Crear, JsonSerializer.Serialize(newRequest), $"Guardar archivo", path);
                _nasArchivo.AlmacenarArchivo(path, encryptedData);
                RegistrarLog(TipoMovimiento.Crear, JsonSerializer.Serialize(newRequest), "Archivo guardado", path);

                var accion = result.Version.Equals(1) ? "un nuevo proyecto" : "una nueva versión de proyecto vinculada";
                var mensaje = $"El secretario {result.Secretario} ha subido {accion} al expediente: {result.NumeroExpediente} {result.TipoAsunto}";

                var alerta = new EnviarAlerta
                {
                    Destinatarios = new List<Destinatario>
                    {
                        new()
                        {
                            OrganismoId = result.CatOrganismoId.ToString(),
                            UsuarioId = result.TitularId.ToString(),
                        }
                    },
                    Mensaje = mensaje
                };

                RegistrarLog(TipoMovimiento.Crear, JsonSerializer.Serialize(alerta), "Enviar alerta");
                _alertsHelper.SendAlert(alerta);
                RegistrarLog(TipoMovimiento.Crear, JsonSerializer.Serialize(alerta), "Alerta enviada");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                var rollback = new RollbackProyectoArchivo()
                {
                    ProyectoId = result.ProyectoId,
                    ArchivoId = result.ProyectoArchivoId,
                    EmpleadoId = _sessionService.SesionActual.EmpleadoId
                };

                RegistrarLog(TipoMovimiento.Crear, JsonSerializer.Serialize(rollback), "Rollback", JsonSerializer.Serialize(rollback));
                _ = await _proyectoRepository.EliminarProyectoArchivo(rollback);
                throw;
            }

            RegistrarLog(TipoMovimiento.Crear, JsonSerializer.Serialize(newRequest), "Fin subir proyecto", JsonSerializer.Serialize(result));
            return await Task.FromResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            var newRequest = CopiarRequest(request);
            RegistrarLog(TipoMovimiento.Crear, JsonSerializer.Serialize(newRequest), "Error subir proyecto", ex.Message);
            throw;
        }
    }

    private void RegistrarLog(TipoMovimiento tipoMovimiento, string request, string accion, string? response = null)
    {
        _logService.RegistrarEvento(
        new DatosLog
        {
            TipoMovimiento = tipoMovimiento,
            IdUsuario = _sessionService.SesionActual.EmpleadoId,
            DatosEntrada = request,
            DatosSalida = response,
            ModuloOrigen = $"{GetType().Name} - {accion}"
        }).ConfigureAwait(false).GetAwaiter();
    }

    private SubirProyectoConAudienciaComando CopiarRequest(SubirProyectoConAudienciaComando request)
    {
        return new SubirProyectoConAudienciaComando
        {
            AsuntoNeunId = request.AsuntoNeunId,
            Archivo = null,
            CatOrganismoId = request.CatOrganismoId,
            NombreArchivo = request.NombreArchivo,
            SecretarioId = request.SecretarioId,
            Sintesis = request.Sintesis,
            TipoSentenciaId = request.TipoSentenciaId,
            TitularId = request.TitularId
        };
    }
}
