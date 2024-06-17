using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerAcuerdo;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
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

namespace Actuaria.Functions.Funciones;
public class ListaAcuerdosFunction
{
    private readonly IHttpRequestProcessor _processor;

    public ListaAcuerdosFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }

    [FunctionName("listaacuerdos")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "Actuaria" }, Summary = " Obtiene un listado con los acuerdos solicitados acorde los filtros especificados")]
    [OpenApiParameter(name: "fechaInicio", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Fecha inicial de donde comieza el rango a buscar de los acuerdos, el fortmato es YYYY-MM-dd")]
    [OpenApiParameter(name: "fechaFin", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Fecha final  de donde se termina el rango a buscar de los acuerdos, el fortmato es YYYY-MM-dd")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ObtenerAcuerdoM), Description = "Respuesta exitosa, contiene una lista de objetos 'obtenerAcuerdo' en formato JSON.")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "actuaria/listaacuerdos")] HttpRequest req, ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");
        log.LogInformation($"Fecha inicial: {req.Query["fechaInicio"]} Fecha final: {req.Query["fechaFin"]}");

        string fechaInicialStr = req.Query["fechaInicio"];
        string fechaFinalStr = req.Query["fechaFin"];

        var consulta = new ObtenerAcuerdoRequest()
        {
            FechaInicial = fechaInicialStr, 
            FechaFinal = fechaFinalStr,
        };

        return await _processor.ExecuteAsync<ObtenerAcuerdoRequest, List<ObtenerAcuerdoM>>(consulta);
    }
}
