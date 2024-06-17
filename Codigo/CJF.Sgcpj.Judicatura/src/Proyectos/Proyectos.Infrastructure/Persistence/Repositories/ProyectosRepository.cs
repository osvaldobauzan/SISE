using System.Data;
using System.Text.Json;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Persistence;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Comandos.ActualizarEstadoProyectoComando;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Comandos.SubirProyectoConAudienciaComando;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerMotivosPartes;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerUltimaVersionProyecto;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerVersionesProyecto;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ValidarExpediente;
using CJF.Sgcpj.Judicatura.Proyectos.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Proyectos.Application.Common.Models;
using Proyectos.Domain.Entities;

namespace CJF.Sgcpj.Judicatura.Proyectos.Infrastructure.Persistence.Repositories;

public class ProyectosRepository : IProyectosRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<ProyectosRepository> _logger;
    private readonly ILogService _logService;

    public ProyectosRepository(ApplicationDbContext dbContext, IMapper mapper, ILogger<ProyectosRepository> logger, ILogService logService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
        _logService = logService;
    }

    public async Task<(List<Proyecto>, MetaDataEstadoProyecto)> ObtenerProyectosConFiltro(ConsultaPaginadaProyectos consultaPaginada)
    {
        try
        {
            consultaPaginada.Texto = consultaPaginada?.Texto?.ToLower();
            return await ObtenerProyectosAsync(consultaPaginada);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    private async Task<(List<Proyecto>, MetaDataEstadoProyecto)> ObtenerProyectosAsync(ConsultaPaginadaProyectos consultaPaginada)
    {
        try
        {
            var metadata = new MetaDataEstadoProyecto();
            var listaTablero = new List<Proyecto>();

            var listaTableroSql = new[]
            {   new SqlParameter("@pi_CatOrganismoId", consultaPaginada.OrganismoID),
            new SqlParameter("@pi_TamanoPagina", consultaPaginada.RegistrosPorPagina),
            new SqlParameter("@pi_NumeroPagina", consultaPaginada.Pagina),
            new SqlParameter("@pi_FechaPresentacionIni", consultaPaginada.FechaInicial),
            new SqlParameter("@pi_FechaPresentacionFin", consultaPaginada.FechaFinal),
            new SqlParameter("@pi_UsuariId", consultaPaginada.UsuarioId),
            new SqlParameter("@pi_Texto", consultaPaginada.Texto),
            new SqlParameter("@pi_OrdenarPor", consultaPaginada.OrdenarPor),
            new SqlParameter("@pi_TipoOrden", consultaPaginada.Descendente),
            new SqlParameter("@pi_FiltroTipo", consultaPaginada.Estado)
        };

            var (metadatos, datos) = await _dbContext.ExecuteStoredProc<ContadoresEstadosProyectosTablero, ProyectosItemTablero>("SISE3.pcTableroProyecto", listaTableroSql.ToArray());
            listaTablero = datos.Select(x => _mapper.Map<Proyecto>(x)).ToList();

            metadata.TotalSinProyecto = metadatos.FirstOrDefault()?.TotalSinProyecto ?? 0;
            metadata.TotalNoAprobado = metadatos.FirstOrDefault()?.TotalNoAprobado ?? 0;
            metadata.TotalAprobado = metadatos.FirstOrDefault()?.TotalAprobado ?? 0;
            metadata.TotalParaRevision = metadatos.FirstOrDefault()?.TotalParaRevision ?? 0;
            metadata.TotalConAjustes = metadatos.FirstOrDefault()?.TotalConAjustes ?? 0;
            metadata.TotalProyectos = metadatos.FirstOrDefault()?.TotalProyectos ?? 0;

            return await Task.FromResult((listaTablero, metadata));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<ProyectoConAudienciaDto> SubirProyectoConAudiencia(SubirProyectoConAudiencia subirProyecto)
    {
        try
        {
            RegistrarLog(TipoMovimiento.Crear, subirProyecto.EmpleadoId, JsonSerializer.Serialize(subirProyecto), "Inicio registrar proyecto");

            var proyectoConAudienciaDto = new ProyectoConAudienciaDto();
            var listaParametros = new[]
            {   new SqlParameter("@pi_CatOrganismoId", subirProyecto.CatOrganismoId),
                new SqlParameter("@pi_AsuntoNeunId", subirProyecto.AsuntoNeunId),
                new SqlParameter("@pi_iTitular", subirProyecto.TitularId),
                new SqlParameter("@pi_iSecretario", subirProyecto.SecretarioId),
                new SqlParameter("@pi_iTipoSentenciaId", subirProyecto.TipoSentenciaId),
                new SqlParameter("@pi_sSintesis", subirProyecto.Sintesis),
                new SqlParameter("@pi_iRegistroEmpleadoId", subirProyecto.EmpleadoId),
                new SqlParameter("@pi_sNombreArchivoReal", subirProyecto.NombreArchivo),
                new SqlParameter("@pi_sIPUsuario", subirProyecto.IpUsuario),
                new SqlParameter("@pi_TableroPartes", subirProyecto.Motivos?.Select(x => x.ToSqlDataRecord()))
                {
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "SISE3.TableroInsertaProyecto_type"
                }
            };

            var datos = await _dbContext.ExecuteStoredProc<ProyectoConAudiencia>("SISE3.piTableroProyectoInsertar", listaParametros.ToArray());
            proyectoConAudienciaDto = datos.Select(x => _mapper.Map<ProyectoConAudienciaDto>(x)).ToList().FirstOrDefault();

            RegistrarLog(TipoMovimiento.Crear, subirProyecto.EmpleadoId, JsonSerializer.Serialize(subirProyecto), "Fin registrar proyecto", JsonSerializer.Serialize(proyectoConAudienciaDto));

            return await Task.FromResult(proyectoConAudienciaDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            RegistrarLog(TipoMovimiento.Crear, subirProyecto.EmpleadoId, JsonSerializer.Serialize(subirProyecto), "Error registrar proyecto", ex.Message);
            throw;
        }
    }

    public async Task<bool> EliminarProyectoArchivo(RollbackProyectoArchivo rollback)
    {
        try
        {
            RegistrarLog(TipoMovimiento.Crear, rollback.EmpleadoId, JsonSerializer.Serialize(rollback), "Inicio rollback proyecto");

            var listaParametros = new[]
            {
                new SqlParameter("@pi_pkProyectoId", rollback.ProyectoId),
                new SqlParameter("@iBajaEmpleadoId", rollback.EmpleadoId)
            };

            var result = await _dbContext.ExecuteStoredProcNonQuery("[SISE3].[peTableroProyectoCancelaVersion]", listaParametros.ToArray());

            RegistrarLog(TipoMovimiento.Crear, rollback.EmpleadoId, JsonSerializer.Serialize(rollback), "Fin rollback proyecto", result.ToString());

            return await Task.FromResult(result > 0);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            RegistrarLog(TipoMovimiento.Crear, rollback.EmpleadoId, JsonSerializer.Serialize(rollback), "Error registrar proyecto", ex.Message);
            throw;
        }
    }

    public async Task<ValidacionExpedienteDto> ValidarExpediente(ValidarExpediente validarExpediente)
    {
        try
        {
            var validacionExpediente = new ValidacionExpedienteDto();
            var listaParametros = new[]
            {
            new SqlParameter("@pi_CatOrganismoId", validarExpediente.CatOrganismoId),
            new SqlParameter("@pi_CatCuadernoId", validarExpediente.CatCuadernoId),
            new SqlParameter("@pi_AsuntoAlias", validarExpediente.NumeroExpediente),
            new SqlParameter("@pi_CatTipoAsuntoId", validarExpediente.CatTipoAsuntoId),
            new SqlParameter("@pi_AsuntoNeunId", validarExpediente.AsuntoNeunId),
        };

            var datos = await _dbContext.ExecuteStoredProc<ValidacionExpediente>("SISE3.pcTableroProyectoValidaIngesta", listaParametros.ToArray());
            validacionExpediente.PuedeIngestar = datos.FirstOrDefault().PuedeIngestar;
            validacionExpediente.Motivos = datos.FirstOrDefault().MotivoNoIngesta is null ? new List<string>() : datos.FirstOrDefault().MotivoNoIngesta.Split(",").ToList();

            return await Task.FromResult(validacionExpediente);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<ListadoVersionesDto> ObtenerVersionesProyecto(ObtenerListadoVersiones proyecto)
    {
        try
        {
            var listado = new ListadoVersionesDto();
            var listaParametros = new[]
            {
            new SqlParameter("@pi_AsuntoNeunId", proyecto.AsuntoNeunId),
        };

            var datos = await _dbContext.ExecuteStoredProc<ListadoVersiones>("SISE3.pcObtieneVersionesProyecto", listaParametros.ToArray());
            listado = datos.Select(x => _mapper.Map<ListadoVersionesDto>(x)).FirstOrDefault();
            if (listado is not null)
            {
                listado.Archivos = datos.Select(x => _mapper.Map<VersionDto>(x)).ToList();
            }

            return await Task.FromResult(listado);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<EstadoProyectoActualizadoDto> ActualizarEstadoProyecto(ActualizarEstadoProyecto actualizarEstadoProyecto)
    {
        try
        {
            var proyectoActualizado = new EstadoProyectoActualizadoDto();
            var listaParametros = new[]
            {
                new SqlParameter("@pi_pkProyectoId", actualizarEstadoProyecto.ProyectoId),
                new SqlParameter("@pi_ComentarioTitular", actualizarEstadoProyecto.Correcciones),
                new SqlParameter("@pi_ArchivoComentarios", actualizarEstadoProyecto.ArchivoCorrecciones),
                new SqlParameter("@pi_iEstado", actualizarEstadoProyecto.EstadoId),
                new SqlParameter("@pi_sIPUsuario", actualizarEstadoProyecto.IpUsuario),
                new SqlParameter("@pi_iRegistroEmpleadoId", actualizarEstadoProyecto.UsuarioId)
            };

            var datos = await _dbContext.ExecuteStoredProc<ProyectoActualizado>("SISE3.paTableroProyectoVersion", listaParametros.ToArray());
            proyectoActualizado = datos.Select(x => _mapper.Map<EstadoProyectoActualizadoDto>(x)).FirstOrDefault();

            return await Task.FromResult(proyectoActualizado);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<bool> EliminarCorreccionesArchivo(RollbackProyectoArchivo rollback)
    {
        try
        {
            var listaParametros = new[]
        {
            new SqlParameter("@pi_pkProyectoId", rollback.ProyectoId),
            new SqlParameter("@iBajaEmpleadoId", rollback.EmpleadoId)
        };

            var result = await _dbContext.ExecuteStoredProcNonQuery("SISE3.peTableroProyectoCancelaCorreccion", listaParametros.ToArray());

            return await Task.FromResult(result > 0);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    private async void RegistrarLog(TipoMovimiento tipoMovimiento, long idUsuario, string request, string accion, string? response = null)
    {
        _logService.RegistrarEvento(new DatosLog { TipoMovimiento = tipoMovimiento, IdUsuario = idUsuario, DatosEntrada = request, DatosSalida = response, ModuloOrigen = $"{GetType().Name} - {accion}" }).ConfigureAwait(false).GetAwaiter();
    }

    public async Task<ListadoMotivosPartesDto> ObtenerMotivosPartes(long idProyecto)
    {
        try
        {
            var listaParametros = new[]
            {
                new SqlParameter("@pkProyectoId", idProyecto)
            };

            var datos = await _dbContext.ExecuteStoredProc<MotivosParte>("SISE3.pcTableroProyectoPartesMotivoSentidoDesc", listaParametros.ToArray());
            var listado = datos.Select(x => _mapper.Map<Application.Proyectos.Consultas.ObtenerMotivosPartes.MotivosParteDto>(x)).ToList();
            var response = new ListadoMotivosPartesDto
            {
                Motivos = listado
            };

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<VersionDto> ObtenerUltimaVersionProyecto(ObtenerVersion proyecto)
    {
        try
        {
            var listado = new VersionDto();
            var listaParametros = new[]
            {
                new SqlParameter("@pi_AsuntoNeunId", proyecto.AsuntoNeunId)
            };

            var result = await _dbContext.ExecuteStoredProc<ListadoVersiones>("[SISE3].[pcObtieneUltimaVersionProyecto]", listaParametros.ToArray());
            listado = result.Select(x => _mapper.Map<VersionDto>(x)).FirstOrDefault();
            return await Task.FromResult(listado);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<bool> ReasignarSecretario(ReasignarSecretario reasignar)
    {
        try
        {
            var listaParametros = new[]
            {
                new SqlParameter("@pi_CatOrganismoId", reasignar.CatOrganismoId),
                new SqlParameter("@pi_UsuarioId", reasignar.EmpleadoId),
                new SqlParameter("@pi_pkProyectoId", reasignar.ProyectosId),
                new SqlParameter("@pi_SecretarioNuevoId", reasignar.SecretarioNuevoId)
            };

            var result = await _dbContext.ExecuteStoredProcObj<bool>("[SISE3].[paTableroProyectoActualizaSecretario]", listaParametros.ToArray());
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
