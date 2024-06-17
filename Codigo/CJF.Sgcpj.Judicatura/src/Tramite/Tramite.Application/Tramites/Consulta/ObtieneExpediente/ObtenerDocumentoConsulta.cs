using System.Text.Json;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Repositories;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtieneExpediente;
public record class ObtenerDocumentoConsulta : IRequest<DocumentoBase64Dto>
{
    public string Id { get; set; }
    public string esDescarga { get; set; }
}

public class ObtieneExpedienteHandler : IRequestHandler<ObtenerDocumentoConsulta, DocumentoBase64Dto>
{
    private readonly IDocumentoService _documentoService;
    private readonly IWordsUtil _wordsUtil;
    private readonly IRutasChunkService _rutasChunkService;
    private readonly ISesionService _sesionService;
    private readonly ITramitesRepository _tramitesRepository;
    private readonly INasArchivo _clienteNas;
    private readonly ILogService _logService;
    public ObtieneExpedienteHandler(IDocumentoService documentoService, IWordsUtil wordsUtil, IRutasChunkService rutasChunkService,
        ISesionService sesionService, ITramitesRepository tramitesRepository, INasArchivo clienteNas, ILogService logService)
    {
        _documentoService = documentoService;
        _wordsUtil = wordsUtil;
        _rutasChunkService = rutasChunkService;
        _sesionService = sesionService;
        _tramitesRepository = tramitesRepository;
        _clienteNas = clienteNas;
        _logService = logService;
    }

    public async Task<DocumentoBase64Dto?> Handle(ObtenerDocumentoConsulta request, CancellationToken cancellationToken)
    {
        RegistrarLog(TipoMovimiento.Crear, JsonSerializer.Serialize(request), "Inicio descarga archivo");
        var archivos = await _tramitesRepository.ObtenerTramitePorIdModuloTipoAsync(Guid.Parse(request.Id), 2, "acuerdo");
        RegistrarLog(TipoMovimiento.Crear, JsonSerializer.Serialize(archivos), "Consulta rutas");
        string contenido = string.Empty;
        string nombreArchivo = string.Empty;
        if (archivos.FirstOrDefault() != null)
        {
            var archivo = archivos.FirstOrDefault();
            nombreArchivo = archivo.NombreArchivo;
            var ext = Path.GetExtension(nombreArchivo);

            try
            {
                contenido = _clienteNas.ObtenerArchivoComoBase64String(archivo.RutaCompleta).Base64;
                RegistrarLog(TipoMovimiento.Crear, JsonSerializer.Serialize(archivo.RutaCompleta), "Obtuvo Base 64");
            }
            catch (Exception ex)
            {
                var hist = await _rutasChunkService.RutasChunkPorModuloNombreArchivoAsync(nombreArchivo, archivo.CatOrganismoId, RutasChunkModulos.Tramite, archivo.AsuntoNeunId, 0, 0, archivo.AsuntoDocumentoId);
                RegistrarLog(TipoMovimiento.Crear, JsonSerializer.Serialize(archivo.RutaCompleta), "Obtuvo Base 64 del historia");
                contenido = hist?.Base64;
            }
            if(request.esDescarga != "true")
            {
                if (ext == ".doc" && !string.IsNullOrEmpty(contenido))
                {
                    var pdf = _wordsUtil.ConvertDocToPdf(System.Convert.FromBase64String(contenido));
                    contenido = Convert.ToBase64String(pdf);
                    nombreArchivo = nombreArchivo.Replace(ext, ".pdf");
                }
            }
            
        }
        return new DocumentoBase64Dto()
        {
            Base64 = contenido,
            NombreArchivo = nombreArchivo
        };
    }
    private void RegistrarLog(TipoMovimiento tipoMovimiento, string request, string accion, string? response = null)
    {
        _logService.RegistrarEvento(
        new DatosLog
        {
            TipoMovimiento = tipoMovimiento,
            IdUsuario = -1,//_sessionService.SesionActual.EmpleadoId,
            DatosEntrada = request,
            DatosSalida = response,
            ModuloOrigen = $"{GetType().Name} - {accion}"
        }).ConfigureAwait(false).GetAwaiter();
    }
}

