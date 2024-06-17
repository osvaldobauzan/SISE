using System.Text.Json;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Sentencias.Application.Common.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using CJF.Sgcpj.Judicatura.Sentencias.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Sentencias.Application.FirmadorDocumentos.Consultas.LecturaArchivoFirmador;
public class LecturaDocumentoQuery : IRequest<RespuestaLecturaDto>
{
    public string Id { get; set; }
}


public class LecturaDocumentoQueryHandler : IRequestHandler<LecturaDocumentoQuery, RespuestaLecturaDto>
{
    private readonly INasArchivo _clienteNas;
    private readonly IConfiguration _configuration;
    private readonly IArchivosRepository _archivosRepository;
    private readonly ILogService _logService;

    public LecturaDocumentoQueryHandler(INasArchivo clienteNas,
                                        IConfiguration configuration,
                                        IArchivosRepository tramitesRepository,
                                        ILogService logService)
    {
        _clienteNas = clienteNas;
        _configuration = configuration;
        _archivosRepository = tramitesRepository;
        _logService = logService;
    }
    public async Task<RespuestaLecturaDto?> Handle(LecturaDocumentoQuery request, CancellationToken cancellationToken)
    {
        RegistrarLog(TipoMovimiento.Crear, JsonSerializer.Serialize(request), "obtener documento para firma");
        //request.Id = request.Id.Substring(2);
        var documentoNombre = string.Empty;

        //var archivo = await _tramitesRepository.ObtenerTramitePorIdModuloTipoAsync(request.Id, 2, "acuerdo");
        var archivo = await _archivosRepository.ObtenerSentenciaPorIdModuloTipoAsync(new Guid(request.Id), 2, "acuerdo");

        DocumentoBase64Dto? archivoBase64 = new DocumentoBase64Dto();
        if (archivo != null && archivo.Any())
        {
            DatosDocumento(out documentoNombre, archivo, out archivoBase64);
        }

        string base64 = archivoBase64?.Base64;

        //documentoNombre = request.modulo.ToString() + "=" + documentoNombre;
        //var guidDoc = request.modulo.ToString() + "_" + request.Id.ToString();

        var respuesta = new RespuestaLecturaDto()
        {
            Status = base64 == null ? false : true,
            Mensaje = base64 == null ? "Archivo no recuperado" : "Archivo recuperado exitosamente",
            Data = new RespuestaLecturaDataDto()
            {
                Id = request.Id.ToString(),
                Nombre = documentoNombre,
                Contenido = base64
            }
        };

        RegistrarLog(TipoMovimiento.Crear, JsonSerializer.Serialize(respuesta), "objeto enviado a firma");
        return respuesta;
    }

    private void DatosDocumento(out string documentoNombre, IEnumerable<SentenciaArchivo>? archivo, out DocumentoBase64Dto archivoBase64)
    {
        var archivoEncontrado = archivo.First();

        documentoNombre = archivoEncontrado.NombreArchivo;
        if (archivoEncontrado.Firmado)
        {
            archivoEncontrado.RutaCompleta = archivoEncontrado.RutaCompleta + ".p7m";
            documentoNombre = archivoEncontrado.NombreArchivo + ".p7m";
        }
        archivoBase64 = _clienteNas.ObtenerArchivoComoBase64String(archivoEncontrado.RutaCompleta);
    }

    private void RegistrarLog(TipoMovimiento tipoMovimiento, string request, string accion, string? response = null)
    {
        _logService.RegistrarEvento(
        new DatosLog
        {
            TipoMovimiento = tipoMovimiento,
            IdUsuario = 0,//_sessionService.SesionActual.EmpleadoId,
            DatosEntrada = request,
            DatosSalida = response,
            ModuloOrigen = $"{GetType().Name} - {accion}"
        }).ConfigureAwait(false).GetAwaiter();
    }
}
