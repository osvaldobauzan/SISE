using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerPersonaAsunto;
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

namespace Actuaria.Functions.Funciones;

public class ListaPersonasAsuntoFunction
{
    private readonly IHttpRequestProcessor _processor;
    public ListaPersonasAsuntoFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }

    [FunctionName("ObtienePersonasAsuntoFunction")]

    [OpenApiOperation(operationId: "Run", tags: new[] { "ObtienePersonasAsunto" }, Summary = "Obtiene la información de las personas relacionadas a un asuntoneunid de la tabla PersonasAsunto")]
    [OpenApiParameter(name: "PersonaId", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Id de la persona")]
    [OpenApiParameter(name: "AsuntoNeunId", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "AsuntoneunId")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ListarPersonaAsuntoDto), Description = "Id de la persona obtenido con éxito")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "actuaria/personaasunto")] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");
        var request = new ListarPersonaAsuntoRequestDto()
        {
            EmpleadoId = Convert.ToInt64(req.Query["PersonaId"]),
            AsuntoNeunId = Convert.ToInt64(req.Query["AsuntoNeunId"])
        };
        return await _processor.ExecuteAsync<ListarPersonaAsuntoRequestDto, List<ListarPersonaAsuntoDto>>(request);
    }
}