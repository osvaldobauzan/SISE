using System.Data;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Persistence;
using CJF.Sgcpj.Judicatura.Sentencias.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Sentencias.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Sentencias.Application.Sentencias.Consultas.ObtenerSentencias;
using CJF.Sgcpj.Judicatura.Sentencias.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Sentencias.Infrastructure.Persistence.Repositories;

public class SentenciasRepository : ISentenciasRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<SentenciasRepository> _logger;

    public SentenciasRepository(ApplicationDbContext dbContext, IMapper mapper, ILogger<SentenciasRepository> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<SentenciaDto>> ObtenerSentencias(ConsultaSentencias filtroSentencias)
    {
        try
        {
            var listadoSentencias = new List<SentenciaDto>();
            var listaParametros = new[]
            {
                new SqlParameter("@pi_CatOrganismoId", filtroSentencias.CatOrganismoId),              
                new SqlParameter("@pi_FechaPublicacionIni", filtroSentencias.Fecha),
                new SqlParameter("@pi_FechaPublicacionFin", filtroSentencias.FechaFin),
                new SqlParameter("@pi_FechaPublicacion", null)
            };

            var datos = await _dbContext.ExecuteStoredProc<SentenciaItemTablero>("SISE3.pcTableroSentencias", listaParametros.ToArray());
            listadoSentencias = datos.Select(x => _mapper.Map<SentenciaDto>(x)).ToList();

            return await Task.FromResult(listadoSentencias);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<RegistroSentencia> GuardarSentencia(Sentencia sentencia)
    {
        try
        {
            var result = new Sentencia();
            var listaParametros = new[]
            {
                new SqlParameter("@pi_AsuntoNeunId", sentencia.AsuntoNeunId),
                new SqlParameter("@pi_AsuntoId", sentencia.AsuntoId),
                new SqlParameter("@pi_NombreDocumento", sentencia.NomArchivoReal),
                new SqlParameter("@pi_NombreArchivo", sentencia.NombreArchivo),
                new SqlParameter("@pi_ExtensionDocumento", sentencia.ExtensionDocumento),
                new SqlParameter("@pi_Contenido", sentencia.Contenido),
                new SqlParameter("@pi_TipoCuaderno", sentencia.TipoCuadernoId),
                new SqlParameter("@pi_FechaAcuerdo", sentencia.FechaAuto),
                new SqlParameter("@pi_TipoArchivo", sentencia.TipoArchivo),
                new SqlParameter("@pi_Sigilo", sentencia.Sigilo),
                new SqlParameter("@pi_SentenciaDefinitiva", sentencia.SentenciaDefinitiva),
                new SqlParameter("@pi_EsJDA", sentencia.EsJDA),
                new SqlParameter("@pi_TitularId", sentencia.TitularId),
                new SqlParameter("@pi_SecretarioPId", sentencia.SecretarioPId),
                new SqlParameter("@pi_SecretarioCId", sentencia.SecretarioCId),
                new SqlParameter("@pi_ActuarioId", sentencia.ActuarioId),
                new SqlParameter("@pi_Resumen", sentencia.Resumen),
                new SqlParameter("@pi_CatOrganismoId", sentencia.CatOrganismoId),
                new SqlParameter("@pi_EstatusArchivo", sentencia.EstatusArchivo),
                new SqlParameter("@pi_IPUsuario", sentencia.IPUsuario),
                new SqlParameter("@pi_UsuarioCaptura", sentencia.UsuarioCaptura),
                new SqlParameter("@pi_IdOrigen", sentencia.IdOrigen),
                new SqlParameter("@pi_TipoOrigen", sentencia.TipoOrigen),
                new SqlParameter("@pi_VersionPub", sentencia.VersionPublica),
                new SqlParameter("@pi_InfoReservada", sentencia.InfoReservada),
                new SqlParameter("@pi_Perspectiva", sentencia.Perspectiva),
                new SqlParameter("@pi_Criterio", sentencia.Criterio),
                new SqlParameter("@pi_Trascedental", sentencia.Trascedental),
                new SqlParameter("@pi_EsTratadoInternacional", sentencia.EsTratadoInternacional),
                new SqlParameter("@pi_TipoActo", sentencia.TipoActo),
                new SqlParameter("@pi_NombreTratado", sentencia.NombreTratado),
                new SqlParameter("@pi_Derecho", sentencia.Derechos),
                new SqlParameter("@pi_SubClasificacionDerecho", sentencia.DerechoEspecifico),
                new SqlParameter("@pi_TipoActoOtro", sentencia.TipoActoOtro),
                new SqlParameter("@pi_SolicitudReparacion", sentencia.SolicitudReparacion),
                new SqlParameter("@pi_SolicitudReparacionOpcion", sentencia.SolicitudReparacionOpcion),
                new SqlParameter("@pi_SolicitudReparacionOtro", sentencia.SolicitudReparacionOtro),
                new SqlParameter("@pi_LecturaFacil", sentencia.LecturaFacil == -1 ? null : (int)sentencia.LecturaFacil),
                new SqlParameter("@pi_TemaEquidadGenero", sentencia.TemaEquidadGenero),
                new SqlParameter("@pi_AplicacionEfectivaDerechoMujeres", sentencia.AplicacionEfectivaDerechoMujeres == -1 ? null : (int)sentencia.AplicacionEfectivaDerechoMujeres),
                new SqlParameter("@pi_TemaAsuntosInternacionales", sentencia.TemaAsuntosInternacionales),
                new SqlParameter("@pi_AplicacionCriteriosPersGenero", sentencia.AplicaCriteriosPerspectivaGenero == -1 ? DBNull.Value : sentencia.AplicaCriteriosPerspectivaGenero),
                new SqlParameter("@pi_CriterioPerspecGenAplicado", sentencia.CriterioPerspectivaGeneroAplicado),
                new SqlParameter("@pi_Justificacion", sentencia.Justificacion),
                new SqlParameter("@pi_PromocionesDeterminacion", sentencia.PromocionesDeterminaciones.Any() ? sentencia.PromocionesDeterminaciones?.Select(x => x.ToSqlDataRecord()) : null)
                {
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "PromocionesDeterminacion_type"
                }
            };

            var datos = await _dbContext.ExecuteStoredProc<RegistroSentencia>("SISE_piDocumentosCargaPanel", listaParametros.ToArray());

            return datos.FirstOrDefault();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<bool> GuardarSentenciaVersionPublica(SentenciaVP sentenciaVP)
    {
        try
        {
            var listaParametros = new[]
            {
                new SqlParameter("pi_AsuntoNeunId", sentenciaVP.AsuntoNeunId),
                new SqlParameter("pi_NumeroOrden", sentenciaVP.NumeroOrden),
                new SqlParameter("pi_SintesisOrden", sentenciaVP.SintesisOrden),
                new SqlParameter("pi_TipoOrigen", sentenciaVP.TipoOrigen),
                new SqlParameter("pi_EmpleadoId", sentenciaVP.UsuarioCaptura),
                new SqlParameter("pi_DelincuenciaOrganizada", sentenciaVP.DelincuenciaOrganizada),
                new SqlParameter("pi_Confidencial", sentenciaVP.Confidencial),
                new SqlParameter("pi_FraccionConfidencial", (object)sentenciaVP.FraccionConfidencial ?? DBNull.Value),
                new SqlParameter("pi_MotivacionConfidencial", (object)sentenciaVP.MotivacionConfidencial ?? DBNull.Value),
                new SqlParameter("pi_ObservacionesConfidencial", (object)sentenciaVP.ObservacionesConfidencial ?? DBNull.Value),
                new SqlParameter("pi_Reservada", sentenciaVP.Reservada),
                new SqlParameter("pi_FraccionReservada", (object)sentenciaVP.FraccionReservada ?? DBNull.Value),
                new SqlParameter("pi_MotivacionReservada", (object)sentenciaVP.MotivacionReservada ?? DBNull.Value),
                new SqlParameter("pi_ObservacionesReservada", (object)sentenciaVP.ObservacionesReservada ?? DBNull.Value)
            };

            var datos = await _dbContext.ExecuteStoredProcNonQuery("piVersionPublica", listaParametros.ToArray());
            return datos > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<string> GuardarBitacora(RegistroBitacora bitacora)
    {
        try
        {
            var listaParametros = new[]
            {
                new SqlParameter("@pi_CatOrganismoId", bitacora.CatOrganismoId),
                new SqlParameter("@pi_AsuntoNeunId", bitacora.AsuntoNeunId),
                new SqlParameter("@pi_TamanioArchivo", bitacora.TamanioArcivo),
                new SqlParameter("@pi_TipoArchivo", bitacora.Carpeta),
                new SqlParameter("@pi_NombreArchivo", bitacora.NombreArchvo),
                new SqlParameter("@pi_FechaInicia", bitacora.FechaInicia),
                new SqlParameter("@pi_FechaTermina", bitacora.FechaInicia),
                new SqlParameter("@pi_MensajeError", ""),
                new SqlParameter("@pi_IpHost", bitacora.IpHost),
                new SqlParameter("@pi_IpCliente", bitacora.IpCliente)
            };

            var datos = await _dbContext.ExecuteStoredProcObj<string>("usp_BitacoraArchivoFU_Ins", listaParametros.ToArray());
            return datos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<string> GuardarDeterminacionJudicial(RegistroDeterminacionJudicial determinacion)
    {
        try
        {
            var listaParametros = new[]
            {
                new SqlParameter("@nomArchivo", determinacion.NombreArchivo),
                new SqlParameter("@estatus", determinacion.IdEstatus),
                new SqlParameter("@ip", determinacion.Ip),
                new SqlParameter("@observaciones", determinacion.Observaciones),
                new SqlParameter("@AsuntoNeunId", determinacion.AsuntoNeunId),
                new SqlParameter("@NumeroOrden", determinacion.NumeroOrden),
                new SqlParameter("@SintesisOrden", determinacion.SintesisOrden),
                new SqlParameter("@PraArchivo", determinacion.ArchivoExtension),
                new SqlParameter("@fojas", determinacion.Fojas)
            };

            var datos = await _dbContext.ExecuteStoredProcObj<string>("usp_EXPE_DeterminacionesJudiciales_File_Upd", listaParametros.ToArray());
            return datos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<bool> GuardarRelacionSentenciaSISE3(SentenciaSISE3 relacionarSentencia)
    {
        try
        {
            var listaParametros = new[]
            {
                new SqlParameter("@pi_CatOrganismoId", relacionarSentencia.CatOrganismoId),
                new SqlParameter("@pi_AsuntoNeunId", relacionarSentencia.AsuntoNeunId),
                new SqlParameter("@pi_SintesisOrden", relacionarSentencia.SintesisOrden),
                new SqlParameter("@pi_NumeroOrden", relacionarSentencia.NumeroOrden),
            };

            var datos = await _dbContext.ExecuteStoredProcNonQuery("SISE3.piTableroSentencias_SISE2", listaParametros.ToArray());
            return datos > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task ActualizaUsuarioAsuntosDocumentos(UsuarioAsuntosDocumentos parametro)
    {
        try
        {
            var listaParametros = new[]
            {
                new SqlParameter("@pi_AsuntoNeunId", parametro.AsuntoNeunId),
                new SqlParameter("@pi_AsuntoDocumentoId", parametro.AsuntoDocumentoId),
                new SqlParameter("@pi_TipoUpdate", parametro.TipoUpdate),
                new SqlParameter("@pi_Valor", parametro.Valor)
            };
            var datos = await _dbContext.ExecuteStoredProcNonQuery("dbo.usp_AsuntosDocumentosTitularSecretario", listaParametros.ToArray());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task ActualizaAutorizacionDocumentos(AutorizacionDocumentos parametro)
    {
        try
        {
            var listaParametros = new[]
            {
                new SqlParameter("@pi_AsuntoNeunId", parametro.AsuntoNeunId),
                new SqlParameter("@pi_AsuntoID", parametro.AsuntoID),
                new SqlParameter("@pi_AsuntoDocumentoId", parametro.AsuntoDocumentoId),
                new SqlParameter("@pi_CatAutorizacionDocumentosId", parametro.AutorizacionDocumentosId)
            };

            var datos = await _dbContext.ExecuteStoredProcNonQuery("dbo.usp_AsuntosDocumentosCambiaStatusUpd", listaParametros.ToArray());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
