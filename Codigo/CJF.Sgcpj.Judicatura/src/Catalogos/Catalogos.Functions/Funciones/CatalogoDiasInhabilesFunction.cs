using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Common.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Azure.WebJobs;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.DiasInhabiles.Consulta;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;


namespace Catalogos.Functions.Funciones;
public class CatalogoDiasInhabilesFunction
{
    private readonly IHttpRequestProcessor _processor;

    public CatalogoDiasInhabilesFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }

    [FunctionName("CatalogoDiasInhabiles")]
    [OpenApiParameter(name: "fechaInicio", In = ParameterLocation.Query, Required = true, Type = typeof(DateTime), Description = "Fecha de inicio a consultar")]
    [OpenApiParameter(name: "fechaFin", In = ParameterLocation.Query, Required = true, Type = typeof(DateTime), Description = "Fecha de fin a consultar")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "CatalogoDiasInhabiles" }, Summary = "Obtiene el catálogo de Dias inhabiles")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<CatalogoDiasInhabilesDto>), Description = "Catálogo de Dias inhabiles con éxito")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "diasinhabiles")] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");
        var filtro = new CatalogosDiasInhabilesFiltro()
        {
            FechaInicio = Convert.ToDateTime(req.Query["fechaInicio"]),
            FechaFin = Convert.ToDateTime(req.Query["fechaFin"])
        };
        return await _processor.ExecuteAsync<CatalogosDiasInhabilesFiltro, List<CatalogoDiasInhabilesDto>>(filtro);
    }
}
