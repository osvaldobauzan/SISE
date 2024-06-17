using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Expediente.Consulta;
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
public class CatalogoExpediente
{
    private readonly IHttpRequestProcessor _processor;

    public CatalogoExpediente(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }

    [FunctionName("CatalogoExpediente")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "CatalogoExpediente" }, Summary = " Obtiene lista de expedientes")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<CatalogoExpedienteDto>), Description = "Catálogo de expedientes")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(CatalogoExpedienteDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "expedientes")] HttpRequest req,
           ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");
        return await _processor.ExecuteAsync<ObtieneCatalogoExpediente, List<CatalogoExpedienteDto>>(new ObtieneCatalogoExpediente());
    }

}
