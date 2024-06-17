using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.AgregarCOE;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.AgregarSintesisManual;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.DetalleNotificacionesFiltros;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.DetalleSintesis;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerSintesisManual;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Actuaria.Domain.Entities;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
public interface IActuariaRepository
{
    Task<List<DiferenciasTiempos>> ObtenerDetalleIntervalosTiempos(long empleadoId, DateTime fechaInicial, DateTime fechaFinal);
    Task<List<ActuarioDetalleLista>> ObtenerDetalleConteos(int catOrganismoId, DateTime fechaInicial, DateTime fechaFinal);
    Task<List<NotificacionesPorTipoYMes>> ObtenerNotificacionesPorTipoYMes(int catOrganismoId, long filtroActuarioId, DateTime fechaInicial, DateTime fechaFinal);
    Task<List<NotificacionesPorTipoYSemana>> ObtenerNotificacionesPorTipoYSemana(int catOrganismoId, long filtroActuarioId, DateTime fechaInicial, DateTime fechaFinal, int mesSeleccionado);
    Task<(List<NotificacionesPendientesPorDias>, TotalNotificaciones, List<NotificacionesPorTipo>)> ObtenerNotificacionesPorTipoYPeriodo(int catOrganismoId, long filtroActuarioId, DateTime fechaInicial, DateTime fechaFinal);
    Task<(List<FiltroEstado>, List<FiltroContenido>)> ObtenenerFiltrosTablero(int catOrganismoId);
    Task<(List<Notificacion>, MetaDataEstados)> ObtenerNotificacionesConFiltro(ConsultaPaginada consultaPaginada);
    Task<bool> GuardarSintesisAcuerdo(GuardarSintesisAcuerdo guardarSintesisAcuerdo);
    Task<bool> EditarSintesisAcuerdo(EditarSintesisAcuerdo editarSintesisAcuerdo);
    Task<List<RecibirOficiosM>> ObtenerOficiosParaRecibir(int catOrganismoId, string folio, long empleadoId);
    Task<bool> RecibirOficios(int catOrganismoId, long empleadoId, OficiosType oficio);

    Task<bool> AgregarCOE(AgregarCOEDto coem, long empleadoId);
    Task<bool> AgregarSintesisManual(AgregarSintesisManualDto sintesis);
    Task<List<ObtenerAcuerdoM>> ObtenerAcuerdos(int CatOrganismoId, DateTime fechaInicio, DateTime fechaFin);
    Task<(List<DetalleAcuerdo>, List<Promociones>)> ObtenerDetalleAcuerdo(int CatOrganismoId, long AsuntoNeunId, int SintesisOrden, int AsuntoDocumentoId);
    Task<bool> AgregarActuario(AgregarActuarioM actuario, long empleadoId);
    Task<(DatosAsunto datosAsunto,List<NotificacionDetalle> datos, NotificacionDetalleMetaDataEstados metadatos)> ObtenerNotificacionesDetalleConFiltro(ConsultaPaginadaDetalle consultaPaginada);
    Task<(List<FiltroTipoParte>, List<FiltroTipoNotificacion>, List<FiltroActuario>)> ObternerFiltroDetalleNotificaciones(DetalleNotificacionesFiltrosConsulta request);
    Task<(string, long)> InsertarAcuse(SubirAcuseM subirAcuse);
    Task<string> InsertarArchivoAcuse(SubirAcuseArchivoM subirAcuse);
    Task<bool> AgregarActuarioMasivo(AgregarActuarioMasivoM actuario, long empleadoId);
    Task<RutasNas> RutaArchivo(string modulo);
    Task<ObtenerCOE> ConsultaCOE(long NotElectronica);
    Task<List<ArchivosAnexos>> ObtenerArchivosyAnexos(long asuntoNeunId, int numeroOrden, int yearPromocion, int catIdOrganismo, int origen, int modulo, int asuntoDocumentoId, int sintesisOden);
    Task<List<DetalleSintesisDTO>> ObtenerDetalleSintesis(FiltroDetalleSintesis sintesis);
    Task<List<ObtenerSintesisManualDTO>> ObtenerSintesisManual(DateTime fechaPublicacion, int CatOrganismoId);
    Task<bool> GetStatusUNC(int catIdOrganismo);
    Task<ResponseDatosGenerarFolioM> GenerarFoliosPartes(AgregarFoliosPartes info, long empleadoId);
    Task<PersonasAsunto> ObtenerPersonaAsuntoXidEmpleado(long asuntoNeunId, long parte);

}
