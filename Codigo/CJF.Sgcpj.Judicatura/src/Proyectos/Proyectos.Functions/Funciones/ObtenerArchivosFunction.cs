using System;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerArchivos;
using Common.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Proyectos.Functions.Funciones;

public class ObtenerArchivosFunction
{
    private readonly IHttpRequestProcessor _processor;
    public ObtenerArchivosFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }

    [FunctionName("ObtenerArchivo")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "ObtenerArchivo" }, Summary = " Obtiene los archivos en formato word-base64")]
    [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "id del documento")]
    [OpenApiParameter(name: "descargar", In = ParameterLocation.Query, Required = false, Type = typeof(bool), Description = "bandera para descargar archivo")]

    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(DocumentoBase64Dto), Description = "Respuesta exitosa se obtiene los expediente en formato word-base64 ")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]

    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "proyecto/documento")] HttpRequest req, ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");
        log.LogInformation($"ruta: {req.Query["id"]}");

        var consulta = new ObtenerArchivo()
        {
            Id = Convert.ToInt32(req.Query["id"]),
            Descargar = req.Query["descargar"].Count == 0 ? false : bool.Parse(req.Query["descargar"])
        };

        return await _processor.ExecuteAsync<ObtenerArchivo, DocumentoBase64Dto>(consulta);
    }
}
