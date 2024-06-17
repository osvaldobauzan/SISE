using Azure.Storage.Blobs.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Documentos.Application.FirmadorDocumentos.Consulta.LecturaArchivoFirmador;
public class LecturaDocumentoQuery : IRequest<RespuestaLecturaDto>
{
    public string Id { get; set; }
    public int modulo { get; set; }
}


public class LecturaDocumentoQueryHandler : IRequestHandler<LecturaDocumentoQuery, RespuestaLecturaDto>
{
    private readonly INasArchivo _clienteNas;
    private readonly IConfiguration _configuration;
    private readonly ITramitesRepository _tramitesRepository;
    private readonly ILogService _logService;

    public LecturaDocumentoQueryHandler(INasArchivo clienteNas,
                                        IConfiguration configuration,
                                        ITramitesRepository tramitesRepository,
                                        ILogService logService)
    {
        _clienteNas = clienteNas;
        _configuration = configuration;
        _tramitesRepository = tramitesRepository;
        _logService = logService;
    }
    public async Task<RespuestaLecturaDto?> Handle(LecturaDocumentoQuery request, CancellationToken cancellationToken)
    {
        request.Id = request.Id.Substring(2);
        var documentoNombre = string.Empty;
        string descripcionModulo = "";
        if(request.modulo == 1)
        {
            descripcionModulo = "promocion";
        }
        else if(request.modulo == 2)
        {
            descripcionModulo = "acuerdo";
        }

        //var archivo = await _tramitesRepository.ObtenerTramitePorIdModuloTipoAsync(request.Id, 2, "acuerdo");
        var archivo = await _tramitesRepository.ObtenerTramitePorIdModuloTipoAsync(new Guid(request.Id), request.modulo, descripcionModulo);
        
        DocumentoBase64Dto? archivoBase64 = new DocumentoBase64Dto();
        if (archivo != null && archivo.Any())
        {
            DatosDocumento(out documentoNombre, archivo, out archivoBase64);
        }
        else
        {
            archivo = await _tramitesRepository.ObtenerTramitePorIdModuloTipoAsync(new Guid(request.Id), 2, "oficio");
            if (archivo != null && archivo.Any())
            {
                DatosDocumento(out documentoNombre, archivo, out archivoBase64);
            }
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

        return respuesta;
    }

    private void DatosDocumento(out string documentoNombre, IEnumerable<CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models.Tramite>? archivo, out DocumentoBase64Dto archivoBase64)
    {
        var archivoEncontrado = archivo.First();


        documentoNombre = archivoEncontrado.NombreArchivo;
        if (archivoEncontrado.Firmado)
        {
            archivoEncontrado.RutaCompleta  = archivoEncontrado.RutaCompleta + ".p7m";
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