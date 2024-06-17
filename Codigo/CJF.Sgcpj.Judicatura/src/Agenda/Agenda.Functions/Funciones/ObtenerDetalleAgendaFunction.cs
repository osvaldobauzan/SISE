using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Consulta.ObtenerDetalleAgenda;
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
public class ObtenerDetalleAgendaFunction
{
    private readonly IHttpRequestProcessor _processor;

    public ObtenerDetalleAgendaFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }

    //[FunctionName("ObtenerDetalleAgenda")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "ObtenerDetalleAgenda" }, Summary = "Obtiene el detalle de agenda por audiencia")]
    //[OpenApiParameter(name: "YearPromocion", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Año de la promocion")]
    //[OpenApiParameter(name: "NumeroRegistro", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Numero de registro de la promoción")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ObtenerDetalleAgendaDto), Description = "Se obtuvo correctamente el detalle de la agenda por audiencia")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "agenda/detalle")] HttpRequest req,
           ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        var consulta = new ObtenerDetalleAgendaRequest
        {
            //YearPromocion = Convert.ToInt32(req.Query["YearPromocion"]),
            //NumeroRegistro = Convert.ToInt32(req.Query["NumeroRegistro"]),

        };
        return await _processor.ExecuteAsync<ObtenerDetalleAgendaRequest, ObtenerDetalleAgendaDto>(consulta);
    }

}
