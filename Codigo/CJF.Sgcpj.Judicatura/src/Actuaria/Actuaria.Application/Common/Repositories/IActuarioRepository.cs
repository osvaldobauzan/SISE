using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models.Actuario;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
public interface IActuarioRepository
{
    // ActuarioNotificacionesDto, ActuarioNotificacionesMetaDataEstadosDto
    Task<(List<ActuarioNotificaciones> datos, ActuarioNotificacionesMetaDataEstados metadatos)> ObtenerActuarioNotificacionesConFiltro(ConsultaPaginadaNotificaciones consultaPaginada);
    Task<List<ConsultaOficioActuario>> ListaConsultaOficioPorActuario(long asuntoNeunId, int asuntoDocumentoId, long actuarioId, int catOrganismoId);
    Task<List<ConsultaOficioActuario>> ListaConsultaOficioPorActuarioPorFecha(long actuarioId, string fechaInicio, string fechaFin, int catOrganismoId);
}
