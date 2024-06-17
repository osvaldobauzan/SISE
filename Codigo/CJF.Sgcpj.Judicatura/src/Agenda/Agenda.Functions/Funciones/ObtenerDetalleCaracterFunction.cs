using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Consulta.ObtenerDetalleCaracter;
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

namespace Agenda.Functions.Funciones;
public class ObtenerDetalleCaracterFunction
{
    private readonly IHttpRequestProcessor _processor;

    public ObtenerDetalleCaracterFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }

    [FunctionName("ObtenerDetalleCaracter")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "ObtenerDetalleCarcater" }, Summary = "Obtiene el detalle Caracter (Quejoso)")]
    [OpenApiParameter(name: "NeunId", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Nímero de NeunId")]
    [OpenApiParameter(name: "TipoAsuntoId", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "Representa el identificador del tipo de asunto")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ObtenerDetalleCaracterDto), Description = "Se obtuvo correctamente el detalle del actor por agenda")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "caracter/detalle")] HttpRequest req,
           ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");
     
        var consulta = new ObtenerDetalleCaracterRequest
        {
           IdNeun = Convert.ToInt32(req.Query["NeunId"]),
           IdTipoAsunto = Convert.ToInt32(req.Query["TipoAsuntoId"])
        };
        return await _processor.ExecuteAsync<ObtenerDetalleCaracterRequest, List<ObtenerDetalleCaracterDto>>(consulta);
    }


}
