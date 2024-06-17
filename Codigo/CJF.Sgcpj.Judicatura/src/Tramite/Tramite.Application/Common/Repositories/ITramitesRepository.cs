using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerFiltrosTramite;
using CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerDetalleTramite;
using CJF.Sgcpj.Judicatura.Tramite.Application.Models;
using CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerDatosAudiencia;
namespace CJF.Sgcpj.Judicatura.Tramite.Application.Common.Repositories;

public interface ITramitesRepository
{
    Task<(List<CJF.Sgcpj.Judicatura.Tramite.Domain.Entities.Tramite>, MetaDataEstadosTramite)> ObtenerTramitesConFiltro(ConsultaPaginadaTramite consultaPaginada);
    Task<IEnumerable<Ruta>?> RutaArchivo(string modulo);
    Task<DatosDocumento> GuardarDocumentoAcuerdo(AgregarDocumento agregarDocumento);
    Task<bool> ActualizaEstadoAcuerdo(EstadoAcuerdo acuerdo);
    Task<bool> ActualizaEstadoAcuerdoBre(string nombreSp, EstadoAcuerdo acuerdo);
    Task<bool> EliminarAcuerdo(EliminarAcuerdo acuerdo);
    Task<(List<CabeceraTramite>, List<Promociones>, List<Partes>, List<Oficio>)> ObtenerDetalleTramiteAsync(ObtieneDetalleTramiteConsulta consultaPaginada);
    Task<List<ArchivosAnexos>> ObtenerArchivosyAnexos(long asuntoNeunId, int numeroOrden, int yearPromocion, int catIdOrganismo, int origen, int modulo, int asuntoDocumentoId);
    Task<(List<ObtieneFiltroSecretario>, List<ObtieneFiltroOrigen>, List<ObtieneFiltroTipoAsunto>, List<ObtieneFiltroCapturo>, List<ObtieneFiltroPreautorizo>,
        List<ObtieneFiltroAutorizo>, List<ObtieneFiltroCancelo>)> ObtenerFiltroTramite(ObtieneFiltroTramiteConsulta request);
    Task<bool> GetStatusUNC(int catIdOrganismo);
    Task<string> ObtenerNumeroOficio(int catOrganismoId, long asuntoNeunId, int asuntoDocumentoId, int anexoParteId);
    Task<IEnumerable<Models.Tramite>> ObtenerTramitePorIdModuloTipoAsync(Guid id, int modulo, string tipoArchivo);
    Task<bool> ActualizaEstadoOficio(EstadoOficioRoot oficioRoot);
    Task<bool> ActualizaDeterminacion(DeterminacionAcuerdoRoot determinacionRoot);
    Task<List<InfoDocumentos>> ObtenerAcuerdosOficios(Guid acuerdoGuid);
    Task<(List<TiposAsuntoDTO>, List<TiposAudienciaDTO>, List<ResultadosAudienciaDTO>, List<AudienciasDTO>)> ObtenerDatosAudiencia(ObtenerDatosAudienciaFiltro request);

}
