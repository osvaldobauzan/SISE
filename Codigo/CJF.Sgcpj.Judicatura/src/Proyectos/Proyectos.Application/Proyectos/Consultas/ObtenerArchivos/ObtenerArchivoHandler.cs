using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Common.Domain.Common;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Proyectos.Application.Common.Helpers;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerArchivos;

public class ObtenerArchivoHandler : IRequestHandler<ObtenerArchivo, DocumentoBase64Dto>
{
    private readonly IArchivosRepository _archivosRepository;
    private readonly INasArchivo _clienteNas;
    private readonly IWordsUtil _wordsUtil;
    private readonly CryptographicHelper _cryptographicHelper;
    private readonly ILogger<ObtenerArchivoHandler> _logger;

    public ObtenerArchivoHandler(IArchivosRepository archivosRepository, INasArchivo clienteNas, IWordsUtil wordsUtil, CryptographicHelper cryptographicHelper, ILogger<ObtenerArchivoHandler> logger)
    {
        _archivosRepository = archivosRepository;
        _clienteNas = clienteNas;
        _wordsUtil = wordsUtil;
        _cryptographicHelper = cryptographicHelper;
        _logger = logger;
    }

    public async Task<DocumentoBase64Dto?> Handle(ObtenerArchivo request, CancellationToken cancellationToken)
    {
        try
        {
            var archivos = await _archivosRepository.ObtenerArchivoDTO(request.Id);
            string contenido = string.Empty;
            if (archivos.FirstOrDefault() == null)
            {
                throw new FileNotFoundException("El documento no está registrado en la BD");
            }

            var archivo = archivos.FirstOrDefault();
            string nombreArchivo = archivo.sNombreArchivo;
            var organismoId = archivo.CatOrganismoId;
            var anioRuta = archivo.sAnioRuta;
            var ext = Path.GetExtension(nombreArchivo);
            List<RutasNas> rutas = new List<RutasNas>();

            try
            {
                rutas = await _archivosRepository.RutasPorModuloHistorico("Proyectos");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

            foreach (var r in rutas)
            {
                var path = r.Sruta + "\\" + organismoId + "\\" + nombreArchivo;
                try
                {
                    var archivo64 = _clienteNas.ObtenerArchivoComoBase64String(path);
                    if (archivo64 != null)
                    {
                        contenido = archivo64.Base64;
                        break;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    throw;
                }
            }

            if (string.IsNullOrEmpty(contenido))
            {
                throw new FileNotFoundException("El archivo no se encuentra en la NAS", nombreArchivo);
            }

            var keys = new ParametrosClave
            {
                Expediente = archivo.AsuntoNeunId.ToString(),
                Fecha = archivo.fFechaAlta.Date.ToString(),
                Usuarios = new string[]
                {
                    archivo.UserNameSecretario, archivo.UserNameTitular
                }
            };

            var decryptedData = _cryptographicHelper.DecryptData(Convert.FromBase64String(contenido), keys);
            contenido = Convert.ToBase64String(decryptedData);

            if ((ext == Constants.SISE3_EXTENSIONWORD2007FILE || ext == Constants.SISE3_EXTENSIONWORDFILE) && !string.IsNullOrEmpty(contenido) && !request.Descargar)
            {
                var pdf = _wordsUtil.ConvertDocToPdf(Convert.FromBase64String(contenido));
                contenido = Convert.ToBase64String(pdf);
                nombreArchivo = nombreArchivo.Replace(ext, ".pdf");
            }

            return new DocumentoBase64Dto()
            {
                Base64 = contenido,
                NombreArchivo = nombreArchivo
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
