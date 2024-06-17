using CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Comandos.ActualizarEstadoProyectoComando;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Comandos.SubirProyectoConAudienciaComando;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerMotivosPartes;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerUltimaVersionProyecto;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerVersionesProyecto;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ValidarExpediente;
using Proyectos.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Repositories;

public interface IProyectosRepository
{
    Task<(List<Domain.Entities.Proyecto>, MetaDataEstadoProyecto)> ObtenerProyectosConFiltro(ConsultaPaginadaProyectos consultaPaginada);

    Task<ProyectoConAudienciaDto> SubirProyectoConAudiencia(SubirProyectoConAudiencia subirProyecto);

    Task<bool> EliminarProyectoArchivo(RollbackProyectoArchivo rollback);

    Task<ValidacionExpedienteDto> ValidarExpediente(ValidarExpediente validarExpediente);

    Task<ListadoVersionesDto> ObtenerVersionesProyecto(ObtenerListadoVersiones proyecto);

    Task<EstadoProyectoActualizadoDto> ActualizarEstadoProyecto(ActualizarEstadoProyecto actualizarEstadoProyecto);

    Task<bool> EliminarCorreccionesArchivo(RollbackProyectoArchivo rollback);

    Task<VersionDto> ObtenerUltimaVersionProyecto(ObtenerVersion proyecto);

    Task<ListadoMotivosPartesDto> ObtenerMotivosPartes(long idProyecto);

    Task<bool> ReasignarSecretario(ReasignarSecretario reasignar);
}
