using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.Actuario;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.Actuario.GenerarAcuseOficio.Actuario;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.Actuario.GenerarAcuseOficio.Fecha;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.DetalleNotificaciones;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Exceptions;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using Common.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Actuaria.Functions.Funciones.Actuario;
public class ActuarioFunction
{
    public readonly IHttpRequestProcessor _processor;

    public ActuarioFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }

    /// <summary>
    /// Obtiene las Notificaciones por actuario con los filtros especificados
    /// </summary>
    /// <param name="req"></param>
    /// <param name="log"></param>
    /// <returns></returns>
    [FunctionName("ActuarioNotificaciones")]
    [OpenApiOperation(operationId: "Get", tags: new[] { "ActuarioNotificaciones" }, Summary = " Obtiene las notificaciones por actuario con los filtros especificados")]
    [OpenApiParameter(name: "fechaInicial", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Fecha inicial de donde comieza el rango a buscar de las notificaciones, el fortmato es dd/MM/YYYY")]
    [OpenApiParameter(name: "fechaFinal", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Fecha final  de donde se termina el rango a buscar de las notificaciones, el fortmato es dd/MM/YYYY")]
    [OpenApiParameter(name: "tamanioPagina", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Tamaño de página")]
    [OpenApiParameter(name: "numeroPagina", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Número de página")]
    [OpenApiParameter(name: "texto", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "texto para filtrar resultados ")]
    [OpenApiParameter(name: "ordenarPor", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "Campo por el que se ordenará el dataset")]
    [OpenApiParameter(name: "tipoOrden", In = ParameterLocation.Query, Required = false, Type = typeof(bool?), Description = "Tipo de órden ascendente o descendente")]
    [OpenApiParameter(name: "filtroTipo", In = ParameterLocation.Query, Required = false, Type = typeof(int?), Description = "Tipo de filtro")]
    [OpenApiParameter(name: "filtroTipoParteID", In = ParameterLocation.Query, Required = false, Type = typeof(int?), Description = "Id del tipo parte")]
    [OpenApiParameter(name: "filtroTipoNotificacionID", In = ParameterLocation.Query, Required = false, Type = typeof(int?), Description = "ID del tipo de notificación")]
    [OpenApiParameter(name: "filtroActuarioID", In = ParameterLocation.Query, Required = false, Type = typeof(int?), Description = "ID del actuario")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ListaPaginada<ActuarioNotificacionesDto, ActuarioNotificacionesMetaDataEstadosDto>), Description = "Respuesta exitosa, contiene una lista de objetos 'NotificacionDto' en formato JSON.")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Get(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "actuaria/actuarionotificaciones")] HttpRequest req, ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        var aux = 0;
        
        string fechaInicialStr = req.Query["fechaInicial"];
        string fechaFinalStr = req.Query["fechaFinal"];

        if (!DateTime.TryParseExact(fechaInicialStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fecIni) ||
                 !DateTime.TryParseExact(fechaFinalStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fecfin))
        {

            throw new Exception("Alguna de las fechas es invalida");
        }

        // Verificar que la fecha inicial sea anterior a la fecha final
        if (fecIni > fecfin)
        {
            throw new Exception("Verificar que la fecha inicial sea anterior a la fecha final");
        }


        
        bool tipoOrden = Convert.ToBoolean(req.Query["tipoOrden"]);
        int? filtroTipo = int.TryParse(req.Query["filtroTipo"].ToString(), out aux) ? Convert.ToInt32(req.Query["filtroTipo"]) : null;
        int? filtroTipoParteID = int.TryParse(req.Query["filtroTipoParteID"].ToString(), out aux) ? Convert.ToInt32(req.Query["filtroTipoParteID"]) : null;
        int? filtroTipoNotificacionID = int.TryParse(req.Query["filtroTipoNotificacionID"].ToString(), out aux) ? Convert.ToInt32(req.Query["filtroTipoNotificacionID"]) : null;
        int? filtroActuarioID = int.TryParse(req.Query["filtroActuarioID"].ToString(), out aux) ? Convert.ToInt32(req.Query["filtroActuarioID"]) : null;

        log.LogInformation($"{fecIni}, {fecfin}, {tipoOrden}, {filtroTipo}, " +
                           $"{req.Query["TamanioPagina"]}, {req.Query["NumeroPagina"]}, " +
                           $"{req.Query["texto"]}, {req.Query["ordenarPor"]}, {filtroTipo}, " +
                           $"{filtroTipoParteID}, {filtroTipoNotificacionID}, {filtroActuarioID}");


        var notificaciones = new ActuarioNotificacionesConsulta()
        {
            FechaInicial = fecIni,
            FechaFinal = fecfin,
            TamanioPagina = Convert.ToInt32(req.Query["TamanioPagina"]),
            NumeroPagina = Convert.ToInt32(req.Query["NumeroPagina"]),
            Texto = req.Query["texto"],
            OrdenarPor = req.Query["ordenarPor"],
            TipoOrden = tipoOrden,
            FiltroTipo = filtroTipo,
            FiltroTipoParteID = filtroTipoParteID,
            FiltroTipoNotificacionID = filtroTipoNotificacionID,
            FiltroActuarioID = filtroActuarioID,

        };
        return await _processor.ExecuteAsync<ActuarioNotificacionesConsulta, ListaPaginada<ActuarioNotificacionesDto, ActuarioNotificacionesMetaDataEstadosDto>>(notificaciones);
    }


    [FunctionName("GenerarAcuseOficio")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "GenerarAcuseOficio" }, Summary = "Genera el acuse de oficio")]
    [OpenApiParameter(name: "AsuntoNeunId", In = ParameterLocation.Query, Required = true, Type = typeof(long), Description = "Identificador de Asunto Neun del oficio")]
    [OpenApiParameter(name: "AsuntoDocumentoId", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Identificador Documento del oficio")]
    [OpenApiParameter(name: "ActuarioId", In = ParameterLocation.Query, Required = true, Type = typeof(long), Description = "Identificador de Actuario")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(GenerarAcuseOficioResponseDto), Description = "Respuesta exitosa, contiene la propiedad contenido codificada en base64 con el documento generado")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(GenerarAcuseOficioResponseDto), Description = "Error data de entrada incorrecta")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]

    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "actuaria/acuseoficioactuario")] HttpRequest req,
                                         ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request. GenerarEvidenciaFirma");

        var request = new GenerarAcuseOficioRequestDto
        {
            AsuntoNeunId = Convert.ToInt64(req.Query["AsuntoNeunId"]),
            AsuntoDocumentoId = Convert.ToInt32(req.Query["AsuntoDocumentoId"]),
            ActuarioId = Convert.ToInt64(req.Query["ActuarioId"])
        };

        return await _processor.ExecuteAsync<GenerarAcuseOficioRequestDto, GenerarAcuseOficioResponseDto>(request);
    }


    [FunctionName("GenerarAcuseOficioPorFecha")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "GenerarAcuseOficioPorFecha" }, Summary = "Genera el acuse de oficio de actuario por fecha")]
    [OpenApiParameter(name: "FechaInicio", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Fecha de inicio de los oficios a consultar")]
    [OpenApiParameter(name: "FechaFin", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Fecha de fin de los oficios a consultar")]
    [OpenApiParameter(name: "ActuarioId", In = ParameterLocation.Query, Required = true, Type = typeof(long), Description = "Identificador de Actuario")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(GenerarAcuseOficioPorFechaResponseDto), Description = "Respuesta exitosa, contiene la propiedad contenido codificada en base64 con el documento generado")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(GenerarAcuseOficioPorFechaResponseDto), Description = "Error data de entrada incorrecta")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]

    public async Task<IActionResult> Run2(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "actuaria/acuseoficioactuarioporfecha")] HttpRequest req,
    ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request. GenerarEvidenciaFirma");

        string fechaInicialStr = req.Query["FechaInicio"];
        string fechaFinalStr = req.Query["FechaFin"];

        if (!DateTime.TryParseExact(fechaInicialStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fecIni) ||
                 !DateTime.TryParseExact(fechaFinalStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fecfin))
        {

            throw new Exception("Alguna de las fechas es invalida");
        }

        if (fecIni > fecfin)
        {
            throw new Exception("Verificar que la fecha inicial sea anterior a la fecha final");
        }


        var request = new GenerarAcuseOficioPorFechaRequestDto
        {
            ActuarioId = Convert.ToInt64(req.Query["ActuarioId"]),
            FechaInicio = fecIni,
            FechaFin = fecfin
        };

        return await _processor.ExecuteAsync<GenerarAcuseOficioPorFechaRequestDto, GenerarAcuseOficioPorFechaResponseDto>(request);
    }
} 
