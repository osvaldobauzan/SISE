using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Consulta.ObtenerAgendaFecha;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Utils;
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
public class ObtenerAgendaFechaFunction
{
    private readonly IHttpRequestProcessor _processor;

    public ObtenerAgendaFechaFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }

    [FunctionName("ObtenerAgendaAudienciaPorFecha")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "ObtenerAgendaAudienciaPorFecha" }, Summary = "Obtiene agenda de audiencia por Fecha")]
    [OpenApiParameter(name: "fechaIni", In = ParameterLocation.Query, Required = false, Type = typeof(DateTime?), Description = "Representa fecha inicio")]
    [OpenApiParameter(name: "fechaFin", In = ParameterLocation.Query, Required = false, Type = typeof(DateTime?), Description = "Representa fecha fin")]
    [OpenApiParameter(name: "expediente", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "Representa el número de expediente")]
    [OpenApiParameter(name: "persona", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "Representa el nombre de persona")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ObtenerAgendaFechaDto), Description = "Se obtuvo correctamente la información de audiencia agendada")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "audiencia/agenda")] HttpRequest req,
           ILogger log)
    {
        var fechaInicio = req.Query["fechaIni"];
        var fechaFin = req.Query["fechaFin"];
        Nullable<DateTime> nulldatetime;
        nulldatetime = null;

        var consulta = new ObtenerAgendaFechaRequest
        {
            FechaIni = !string.IsNullOrEmpty(fechaInicio) ? MappingUtils.ObtenerFechaDeCadena(fechaInicio) : nulldatetime,
            FechaFin = !string.IsNullOrEmpty(fechaFin) ? MappingUtils.ObtenerFechaDeCadena(fechaFin) : nulldatetime,
            Expediente = Convert.ToString(req.Query["expediente"]),
            Persona = Convert.ToString(req.Query["persona"])
        };
        return await _processor.ExecuteAsync<ObtenerAgendaFechaRequest, List<ObtenerAgendaFechaDto>>(consulta);
    }

}
