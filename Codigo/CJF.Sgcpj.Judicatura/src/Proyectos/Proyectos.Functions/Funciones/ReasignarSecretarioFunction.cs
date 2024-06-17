using System;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Comandos.ReasignarSecretarioComando;
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

public class ReasignarSecretarioFunction
{
    private readonly IHttpRequestProcessor _processor;

    public ReasignarSecretarioFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }

    /// <summary>
    /// Función para realizar la reasignación de secretario
    /// </summary>
    /// <param name="req"></param>
    /// <param name="log"></param>
    /// <returns></returns>
    [FunctionName("ReasignarSecretarioFunction")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "ReasignarSecretario" }, Summary = "Reasignación de proyectos a un secretario")]
    [OpenApiParameter(name: "SecretarioNuevoId", In = ParameterLocation.Query, Required = true, Type = typeof(long), Description = "Identificador del secretario organismo")]
    [OpenApiParameter(name: "ProyectosId", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Listado de proyectos para reasignar")]

    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ReasignacionSecretarioDTO), Description = "Estatus de la reasignación")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "proyectos/reasignar")] HttpRequest req, ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        var request = new ReasignarSecretarioComando
        {
            SecretarioNuevoId = Convert.ToInt64(req.Query["SecretarioNuevoId"]),
            ProyectosId = req.Query["ProyectosId"]
        };

        return await _processor.ExecuteAsync<ReasignarSecretarioComando, ReasignacionSecretarioDTO>(request);
    }
}
