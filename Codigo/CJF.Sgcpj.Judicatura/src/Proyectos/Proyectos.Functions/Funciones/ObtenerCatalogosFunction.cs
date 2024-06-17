using System;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerCatalogos;
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

public class ObtenerCatalogosFunction
{
    private readonly IHttpRequestProcessor _processor;

    public ObtenerCatalogosFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }

    /// <summary>
    /// Función para obtener los catálogos de módulo de proyectos
    /// </summary>
    /// <param name="req"></param>
    /// <param name="log"></param>
    /// <returns></returns>
    [FunctionName("ObtenerCatalogosFunction")]
    [OpenApiParameter(name: "catTipoAsuntoId", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Representa el identificador de tipo de asunto")]

    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ObtenerCatalogosDto), Description = "Catálogo de acto reclamado, delito o prestación solicitada obtenido con éxito")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "proyecto/motivos")] HttpRequest req, ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        var consulta = new ObtenerCatalogos
        {
            CatTipoAsuntoId = Convert.ToInt32(req.Query["catTipoAsuntoId"]),
        };

        return await _processor.ExecuteAsync<ObtenerCatalogos, ObtenerCatalogosDto>(consulta);
    }
}
