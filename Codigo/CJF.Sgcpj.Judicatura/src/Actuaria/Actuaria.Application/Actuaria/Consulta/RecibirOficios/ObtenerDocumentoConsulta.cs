using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Repositories;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.RecibirOficios;
public record class ObtenerDocumentoConsulta : IRequest<DocumentoBase64Dto>
{
    public string Id { get; set; }
    public string tipoArchivo { get; set; }
}

public class ObtieneExpedienteHandler : IRequestHandler<ObtenerDocumentoConsulta, DocumentoBase64Dto>
{
    private readonly IDocumentoService _documentoService;
    private readonly IWordsUtil _wordsUtil;
    private readonly IRutasChunkService _rutasChunkService;
    private readonly ISesionService _sesionService;
    private readonly ITramitesRepository _tramitesRepository;
    private readonly INasArchivo _clienteNas;
    public ObtieneExpedienteHandler(IDocumentoService documentoService, IWordsUtil wordsUtil, IRutasChunkService rutasChunkService,
        ISesionService sesionService, ITramitesRepository tramitesRepository, INasArchivo clienteNas)
    {
        _documentoService = documentoService;
        _wordsUtil = wordsUtil;
        _rutasChunkService = rutasChunkService;
        _sesionService = sesionService;
        _tramitesRepository = tramitesRepository;
        _clienteNas = clienteNas;
    }

    public async Task<DocumentoBase64Dto?> Handle(ObtenerDocumentoConsulta request, CancellationToken cancellationToken)
    {
        var archivos = await _tramitesRepository.ObtenerTramitePorIdModuloTipoAsync(Guid.Parse(request.Id), 2, request.tipoArchivo);
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
            }
            catch (Exception ex)
            {
                var hist = await _rutasChunkService.RutasChunkPorModuloNombreArchivoAsync(nombreArchivo, archivo.CatOrganismoId, RutasChunkModulos.Tramite, archivo.AsuntoNeunId, 0, 0, archivo.AsuntoDocumentoId);
                contenido = hist?.Base64;
            }

        }
        return new DocumentoBase64Dto()
        {
            Base64 = contenido,
            NombreArchivo = nombreArchivo
        };
    }
}