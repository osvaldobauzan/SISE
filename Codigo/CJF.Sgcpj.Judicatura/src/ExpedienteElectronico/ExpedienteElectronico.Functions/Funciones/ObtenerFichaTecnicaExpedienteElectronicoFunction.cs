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
public class ObtenerFichaTecnicaExpedienteElectronicoFunction
{
    private readonly IHttpRequestProcessor _processor;
    public ObtenerFichaTecnicaExpedienteElectronicoFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }
    [FunctionName("ObtenerFichaTecnicaExpedienteElectronico")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "FichaTecnicaExpedienteElectronico" }, Summary = " Obtiene el detalle de la Ficha Técnica del Expediente Electrónico")]
    [OpenApiParameter(name: "asuntoNeunId", In = ParameterLocation.Query, Required = false, Type = typeof(long), Description = "")]

    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(FichaTecnicaExpedienteElectronico), Description = "Respuesta exitosa la Ficha Técnica del Expediente Electrónico")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "Error interno en el servidor")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ObtenerFichaTecnicaExpedienteElectronico")] HttpRequest req,
        ILogger log)
    {

        log.LogInformation("C# HTTP trigger function processed a request. ExpedienteElectronico");
        var consulta = new FichaTecnicaExpedienteElectronicoFiltro
        {
            AsuntoNeunId = (req.Query != null && req.Query.ContainsKey("asuntoNeunId")) ? Convert.ToInt32(req.Query["asuntoNeunId"]) : 0,
        };
        return await _processor.ExecuteAsync<FichaTecnicaExpedienteElectronicoFiltro, List<FichaTecnicaExpedienteElectronico>>(consulta);
    }
}


