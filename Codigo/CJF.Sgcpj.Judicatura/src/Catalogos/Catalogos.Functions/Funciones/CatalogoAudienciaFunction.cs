using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Audiencia.Consulta;
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
public class CatalogoAudienciaFunction
{
    private readonly IHttpRequestProcessor _processor;
    public CatalogoAudienciaFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }

    [FunctionName("CatalogoAudiencias")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "CatalogoAudiencias" }, Summary = " Obtiene el catalogo de audiencias")]
    [OpenApiParameter(name: "TipoAsuntoId", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "Representa el identificador del tipo de asunto")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<CatalogoAudienciaDto>), Description = "Catálogo de audiencias obtenido con éxito")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "audiencia")] HttpRequest req,
    ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");
        int tipoAsuntoId = Convert.ToInt32(req.Query["TipoAsuntoId"]);

        var consulta = new CatalogAudienciaRequest
        {
            TipoAsuntoId = tipoAsuntoId
        };
        return await _processor.ExecuteAsync<CatalogAudienciaRequest, List<CatalogoAudienciaDto>>(consulta);
    }
}
