using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Repositories;
public interface IRutasChunkRepository
{
    Task<IEnumerable<RutaHistorica>?> GetRutasHistoricasPorModulo(string modulo);
    Task<bool> ActualizarRutaHistorica(int idRuta, int modulo, int catOrganismoId, long? asuntoNeunId, int? yearPromocion, int? numeroOrden, int? asuntoDocumentoId);
    Task<IEnumerable<RutaNas>?> RutaArchivo(string modulo);
}
