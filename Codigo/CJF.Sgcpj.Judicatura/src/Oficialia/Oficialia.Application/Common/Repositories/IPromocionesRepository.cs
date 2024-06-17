using CJF.Sgcpj.Judicatura.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.InsertarExpediente;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtienePromocionesConFiltro;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Promociones.Consulta.ObtenerDetalleCargaMasiva;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Promociones.Consulta.ObtenerNumeroExpediente;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Promociones.Consulta.ObtenerPromocionesFiltros;
using CJF.Sgcpj.Judicatura.Oficialia.Domain.Entities;
using CJF.Sgcpj.Judicatura.Tramite.Application.Models;

namespace CJF.Sgcpj.Judicatura.Application.Common.Repositories;
public interface IPromocionesRepository
{
    Task<(List<Promocion>, MetaDataEstados)> ObtenerPromocionesConFiltro(ConsultaPaginada consultaPaginada);
    Task<List<DetalleIndicadores>> ObtenerIndicadoresPromocion(DateTime fechaInicial, DateTime fechaFinal, int origenId);
    Task<List<DetalleGruposMes>> ObtenerAsuntosMesPromocion(int empleadoId);
    Task<List<DetalleIntervalos>> ObtenerTiemposTurnos(DateTime fechaInicial, DateTime fechaFinal, int empleadoId);
    Task<List<PromocionDetalle>> ObtenerLecturaPromocion(long asuntoNeunId, int asuntoID, int yearPromocion, int numeroOrden, int catIdOrganismo, int numeroRegistro, int origenPromocion);
    Task<int> ObtenerCalculoRegistro(int catOrganismoId, int yearPromocion, int statusReg);
    Task<DatosPromocion> AgregarPromocion(AgregarPromocion promocion);
    Task<long> EditarPromocion(EditarPromocion promocion);
    Task<bool> ValidarExpedienteRepetido(long asuntoNeunId, int catTipoAsuntoId);
    Task<bool> ActualizarArchivo(AgregarDocumento agregarDocumento);
    Task<DatosDocumento> GuardarDocumento(AgregarDocumento agregarDocumento);
    Task<DatosDocumento> GuardarCargaMasiva(CargaMasiva cargaMasiva);
    Task<string> RutaArchivo(string modulo);
    Task<long> RollBackArchivo(RollBackArchivo rollBackArchivo);
    Task<long> RollBackAnexo(RollBackAnexo rollBackAnexo);
    Task<List<ArchivosAnexos>> ObtenerArchivosyAnexos(long asuntoNeunId, int numeroOrden, int yearPromocion, int catIdOrganismo, int origen, int modulo, int asuntoDocumentoId);
    Task<(List<PromocionDetalleTablero>,List<AnexosLista>)> ObtenerPromocionDetalleTablero(int catOrganismoId, long asuntoNeunId, int usuariId, int origen, int numeroOrden, int yearPromocion, long? kIdElectronica);
    Task<(List<PromocionDetalleTablero>,List<AnexosLista>)> ObtenerPromocionDetalleTableroElectronicas(int catOrganismoId, long asuntoNeunId, int usuariId, int origen, int numeroOrden, int yearPromocion, long? kIdElectronica);
    Task<string> EliminarPromocion(EliminarPromocion promocion);
    Task<int> InsertarPromocion(Models.InsertarPromocion promocion);
    Task<ResultadoInsertarExpedienteDto> InsertarExpediente(Models.InsertarExpediente expediente);
    Task<ExpedienteDto> ObtenerNumeroExpediente(ObtenerNumeroExpediente expediente);
    Task<ObtenerDetalleAlertaDto?> ObtenerDetalleAlerta(ObtenerDetalleCargaMasivaRequest detalleAlerta);
    Task<bool>RelacionPromocionElectronica(long asuntoNeunId, int numeroOrden, int catOrganismoId, long? idPromocion, int? origen, int empleadoId, bool? conExpediente);
    Task<(List<FiltroSecretario>, List<FiltroOrigen>, List<FiltroCapturo>)> ObtenerFiltrosPromociones(ObtienePromocionesFiltrosConsulta request);
    Task<List<InfoDocumentos>> ObtenerPromociones(Guid promocionId);
    Task<bool> ActualizaEstadoFirmaPromocion(string? promocionGuid, int catOrganismo, long AsuntoNeun, string nombreArchivo);
}
