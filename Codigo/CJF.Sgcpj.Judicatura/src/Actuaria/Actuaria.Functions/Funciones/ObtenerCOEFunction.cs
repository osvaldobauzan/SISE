using System;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerCOE;
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
public class ObtenerCOEFunction
{
    private readonly IHttpRequestProcessor _processor;

    public ObtenerCOEFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }

    [FunctionName("ObtenerCOE")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "ObtenerCOE" }, Summary = "Obtiene el detalle de COE")]
    [OpenApiParameter(name: "NotificacionElectronicaId", In = ParameterLocation.Query, Required = true, Type = typeof(long), Description = "Identificacion de Notificacion Electronica")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ObtenerCOEDto), Description = "Se obtuvo correctamente el detalle del COE")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "actuaria/obtenerCOE")] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        var consulta = new ObtenerCOERequest
        {
            NotificacionElectronicaId = Convert.ToInt64(req.Query["NotificacionElectronicaId"])
        };

        return await _processor.ExecuteAsync<ObtenerCOERequest, ObtenerCOEDto>(consulta);
    }
}
