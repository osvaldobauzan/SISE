using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
public interface IRutasChunkService
{
    Task<DocumentoBase64Dto?> RutasChunkPorModuloAsync(string ruta, RutasChunkModulos modulo
        , long? asuntoNeunId = null, int? yearPromocion = null, int? numeroOrden = null, int? asuntoDocumentoId = null);
    Task<DocumentoBase64Dto?> RutasChunkPorModuloNombreArchivoAsync(string nombreArchivo, int catOrganismoId, RutasChunkModulos modulo
        , long? asuntoNeunId = null, int? yearPromocion = null, int? numeroOrden = null, int? asuntoDocumentoId = null);
}
