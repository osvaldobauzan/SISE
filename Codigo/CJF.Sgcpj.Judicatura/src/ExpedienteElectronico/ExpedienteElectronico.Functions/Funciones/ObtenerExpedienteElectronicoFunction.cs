using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.Common.Models;
using Common.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace ExpedienteElectronico.Functions.Funciones;
public class ObtenerExpedienteElectronicoFunction
{
    private readonly IHttpRequestProcessor _processor;
    public ObtenerExpedienteElectronicoFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }
    [FunctionName("ObtenerExpedienteElectronico")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "ExpedienteElectronico" }, Summary = " Obtiene el detalle del tablero para el Expediente Electrónico")]
    [OpenApiParameter(name: "asuntoNeunId", In = ParameterLocation.Query, Required = false, Type = typeof(long), Description = "")]

    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(DetalleExpedienteElectronico), Description = "Respuesta exitosa para el Expediente Electrónico")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "Error interno en el servidor")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ObtenerExpedienteElectronico")] HttpRequest req,
        ILogger log)
    {

        log.LogInformation("C# HTTP trigger function processed a request. ExpedienteElectronico");
        var consulta = new ExpedienteElectronicoFiltro
        {
            AsuntoNeunId = (req.Query != null && req.Query.ContainsKey("asuntoNeunId")) ? Convert.ToInt32(req.Query["asuntoNeunId"]) : 0,
        };
        return await _processor.ExecuteAsync<ExpedienteElectronicoFiltro, List<DetalleExpedienteElectronico>>(consulta);
    }
}


