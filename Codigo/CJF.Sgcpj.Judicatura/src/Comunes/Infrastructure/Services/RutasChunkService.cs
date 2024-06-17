using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Repositories;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
public class RutasChunkService : IRutasChunkService
{
    private readonly IRutasChunkRepository _repository;
    private readonly INasArchivo _nasArchivo;
    private readonly ILogger<RutasChunkService> _logger;

    public RutasChunkService(IRutasChunkRepository repository,
                             INasArchivo nasArchivo,
                             ILogger<RutasChunkService> logger)
    {
        _repository = repository;
        _nasArchivo = nasArchivo;
        _logger = logger;
    }

    public async Task<DocumentoBase64Dto?> RutasChunkPorModuloAsync(string ruta, RutasChunkModulos modulo
        , long? asuntoNeunId = null, int? yearPromocion = null, int? numeroOrden = null, int? asuntoDocumentoId = null)
    {
        try
        {
            var archivoBase64 = _nasArchivo.ObtenerArchivoComoBase64String(ruta);

            if (archivoBase64 != null) return archivoBase64;

            var rutas = await _repository.GetRutasHistoricasPorModulo(modulo.ToString());
            if (rutas is null)
            {
                return null;
            }

            var segmentosRuta = ruta.Split('\\');
            var organismoId = segmentosRuta[segmentosRuta.Length - 2];
            var nombre = segmentosRuta[segmentosRuta.Length - 1];

            foreach (var r in rutas)
            {
                var nasResult = _nasArchivo.ObtenerArchivoComoBase64String($"{r.sRuta}\\{organismoId}\\{nombre}");
                if (nasResult != null)
                {
                    var vModulo = modulo.Equals(RutasChunkModulos.Oficialia) ? 1 : modulo.Equals(RutasChunkModulos.Tramite) ? 2 : 3;
                    await _repository.ActualizarRutaHistorica(r.kId, vModulo, int.Parse(organismoId), asuntoNeunId, yearPromocion, numeroOrden, asuntoDocumentoId);
                    return nasResult;
                }
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex.ToString());
        }

        return null;
    }
    public async Task<DocumentoBase64Dto?> RutasChunkPorModuloNombreArchivoAsync(string nombreArchivo, int catOrganismoId, RutasChunkModulos modulo
        , long? asuntoNeunId = null, int? yearPromocion = null, int? numeroOrden = null, int? asuntoDocumentoId = null)
    {
        try
        {
            var rutas = await _repository.GetRutasHistoricasPorModulo(modulo.ToString());
            if (rutas is null)
            {
                return null;
            }

            var organismoId = catOrganismoId;
            var nombre = nombreArchivo;

            foreach (var r in rutas)
            {
                DocumentoBase64Dto nasResult = new DocumentoBase64Dto();

                try
                {
                    nasResult = _nasArchivo.ObtenerArchivoComoBase64String($"{r.sRuta}\\{organismoId}\\{nombre}");
                    if (nasResult != null)
                    {
                        await _repository.ActualizarRutaHistorica(r.kId, modulo.Equals(RutasChunkModulos.Oficialia) ? 1 : 2, organismoId, asuntoNeunId, yearPromocion, numeroOrden, asuntoDocumentoId);
                        return nasResult;
                    }
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex.ToString());
                }

            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex.ToString());
        }

        return null;
    }
}
