using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.CatalogoDependiente;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Generico.Consulta;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using Common.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Catalogos.Functions.Funciones;

public class CatalogoDependienteFunction
{
    private readonly IHttpRequestProcessor _processor;

    public CatalogoDependienteFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }

    /// <summary>
    /// Función para obtener un catálogo dependiente
    /// </summary>
    /// <param name="req"></param>
    /// <param name="log"></param>
    /// <returns></returns>
    [FunctionName("CatalogoDependienteFunction")]
    [OpenApiParameter(name: "CatalogoPadreId", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Representa el identificador del catálogo padre")]
    [OpenApiParameter(name: "CatalogoId", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Representa el identificador del catálogo hijo")]

    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<CatalogoGenericoDTO>), Description = "Lista de opciones de catálogo")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "catalogodependiente")] HttpRequest req, ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        var request = new CatalogoDependienteFiltro
        {
            CatalogoPadreId = Convert.ToInt32(req.Query["CatalogoPadreId"]),
            CatalogoId = Convert.ToInt32(req.Query["CatalogoId"])
        };

        return await _processor.ExecuteAsync<CatalogoDependienteFiltro, List<CatalogoDependienteDTO>>(request);
    }
}
