using System;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Sentencias.Application.Sentencias.Consultas.ObtenerSentencias;
using Common.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Sentencia.Functions.Funciones;

public class ObtenerSentenciasFunction
{
    private readonly IHttpRequestProcessor _processor;

    public ObtenerSentenciasFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }

    /// <summary>
    /// Función para obtener el listado de sentencias
    /// </summary>
    /// <param name="req"></param>
    /// <param name="log"></param>
    /// <returns></returns>
    [FunctionName("ObtenerSentencias")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "ObtenerSentencias" }, Summary = "Obtiene listado de sentencias")]
    [OpenApiParameter(name: "fecha", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Fecha de búsqueda inicial, formato dd/MM/YYYY")]
    [OpenApiParameter(name: "fechaFin", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Fecha de búsqueda final, formato dd/MM/YYYY")]

    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(TableroSentenciasDto), Description = "Respuesta exitosa, contiene el resultado en formato JSON.")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "sentencias")] HttpRequest req, ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        var consulta = new ObtenerSentenciasFiltro()
        {
            Fecha = req.Query["fecha"],
            FechaFin = req.Query["fechaFin"]
        };

        return await _processor.ExecuteAsync<ObtenerSentenciasFiltro, TableroSentenciasDto>(consulta);
    }

    [FunctionName("ObtenerArchivo")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "ObtenerArchivo" }, Summary = " Obtiene los archivos en formato word-base64")]
    [OpenApiParameter(name: "asuntoNeunId", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "asuntoNeunId del documento")]
    [OpenApiParameter(name: "asuntoDocumentoId", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "asuntoDocumentoId del documento")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(DocumentoBase64Dto), Description = "Respuesta exitosa se obtiene los expediente en formato word-base64 ")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]

    public async Task<IActionResult> Run2([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "sentencias/documento")] HttpRequest req, ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");
        log.LogInformation($"ruta: {req.Query["asuntoNeunId"]}");
        log.LogInformation($"ruta: {req.Query["asuntoDocumentoId"]}");
        var consulta = new ObtenerArchivo()
        {
            AsuntoNeunId = Convert.ToInt64(req.Query["asuntoNeunId"]),
            AsuntoDocumentoId = Convert.ToInt32(req.Query["asuntoDocumentoId"]),
            
        };
        return await _processor.ExecuteAsync<ObtenerArchivo, DocumentoBase64Dto>(consulta);
    }
}
