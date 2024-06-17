using System.Data;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.InsertarExpediente;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtienePromocionesConFiltro;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Persistence;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Promociones.Consulta.ObtenerDetalleCargaMasiva;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Promociones.Consulta.ObtenerNumeroExpediente;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Promociones.Consulta.ObtenerPromocionesFiltros;
using CJF.Sgcpj.Judicatura.Oficialia.Domain.Entities;
//using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Tramite.Application.Models;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Oficialia.Infrastructure.Persistence.Repositories;
public class PromocionesRepository : IPromocionesRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public PromocionesRepository(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<(List<Promocion>, MetaDataEstados)> ObtenerPromocionesConFiltro(ConsultaPaginada consultaPaginada)
    {
        consultaPaginada.Texto = consultaPaginada?.Texto?.ToLower();
        return await ObtenerPromocionesAsync(consultaPaginada);
    }

    private async Task<(List<Promocion>, MetaDataEstados)> ObtenerPromocionesAsync(ConsultaPaginada consultaPaginada)
    {
        consultaPaginada.Texto = consultaPaginada?.Texto?.ToLower();
        var metadata = new MetaDataEstados();
        var listaTablero = new List<Promocion>();

        List<SqlParameter> listaTableroSql = new List<SqlParameter>();
        var pi_CatOrganismoId = new SqlParameter("@pi_CatOrganismoId", consultaPaginada.OrganismoID);
        var pi_TamanoPagina = new SqlParameter("@pi_TamanoPagina", consultaPaginada.RegistrosPorPagina);
        var pi_NumeroPagina = new SqlParameter("@pi_NumeroPagina", consultaPaginada.Pagina);
        var pi_AsuntoNeunId = new SqlParameter("@pi_AsuntoNeunId", consultaPaginada.AsuntoNeunId);
        var pi_FechaPresentacionIni = new SqlParameter("@pi_FechaPresentacionIni", consultaPaginada.FechaInicial);
        var pi_FechaPresentacionFin = new SqlParameter("@pi_FechaPresentacionFin", consultaPaginada.FechaFinal);
        var pi_Texto = new SqlParameter("@pi_Texto", consultaPaginada.Texto);
        var pi_OrdenarPor = new SqlParameter("@pi_OrdenarPor", consultaPaginada.OrdenarPor);
        var pi_TipoOrden = new SqlParameter("@pi_TipoOrden", consultaPaginada.Descendente);
        var pi_FiltroTipo = new SqlParameter("@pi_FiltroTipo", consultaPaginada.Estado);
        var pi_Origen = new SqlParameter("@pi_Origen", consultaPaginada.Origen);
        var pi_Asignado = new SqlParameter("@pi_Asignado", consultaPaginada.Asignado);
        var pi_Capturo = new SqlParameter("@pi_Capturo", consultaPaginada.Capturo);

        listaTableroSql.Add(pi_CatOrganismoId);
        listaTableroSql.Add(pi_TamanoPagina);
        listaTableroSql.Add(pi_NumeroPagina);
        listaTableroSql.Add(pi_AsuntoNeunId);
        listaTableroSql.Add(pi_FechaPresentacionIni);
        listaTableroSql.Add(pi_FechaPresentacionFin);
        listaTableroSql.Add(pi_Texto);
        listaTableroSql.Add(pi_OrdenarPor);
        listaTableroSql.Add(pi_TipoOrden);
        listaTableroSql.Add(pi_FiltroTipo);
        listaTableroSql.Add(pi_Origen);
        listaTableroSql.Add(pi_Asignado);
        listaTableroSql.Add(pi_Capturo);

        var (metadatos, datos) = await _dbContext.ExecuteStoredProc<ContadoresEstadosTablero, PromocionItemTablero>("SISE3.pcTableroPromociones", listaTableroSql.ToArray());
        listaTablero = datos.Select(x => _mapper.Map<Promocion>(x)).ToList();

        metadata.TotalSinCaptura = metadatos.FirstOrDefault()?.SinCaptura ?? 0;
        metadata.TotalCapturadas = metadatos.FirstOrDefault()?.Capturadas ?? 0;
        metadata.EnviadasAMesa = metadatos.FirstOrDefault()?.Asignadas ?? 0;
        metadata.TotalPromociones = metadatos.FirstOrDefault()?.Total ?? 0;

        return await Task.FromResult((listaTablero, metadata));

    }

    private static string LlaveCache(DateTime fechaInicial, DateTime fechaFinal, int estado, string texto, string cachePrefix)
    {
        return cachePrefix + fechaInicial.ToString("yyyy-MM-dd") + fechaFinal.ToString("yyyy-MM-dd") + estado.ToString() + texto;
    }


    public async Task<List<PromocionDetalle>> ObtenerLecturaPromocion(long asuntoNeunId, int asuntoID, int yearPromocion, int numeroOrden, int catIdOrganismo, int numeroRegistro, int origenPromocion)
    {
        var parametros = new[]
        {
            new SqlParameter("@pi_AsuntoNeunId", asuntoNeunId),
            new SqlParameter("@pi_AsuntoID", asuntoID),
            new SqlParameter("@pi_YearPromocion", yearPromocion),
            new SqlParameter("@pi_NumeroOrden", numeroOrden),
            new SqlParameter("@pi_catIdOrganismo", catIdOrganismo),
            new SqlParameter("@pi_NumeroRegistro", numeroRegistro),
            new SqlParameter("@pi_OrigenPromocion", origenPromocion)
        };

        var sqlQuery = "[dbo].[usp_EXPE_PromocionEditarSel]";
        return await _dbContext.ExecuteStoredProc<PromocionDetalle?>(sqlQuery, parametros);
    }



    public async Task<DatosPromocion> AgregarPromocion(AgregarPromocion promocion)
    {

        var resultadoPromocion = new DatosPromocion();
        var poNumeroOrden = new SqlParameter("@po_NumeroOrden", SqlDbType.Int);
        var poAsuntoNeunId = new SqlParameter("@po_AsuntoNeunId", SqlDbType.BigInt);
        var poNombreArchivo = new SqlParameter("@po_NombreArchivo", SqlDbType.VarChar, 50);
        var poNumeroConsecutivo = new SqlParameter("@po_NumeroConsecutivo", SqlDbType.Int);

        var pi_numeroOCC = new SqlParameter("@pi_numeroOCC", SqlDbType.VarChar, 50);
        var pi_numeroExpediente = new SqlParameter("@pi_NoExpediente", SqlDbType.VarChar, 50);
        var pi_HoraPresentacion = new SqlParameter("@pi_HoraPresentacion", SqlDbType.VarChar, 8);
        var pi_Observaciones = new SqlParameter("@pi_Observaciones", SqlDbType.VarChar, -1);
        var pi_IpUsuario = new SqlParameter("@pi_IpUsuario", SqlDbType.VarChar, 50);

        poNumeroOrden.Direction = ParameterDirection.Output;
        poAsuntoNeunId.Direction = ParameterDirection.Output;
        poNombreArchivo.Direction = ParameterDirection.Output;
        poNumeroConsecutivo.Direction = ParameterDirection.Output;

        pi_numeroOCC.Value = promocion.NumeroOCC;
        pi_numeroExpediente.Value = promocion.NumeroExpediente;
        pi_HoraPresentacion.Value = promocion.HoraPresentacion;
        pi_Observaciones.Value = "Observaciones Promocion";
        pi_IpUsuario.Value = "192.168.0.0";

        SqlParameter[] parametros = {
            //TODO: Parametros para expediente
            new SqlParameter("@pi_CatOrganismoId",promocion.CatOrganismoId),/*promocion.CatOrganismoId),*/
            new SqlParameter("@pi_catTipoAsuntoId", promocion.TipoAsunto.CatTipoAsuntoId),
            pi_numeroOCC,
            pi_numeroExpediente,
            new SqlParameter("@pi_EmpleadoId",promocion.EmpleadoId),//Identificador del empleado que creo el expediente
            new SqlParameter("@pi_TipoProcedimiento",promocion.TipoProcedimiento?.ID),
            new SqlParameter("@pi_TipoCuaderno", promocion.Cuaderno.CuadernoId),
            new SqlParameter("@pi_FechaPresentacion", promocion.FechaPresentacion.Date),
            pi_HoraPresentacion,
            new SqlParameter("@pi_ClasePromocion", DBNull.Value),
            new SqlParameter("@pi_ClasePromovente", promocion.ClasePromovente),
            new SqlParameter("@pi_TipoContenido", promocion.Contenido.ID),
            new SqlParameter("@pi_NumeroCopias", promocion.Copias),
            new SqlParameter("@pi_NumeroAnexo", DBNull.Value),
            new SqlParameter("@pi_Secretario", promocion.SecretarioId),
            pi_Observaciones,
            pi_IpUsuario,
            new SqlParameter("@pi_OrigenPromocion", promocion.Origen),
            new SqlParameter("@pi_NumeroRegistro", promocion.Registro),
            poNumeroOrden,
            poAsuntoNeunId,
            poNombreArchivo,
            poNumeroConsecutivo,
            new SqlParameter("@pi_fojas", promocion.Fojas),

        };

        string sqlQuery = "[SISE3].[piInsertaExpedientePromocionyArchivo]";

        var resultado = await _dbContext.ExecuteStoredProc<AsuntoPromocion>(sqlQuery, parametros);

        resultadoPromocion.NumeroOrden = Convert.ToInt32(poNumeroOrden.Value);
        resultadoPromocion.AsuntoNeunId = Convert.ToInt32(poAsuntoNeunId.Value);
        resultadoPromocion.NombreArchivo = poNombreArchivo.Value.ToString();
        resultadoPromocion.NumeroConsecutivo = Convert.ToInt32(poNumeroConsecutivo.Value);
        return resultadoPromocion;
    }

    public async Task<long> EditarPromocion(EditarPromocion promocion)
    {
        var pi_HoraPresentacion = new SqlParameter("@pi_HoraPresentacion", SqlDbType.VarChar, 8);
        var pi_Observaciones = new SqlParameter("@pi_Observaciones", SqlDbType.VarChar, -1);
        var pi_IpUsuario = new SqlParameter("@pi_IpUsuario", SqlDbType.VarChar, 50);
        var pi_NumeroFojasTomos = new SqlParameter("@pi_NumeroFojasTomos", SqlDbType.VarChar, 100);
        var pi_AsuntoNeunIdNuevo = new SqlParameter("pi_AsuntoNeunIdNuevo", SqlDbType.BigInt);
        pi_HoraPresentacion.Value = promocion.HoraPresentacion;
        pi_Observaciones.Value = "Observaciones Promocion";
        pi_IpUsuario.Value = "192.168.0.0";
        pi_NumeroFojasTomos.Value = promocion.Fojas;
        if (promocion.Fojas == null)
        {
            pi_NumeroFojasTomos.Value = "0";
        }
        //--
        pi_AsuntoNeunIdNuevo.Value = promocion.AsuntoNeunIdNuevo;
        SqlParameter[] parametros = {
                 new SqlParameter("@pi_catIdOrganismo", promocion.IdOrganismo),
                 new SqlParameter("@pi_AsuntoNeunId", promocion.AsuntoNeunId),
                 new SqlParameter("@pi_TipoCuaderno", promocion.Cuaderno.CuadernoId),
                 new SqlParameter("@pi_FechaPresentacion", promocion.FechaPresentacion.Date),
                 pi_HoraPresentacion,
                 new SqlParameter("@pi_ClasePromovente", promocion.ClasePromovente),
                 new SqlParameter("@pi_TipoPromovente", promocion.PersonaId),
                 new SqlParameter("@pi_TipoContenido", promocion.Contenido.ID),
                 new SqlParameter("@pi_NumeroCopias", promocion.Copias),
                 new SqlParameter("@pi_NumeroAnexo", promocion.NumeroAnexos),
                 new SqlParameter("@pi_Secretario", promocion.SecretarioId),
                 new SqlParameter("@pi_RegistroEmpleadoId", promocion.RegistroEmpleadoId),
                 pi_NumeroFojasTomos,
                 pi_Observaciones,
                 pi_IpUsuario,
                 pi_AsuntoNeunIdNuevo,
                 new SqlParameter("@pi_OrigenPromocion", promocion.Origen),
                 new SqlParameter("@pi_NumeroRegistro", promocion.NumeroRegistro),
                 new SqlParameter("@pi_NumeroOrden", promocion.NumeroOrden),
                 new SqlParameter("@pi_YearPromocion", promocion.YearPromocion),

        };

        string sqlQuery = "SISE3.[paActualizaPromocion]";

        await _dbContext.ExecuteStoredProcObj<object?>(sqlQuery, parametros);
        return await Task.FromResult(1l);
    }

    public async Task<int> ObtenerCalculoRegistro(int catOrganismoId, int yearPromocion, int statusReg)
    {
        //var result = await _dbContext.Promociones
        //    .Where(p => p.CatOrganismoId == catOrganismoId && p.YearPromocion == yearPromocion && p.StatusReg == statusReg)
        //    .Select(p => p.NumeroRegistro)
        //    .MaxAsync();

        //return result + 1;
        throw new NotImplementedException();
    }

    public Task<bool> ValidarExpedienteRepetido(long asuntoNeunId, int catTipoAsuntoId)
    {
        var existeAsuntoNeumIDConMismoAsunto = asuntoNeunId == 1 && catTipoAsuntoId == 6;

        return Task.FromResult(!existeAsuntoNeumIDConMismoAsunto);
    }



    public async Task<string> RutaArchivo(string modulo)
    {

        List<SqlParameter> parametros = new List<SqlParameter>();

        // Parámetros de entrada
        var pi_Modulo = new SqlParameter("@pi_Modulo", modulo);
        parametros.Add(pi_Modulo);

        var rutas = await _dbContext.ExecuteStoredProc<RutasNas>("[SISE3].[pcRutasChunkXModulo]", parametros.ToArray());

        var ruta = rutas.FirstOrDefault(s => s.Iescritura);

        if (ruta == null)
        {
            throw new Exception("No existe ruta configurada para guardar el documento");
        }

        return ruta.Sruta;
    }
    public async Task<bool> ActualizarArchivo(AgregarDocumento agregarDocumento)
    {
        var resultado = false;
        List<SqlParameter> parametrosActualizarArchivo = new List<SqlParameter>();

        // Parámetros de entrada
        var pi_AsuntoNeunId = new SqlParameter("@pi_AsuntoNeunId", agregarDocumento.AsuntoNeunId);
        parametrosActualizarArchivo.Add(pi_AsuntoNeunId);
        var pi_AsuntoID = new SqlParameter("@pi_AsuntoID", 1/*agregarDocumento.AsuntoId*/);
        parametrosActualizarArchivo.Add(pi_AsuntoID);
        var pi_YearPromocion = new SqlParameter("@pi_YearPromocion", agregarDocumento.YearPromocion);
        parametrosActualizarArchivo.Add(pi_YearPromocion);
        var pi_NumeroOrden = new SqlParameter("@pi_NumeroOrden", agregarDocumento.NumeroOrden);
        parametrosActualizarArchivo.Add(pi_NumeroOrden);
        var pi_catIdOrganismo = new SqlParameter("@pi_catIdOrganismo", agregarDocumento.CatOrganismoId);
        parametrosActualizarArchivo.Add(pi_catIdOrganismo);
        var pi_Origen = new SqlParameter("@pi_Origen", agregarDocumento.Origen);
        parametrosActualizarArchivo.Add(pi_Origen);
        var pi_Clase = new SqlParameter("@pi_ClaseAnexo", agregarDocumento.Clase);
        parametrosActualizarArchivo.Add(pi_Clase);
        var pi_NumeroRegistro = new SqlParameter("@pi_NumeroRegistro", agregarDocumento.NumeroRegistro);
        parametrosActualizarArchivo.Add(pi_NumeroRegistro);
        var poNombreArchivo = new SqlParameter("@pi_NombreArchivoReal", "test-x");
        parametrosActualizarArchivo.Add(poNombreArchivo);
        var pi_RegistroEmpleadoSISEId = new SqlParameter("@pi_RegistroEmpleadoSISEId", agregarDocumento.RegistroEmpleadoId);
        parametrosActualizarArchivo.Add(pi_RegistroEmpleadoSISEId);
        var pi_Descripcion = new SqlParameter("@pi_Descripcion", agregarDocumento.Descripcion);
        parametrosActualizarArchivo.Add(pi_Descripcion);
        var pi_Caracter = new SqlParameter("@pi_Caracter", agregarDocumento.Caracter);
        parametrosActualizarArchivo.Add(pi_Caracter);
        var pi_NumeroConsecutivo = new SqlParameter("@pi_NumeroConsecutivo", agregarDocumento.NumeroConsecutivo);
        parametrosActualizarArchivo.Add(pi_NumeroConsecutivo);
        var pi_Fojas = new SqlParameter("@pi_Fojas", agregarDocumento.Fojas);
        parametrosActualizarArchivo.Add(pi_Fojas);
        string sqlQuery = "[SISE3].[paActualizaExpedientePromocionyArchivo]";
        await _dbContext.ExecuteStoredProcObj<object?>(sqlQuery, parametrosActualizarArchivo.ToArray());
        resultado = true;
        return resultado;

    }


    public async Task<DatosDocumento> GuardarDocumento(AgregarDocumento agregarDocumento)
    {
        var resultado = new DatosDocumento();

        List<SqlParameter> parametros = new List<SqlParameter>();

        // Parámetros de entrada
        var pi_AsuntoNeunId = new SqlParameter("@pi_AsuntoNeunId", agregarDocumento.AsuntoNeunId);
        parametros.Add(pi_AsuntoNeunId);
        var pi_YearPromocion = new SqlParameter("@pi_YearPromocion", agregarDocumento.YearPromocion);
        parametros.Add(pi_YearPromocion);
        var pi_NumeroOrden = new SqlParameter("@pi_NumeroOrden", agregarDocumento.NumeroOrden);
        parametros.Add(pi_NumeroOrden);
        var pi_RegistroEmpleadoId = new SqlParameter("@pi_RegistroEmpleadoId", agregarDocumento.RegistroEmpleadoId);
        parametros.Add(pi_RegistroEmpleadoId);
        var pi_Clase = new SqlParameter("@pi_Clase", agregarDocumento.Clase);
        parametros.Add(pi_Clase);
        var pi_Descripcion = new SqlParameter("@pi_Descripcion", agregarDocumento.Descripcion);
        parametros.Add(pi_Descripcion);
        var pi_Caracter = new SqlParameter("@pi_Caracter", agregarDocumento.Caracter);
        parametros.Add(pi_Caracter);
        var pi_Fojas = new SqlParameter("@pi_Fojas", agregarDocumento.Fojas);
        parametros.Add(pi_Fojas);

        // Parámetros de salida
        SqlParameter poNombreArchivo = new SqlParameter("@po_NombreArchivo", SqlDbType.VarChar, 50);
        poNombreArchivo.Direction = ParameterDirection.Output;
        parametros.Add(poNombreArchivo);

        SqlParameter poNumeroConsecutivo = new SqlParameter("@po_NumeroConsecutivo", SqlDbType.Int);

        if (agregarDocumento.NumeroConsecutivo == null)
        {
            poNumeroConsecutivo.Value = DBNull.Value;
        }
        else
        {
            poNumeroConsecutivo.Value = agregarDocumento.NumeroConsecutivo;
        }
        poNumeroConsecutivo.Direction = ParameterDirection.InputOutput;
        parametros.Add(poNumeroConsecutivo);

        var pi_Origen = new SqlParameter("@pi_Origen", agregarDocumento.Origen);
        parametros.Add(pi_Origen);

        string sqlQuery = "[SISE3].[piInsertaDocumento]";
        await _dbContext.ExecuteStoredProcObj<object?>(sqlQuery, parametros.ToArray());
        resultado.NombreArchivo = poNombreArchivo.Value.ToString();
        resultado.NumeroConsecutivo = Convert.ToInt32(poNumeroConsecutivo.Value);
        return resultado;





    }
    public async Task<DatosDocumento> GuardarCargaMasiva(CargaMasiva cargaMasiva)
    {

        var resultado = new DatosDocumento();

        List<SqlParameter> parametros = new List<SqlParameter>();

        // Parámetros de entrada
        var pi_YearPromocion = new SqlParameter("@pi_YearPromocion", cargaMasiva.YearPromocion);
        parametros.Add(pi_YearPromocion);
        var pi_NumeroRegistro = new SqlParameter("@pi_NumeroRegistro", cargaMasiva.NumeroRegistro);
        parametros.Add(pi_NumeroRegistro);
        var pi_CatOrganismoId = new SqlParameter("@pi_CatOrganismoId", cargaMasiva.CatOrganismoId);
        parametros.Add(pi_CatOrganismoId);

        var pi_NombreArchivoReal = new SqlParameter("@pi_NombreArchivoReal", cargaMasiva.NombreArchivoReal);
        parametros.Add(pi_NombreArchivoReal);
        var pi_RegistroEmpleadoId = new SqlParameter("@pi_RegistroEmpleadoId", cargaMasiva.RegistroEmpleadoId);
        parametros.Add(pi_RegistroEmpleadoId);

        // Parámetros de salida
        SqlParameter poNumeroConsecutivo = new SqlParameter("@po_NumeroConsecutivo", SqlDbType.Int);

        if (cargaMasiva.NumeroConsecutivo == null)
        {
            poNumeroConsecutivo.Value = DBNull.Value;
        }
        else
        {
            poNumeroConsecutivo.Value = cargaMasiva.NumeroConsecutivo;
        }
        poNumeroConsecutivo.Direction = ParameterDirection.InputOutput;
        parametros.Add(poNumeroConsecutivo);

        SqlParameter po_NombreArchivo = new SqlParameter("@po_NombreArchivo", SqlDbType.VarChar, 250);
        po_NombreArchivo.Direction = ParameterDirection.Output;
        parametros.Add(po_NombreArchivo);
        SqlParameter po_ExpedienteProcesado = new SqlParameter("@po_ExpedienteProcesado", SqlDbType.VarChar, 50);
        po_ExpedienteProcesado.Direction = ParameterDirection.Output;
        parametros.Add(po_ExpedienteProcesado);

        string sqlQuery = "[SISE3].[piInsertaDocumentosMasivo]";
        await _dbContext.ExecuteStoredProcObj<object?>(sqlQuery, parametros.ToArray());
        resultado.NombreArchivo = po_NombreArchivo.Value.ToString();
        resultado.NumeroConsecutivo = Convert.ToInt32(poNumeroConsecutivo.Value);
        resultado.ExpedienteProcesado = po_ExpedienteProcesado.Value.ToString();
        return resultado;






    }
    public async Task<long> RollBackArchivo(RollBackArchivo rollBackArchivo)
    {
        List<SqlParameter> parametros = new List<SqlParameter>();

        // Parámetros de entrada
        var pi_AsuntoNeunId = new SqlParameter("@pi_AsuntoNeunId", rollBackArchivo.AsuntoNeunId);
        var pi_AsuntoID = new SqlParameter("@pi_AsuntoID", rollBackArchivo.AsuntoID);
        var pi_YearPromocion = new SqlParameter("@pi_YearPromocion", rollBackArchivo.YearPromocion);
        var pi_NumeroOrden = new SqlParameter("@pi_NumeroOrden", rollBackArchivo.NumeroOrden);
        var pi_CatIdOrganismo = new SqlParameter("@pi_catIdOrganismo", rollBackArchivo.CatIdOrganismo);
        var pi_RegistroEmpleadoId = new SqlParameter("@pi_RegistroEmpleadoId", rollBackArchivo.RegistroEmpleadoId);
        var pi_NumeroRegistro = new SqlParameter("@pi_NumeroRegistro", rollBackArchivo.NumeroRegistro);
        var pi_Consecutivo = new SqlParameter("@pi_Consecutivo", rollBackArchivo.Consecutivo);
        var pi_Origen = new SqlParameter("@pi_Origen", rollBackArchivo.Origen);
        parametros.Add(pi_AsuntoNeunId);
        parametros.Add(pi_AsuntoID);
        parametros.Add(pi_YearPromocion);
        parametros.Add(pi_NumeroOrden);
        parametros.Add(pi_CatIdOrganismo);
        parametros.Add(pi_RegistroEmpleadoId);
        parametros.Add(pi_NumeroRegistro);
        parametros.Add(pi_Consecutivo);
        parametros.Add(pi_Origen);

        await _dbContext.ExecuteStoredProcObj<object?>("[dbo].[usp_EXPE_AnexoDel]", parametros.ToArray());
        return 1;

    }

    public async Task<List<ArchivosAnexos>> ObtenerArchivosyAnexos(long asuntoNeunId, int numeroOrden, int yearPromocion, int catIdOrganismo, int origen, int modulo, int asuntoDocumentoId)
    {
        var parametros = new[]
{
                new SqlParameter("@pi_AsuntoNeunId", asuntoNeunId),
                new SqlParameter("@pi_YearPromocion", yearPromocion),
                new SqlParameter("@pi_NumeroOrden", numeroOrden),
                new SqlParameter("@pi_catIdOrganismo", catIdOrganismo),
                new SqlParameter("@pi_Origen", origen),
                new SqlParameter("@pi_TipoModulo", modulo),
                new SqlParameter("@pi_AsuntoDocumentoId", asuntoDocumentoId),


            };

        string sqlQuery = "SISE3.pcConsultaArchivosyRutaXModulo"; //SP para recuperar el guid

        var archivos = await _dbContext.ExecuteStoredProc<ArchivosAnexos?>(sqlQuery, parametros);

        //MapearDocumentos


        return archivos;
    }
    public async Task<string> EliminarPromocion(EliminarPromocion promocion)
    {

        SqlParameter[] parametros = {
                new SqlParameter("@pi_AsuntoNeunId", promocion.AsuntoNeunId),
                new SqlParameter("@pi_YearPromocion", promocion.YearPromocion),
                new SqlParameter("@pi_NumeroOrden", promocion.NumeroOrden),
                new SqlParameter("@pi_catIdOrganismo", promocion.CatIdOrganismo)
            };
        string sqlQuery = "[SISE3].[peEliminaPromocion]";

        string respuesta = await _dbContext.ExecuteStoredProcObj<string>(sqlQuery, parametros);
        return respuesta;



    }

    public async Task<(List<
            PromocionDetalleTablero>, List<AnexosLista>)> ObtenerPromocionDetalleTablero(int catOrganismoId, long asuntoNeunId, int usuariId, int origen, int numeroOrden, int yearPromocion, long? kIdElectronica)
    {
        var listaAnexos = new List<AnexosLista>();
        var itemsPromocionDetalle = new List<
            PromocionDetalleTablero>();
        List<SqlParameter> detalleTableroSql = new List<SqlParameter>();
        var pi_CatOrganismoId = new SqlParameter("@pi_CatOrganismoId", catOrganismoId/*4*/);
        var pi_AsuntoNeunId = new SqlParameter("@pi_AsuntoNeunId", asuntoNeunId /*8354118*/);
        var pi_UsuariId = new SqlParameter("@pi_UsuariId", 1);
        var pi_Origen = new SqlParameter("@pi_Origen", origen);
        var pi_NumeroOrden = new SqlParameter("@pi_NumeroOrden", numeroOrden/*148*/);
        var pi_YearPromocion = new SqlParameter("@pi_YearPromocion", yearPromocion/*2010*/);
        var pi_kIdElectronica = new SqlParameter("@pi_kIdElectronica", kIdElectronica/*0*/);
        detalleTableroSql.Add(pi_CatOrganismoId);
        detalleTableroSql.Add(pi_AsuntoNeunId);
        detalleTableroSql.Add(pi_UsuariId);
        detalleTableroSql.Add(pi_Origen);
        detalleTableroSql.Add(pi_NumeroOrden);
        detalleTableroSql.Add(pi_YearPromocion);
        detalleTableroSql.Add(pi_kIdElectronica);

        var (itemsPromocionDetalleSql, listaAnexosSql) = await _dbContext.ExecuteStoredProc<ItemDetallePromocionTablero, AnexosLista>("SISE3.pcDetallePromocionTablero", detalleTableroSql.ToArray());

        itemsPromocionDetalle = itemsPromocionDetalleSql.Select(x => _mapper.Map<PromocionDetalleTablero>(x)).ToList();
        listaAnexos = listaAnexosSql.Select(x => _mapper.Map<AnexosLista>(x)).ToList();
        return await Task.FromResult((itemsPromocionDetalle, listaAnexos));
    }

    public async Task<(List<
           PromocionDetalleTablero>, List<AnexosLista>)> ObtenerPromocionDetalleTableroElectronicas(int catOrganismoId, long asuntoNeunId, int usuariId, int origen, int numeroOrden, int yearPromocion, long? kIdElectronica)
    {
        var listaAnexos = new List<AnexosLista>();
        var itemsPromocionDetalle = new List<
            PromocionDetalleTablero>();
        List<SqlParameter> detalleTableroSql = new List<SqlParameter>();
        var pi_CatOrganismoId = new SqlParameter("@pi_CatOrganismoId", catOrganismoId/*4*/);
        var pi_AsuntoNeunId = new SqlParameter("@pi_AsuntoNeunId", asuntoNeunId /*8354118*/);
        var pi_UsuariId = new SqlParameter("@pi_UsuariId", 1);
        var pi_Origen = new SqlParameter("@pi_Origen", origen);
        var pi_NumeroOrden = new SqlParameter("@pi_NumeroOrden", numeroOrden/*148*/);
        var pi_YearPromocion = new SqlParameter("@pi_YearPromocion", yearPromocion/*2010*/);
        detalleTableroSql.Add(pi_CatOrganismoId);
        detalleTableroSql.Add(pi_AsuntoNeunId);
        detalleTableroSql.Add(pi_UsuariId);
        detalleTableroSql.Add(pi_Origen);
        detalleTableroSql.Add(pi_NumeroOrden);
        detalleTableroSql.Add(pi_YearPromocion);

        var (itemsPromocionDetalleSql, listaAnexosSql) = await _dbContext.ExecuteStoredProc<ItemDetallePromocionTablero, AnexosLista>("SISE3.pcDetallePromocionElectronicaTablero", detalleTableroSql.ToArray());

        itemsPromocionDetalle = itemsPromocionDetalleSql.Select(x => _mapper.Map<PromocionDetalleTablero>(x)).ToList();
        listaAnexos = listaAnexosSql.Select(x => _mapper.Map<AnexosLista>(x)).ToList();
        return await Task.FromResult((itemsPromocionDetalle, listaAnexos));
    }

    public async Task<long> RollBackAnexo(RollBackAnexo rollBackAnexo)
    {
        List<SqlParameter> parametros = new List<SqlParameter>();

        // Parámetros de entrada
        var pi_AsuntoNeunId = new SqlParameter("@pi_AsuntoNeunId", rollBackAnexo.AsuntoNeunId);
        var pi_AsuntoID = new SqlParameter("@pi_AsuntoID", 1);
        var pi_YearPromocion = new SqlParameter("@pi_YearPromocion", rollBackAnexo.YearPromocion);
        var pi_NumeroOrden = new SqlParameter("@pi_NumeroOrden", rollBackAnexo.NumeroOrden);
        var pi_CatIdOrganismo = new SqlParameter("@pi_catIdOrganismo", rollBackAnexo.CatIdOrganismo);
        var pi_RegistroEmpleadoId = new SqlParameter("@pi_RegistroEmpleadoId", rollBackAnexo.RegistroEmpleadoId);
        var pi_NumeroRegistro = new SqlParameter("@pi_NumeroRegistro", rollBackAnexo.NumeroRegistro);
        var pi_Consecutivo = new SqlParameter("@pi_Consecutivo", rollBackAnexo.Consecutivo);
        var pi_Origen = new SqlParameter("@pi_Origen", rollBackAnexo.Origen);
        parametros.Add(pi_AsuntoNeunId);
        parametros.Add(pi_AsuntoID);
        parametros.Add(pi_YearPromocion);
        parametros.Add(pi_NumeroOrden);
        parametros.Add(pi_CatIdOrganismo);
        parametros.Add(pi_RegistroEmpleadoId);
        parametros.Add(pi_NumeroRegistro);
        parametros.Add(pi_Consecutivo);
        parametros.Add(pi_Origen);

        await _dbContext.ExecuteStoredProcObj<object?>("[dbo].[usp_EXPE_AnexoDel]", parametros.ToArray());
        return 1;
    }

    public Task<List<DetalleIndicadores>> ObtenerIndicadoresPromocion(DateTime fechaInicial, DateTime fechaFinal, int origenId)
    {
        List<SqlParameter> lista = new List<SqlParameter>();
        var pi_NumeroOrden = new SqlParameter("@fechaInicio", fechaInicial);
        lista.Add(pi_NumeroOrden);
        var pi_CatOrganismoId = new SqlParameter("@fechaFin", fechaFinal);
        lista.Add(pi_CatOrganismoId);
        var pi_IdPromocion = new SqlParameter("@pi_CatOrganismoId", origenId);
        lista.Add(pi_IdPromocion);

        return _dbContext.ExecuteStoredProc<DetalleIndicadores>("[SISE3].[spObtenerConteoPromociones]", lista.ToArray());
    }

    public Task<List<DetalleGruposMes>> ObtenerAsuntosMesPromocion(int empleadoId)
    {
        List<SqlParameter> lista = new List<SqlParameter>();
        var pi_IdEmpleado = new SqlParameter("@EmpleadoId", empleadoId);
        lista.Add(pi_IdEmpleado);

        return _dbContext.ExecuteStoredProc<DetalleGruposMes>("[SISE3].[spObtenerTiposAsuntoPorMes]", lista.ToArray());
    }

    public Task<List<DetalleIntervalos>> ObtenerTiemposTurnos(DateTime fechaInicial, DateTime fechaFinal, int empleadoId)
    {
        List<SqlParameter> lista = new List<SqlParameter>();
        var pi_NumeroOrden = new SqlParameter("@fechaInicio", fechaInicial);
        lista.Add(pi_NumeroOrden);
        var pi_CatOrganismoId = new SqlParameter("@fechaFin", fechaFinal);
        lista.Add(pi_CatOrganismoId);
        var pi_IdPromocion = new SqlParameter("@EmpleadoId", empleadoId);
        lista.Add(pi_IdPromocion);

        return _dbContext.ExecuteStoredProc<DetalleIntervalos>("[SISE3].[spDiferenciaTiemposPromocion]", lista.ToArray());
    }

    public async Task<int> InsertarPromocion(InsertarPromocion promocion) //Vinculacion de Promocion
    {
        var po_NumeroOrden = new SqlParameter("@po_NumeroOrden", promocion.NumeroOrden);
        po_NumeroOrden.Direction = ParameterDirection.Output;
        SqlParameter[] parametros = {
                new SqlParameter("@pi_AsuntoNeunId", promocion.AsuntoNeunId),
                new SqlParameter("@pi_TipoCuaderno", promocion.TipoCuaderno),
                new SqlParameter("@pi_FechaPresentacion", promocion.FechaPresentacion),
                new SqlParameter("@pi_HoraPresentacion", promocion.HoraPresentacion),
                new SqlParameter("@pi_ClasePromocion",  DBNull.Value),
                new SqlParameter("@pi_ClasePromovente",promocion.ClasePromovente),
                new SqlParameter("@pi_TipoPromovente", promocion.TipoPromovente),
                new SqlParameter("@pi_TipoContenido", promocion.TipoContenido),
                new SqlParameter("@pi_NumeroCopias", promocion.NumeroCopias),
                new SqlParameter("@pi_NumeroAnexo", DBNull.Value),
                new SqlParameter("@pi_Secretario", promocion.Secretario),
                new SqlParameter("@pi_RegistroEmpleadoId", promocion.RegistroEmpleadoId/*promocion.RegistroEmpleadoId*/),
                new SqlParameter("@pi_Observaciones", promocion.Observaciones),
                new SqlParameter("@pi_IpUsuario", promocion.IpUsuario),
                new SqlParameter("@pi_OrigenPromocion", promocion.Origen),
                new SqlParameter("@pi_NumeroRegistro", promocion.NumeroRegistro),
                new SqlParameter("@pi_Fojas", promocion.Fojas),
                po_NumeroOrden
            };

        string sqlQuery = "[SISE3].[piInsertaPromocion]";

        string respuesta = await _dbContext.ExecuteStoredProcObj<string>(sqlQuery, parametros);
        return Convert.ToInt32(po_NumeroOrden.Value);


    }
    public async Task<ResultadoInsertarExpedienteDto> InsertarExpediente(Judicatura.Application.Common.Models.InsertarExpediente expediente)
    {
        var po_AsuntoNeunId = new SqlParameter("@po_AsuntoNeunId", SqlDbType.BigInt);
        po_AsuntoNeunId.Value = (object)expediente.PiAsuntoNeunId ?? DBNull.Value;
        po_AsuntoNeunId.Direction = ParameterDirection.InputOutput;

        var po_AsuntoId = new SqlParameter("@po_AsuntoId", expediente.AsuntoId);
        po_AsuntoId.Direction = ParameterDirection.Output;

        
        var res = new ResultadoInsertarExpedienteDto();
        SqlParameter[] parametros = {
                new SqlParameter("@pi_CatOrganismoId",expediente.CatOrganismoId),
                new SqlParameter("@pi_CatTipoAsuntoId", expediente.CatTipoAsuntoId),
                new SqlParameter("@pi_NumeroOCC", expediente.NumeroOCC),
                new SqlParameter("@pi_NoExpediente", expediente.NoExpediente),
                new SqlParameter("@pi_EmpleadoId", expediente.EmpleadoId),
                new SqlParameter("@pi_TipoProcedimiento",expediente.TipoProcedimiento),
                new SqlParameter("@pi_EsActualizacion",expediente.EsActualizacion),
                new SqlParameter("@pi_AsuntoNeunId", expediente.PiAsuntoNeunId),
                po_AsuntoNeunId,
                po_AsuntoId
            };

        string sqlQuery = "[SISE3].[piAsunto]";

        await _dbContext.ExecuteStoredProcObj<object?>(sqlQuery, parametros);
        res.AsuntoNeunId = Convert.ToInt64(po_AsuntoNeunId.Value);
        res.AsuntoId = Convert.ToInt32(po_AsuntoId.Value);
        return res;


    }

    public async Task<ExpedienteDto> ObtenerNumeroExpediente(ObtenerNumeroExpediente expediente)
    {
        var expedienteDto = new ExpedienteDto();
        List<SqlParameter> lista = new List<SqlParameter>();
        var pi_CatOrganismoId = new Microsoft.Data.SqlClient.SqlParameter("@pi_CatOrganismoId", expediente.CatOrganismoId);
        lista.Add(pi_CatOrganismoId);
        var pi_CatTipoAsunto = new Microsoft.Data.SqlClient.SqlParameter("@pi_CatTipoAsunto", expediente.TipoAsuntoId);
        lista.Add(pi_CatTipoAsunto);
        var pi_CatTipoProcedimiento = new Microsoft.Data.SqlClient.SqlParameter("@pi_CatTipoProcedimiento", expediente.TipoProcedimientoId);
        lista.Add(pi_CatTipoProcedimiento);
        expedienteDto.AsuntoAlias = await _dbContext.ExecuteStoredProcObj<string>("[SISE3].[pcObtieneNumeroExpediente]", lista.ToArray());
        return expedienteDto;
    }
    async Task<ObtenerDetalleAlertaDto?> IPromocionesRepository.ObtenerDetalleAlerta(ObtenerDetalleCargaMasivaRequest detalleAlerta)
    {
        var detalleAlertaDto = new ObtenerDetalleAlertaDto();
        List<SqlParameter> lista = new List<SqlParameter>();
        var pi_CatOrganismoId = new SqlParameter("@pi_CatOrganismoId", detalleAlerta.CatOrganismoId);
        lista.Add(pi_CatOrganismoId);
        var pi_YearPromocion = new SqlParameter("@pi_YearPromocion", detalleAlerta.YearPromocion);
        lista.Add(pi_YearPromocion);
        var pi_NumeroRegistro = new SqlParameter("@pi_NumeroRegistro", detalleAlerta.NumeroRegistro);
        lista.Add(pi_NumeroRegistro);
        List<DetalleAlertaDto?> resultado = await _dbContext.ExecuteStoredProc<DetalleAlertaDto?>("[SISE3].[pcGeneraDetalleAlertaPromocion]", lista.ToArray());


        detalleAlertaDto.NumeroRegistro = resultado.FirstOrDefault()?.NumeroRegistro ?? 0;
        detalleAlertaDto.NumeroExpediente = resultado.FirstOrDefault()?.NumeroExpediente ?? string.Empty;
        detalleAlertaDto.TipoProcedimiento = resultado.FirstOrDefault()?.TipoProcedimiento ?? string.Empty;
        detalleAlertaDto.TipoAsunto = resultado.FirstOrDefault()?.TipoAsunto ?? string.Empty;
        detalleAlertaDto.Mesa = resultado.FirstOrDefault()?.Mesa ?? string.Empty;
        detalleAlertaDto.SecretarioId = resultado.FirstOrDefault()?.SecretarioId ?? 0;

        return detalleAlertaDto;
        
    }

    public async Task<bool> RelacionPromocionElectronica(long asuntoNeunId, int numeroOrden, int catOrganismoId, long? idPromocion, int? origen, int empleadoId,
        bool? conExpediente)
    {
        List<SqlParameter> lista = new List<SqlParameter>();
        var pi_NumeroOrden = new SqlParameter("@pi_NumeroOrden", numeroOrden);
        lista.Add(pi_NumeroOrden);
        var pi_CatOrganismoId = new SqlParameter("@pi_CatOrganismoId", catOrganismoId);
        lista.Add(pi_CatOrganismoId);
        var pi_IdPromocion = new SqlParameter("@pi_IdPromocion", idPromocion);
        lista.Add(pi_IdPromocion);
        var pi_Origen = new SqlParameter("@pi_Origen", origen);
        lista.Add(pi_Origen);
        var pi_EmpleadoId = new SqlParameter("@pi_EmpleadoId", empleadoId);
        lista.Add(pi_EmpleadoId);
        var pi_ConExpediente = new SqlParameter("@pi_ConExpediente", conExpediente);
        lista.Add(pi_ConExpediente);        
        lista.Add(new SqlParameter("@pi_AsuntoNeunId", asuntoNeunId));
        await _dbContext.ExecuteStoredProc<bool?>("[SISE3].[piRelacionPromocionElectronicayFisica]", lista.ToArray());

        return true;
    }

    public async Task<(List<FiltroSecretario>, List<FiltroOrigen>, List<FiltroCapturo>)> ObtenerFiltrosPromociones(ObtienePromocionesFiltrosConsulta request)
    {
        List<SqlParameter> listaTableroSql = new List<SqlParameter>();

        var pi_catIdOrganismo = new SqlParameter("@pi_catIdOrganismo", request.CatOrganismoId);
        listaTableroSql.Add(pi_catIdOrganismo);

        var (result1, result2, result3) =
            await _dbContext.ExecuteStoredProcThree<FiltroSecretario, FiltroOrigen, FiltroCapturo>("[SISE3].[pcObtieneFiltrosOficialia]", listaTableroSql.ToArray());

        return await Task.FromResult((result1, result2, result3));
    }


    public async Task<List<InfoDocumentos>> ObtenerPromociones(Guid acuerdoGuid)
    {
        var parametros = new List<SqlParameter>()
        {
            new SqlParameter("@pi_GuidAcuerdo", acuerdoGuid)
        };

        string sqlQuery = "[SISE3].[pcConsultaPromocionFirma]";
        return await _dbContext.ExecuteStoredProc<InfoDocumentos>(sqlQuery, parametros.ToArray());

    }

    public async Task<bool> ActualizaEstadoFirmaPromocion(string? promocionGuid, int catOrganismo, long AsuntoNeun, string nombreArchivo)
    {
        var parametros = new List<SqlParameter>()
        {
            new SqlParameter("@pi_GuidPromocion", promocionGuid),
            new SqlParameter("@pi_Organismo", catOrganismo),
            new SqlParameter("@pi_asuntoNeunId", AsuntoNeun),
            new SqlParameter("@pi_nombreArchivo", nombreArchivo)
        };
        string sqlQuery = "[SISE3].[paActualizaEstatusFirmadoPromocion]";
        await _dbContext.ExecuteStoredProc<string>(sqlQuery, parametros.ToArray());
        return true;
    }
}
