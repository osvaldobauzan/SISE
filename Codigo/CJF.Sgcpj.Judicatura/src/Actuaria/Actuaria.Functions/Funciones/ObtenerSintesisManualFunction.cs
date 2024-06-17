using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerSintesisManual;
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
public class ObtenerSintesisManualFunction
{
    private readonly IHttpRequestProcessor _processor;

    public ObtenerSintesisManualFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }

    [FunctionName("Listasintesismanual")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "Actuaria" }, Summary = " Obtiene un listado con las sintesis manuales acorde los filtros especificados")]
    [OpenApiParameter(name: "fechaPublicacion", In = ParameterLocation.Query, Required = true, Type = typeof(DateTime), Description = "Fecha inicial de donde comieza el rango a buscar de los acuerdos, el fortmato es YYYY-MM-dd")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<ObtenerSintesisManualDTO>), Description = "Respuesta exitosa, contiene una lista de objetos 'sitesismanual' en formato JSON.")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "actuaria/listasintesismanual")] HttpRequest req, ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");
        log.LogInformation($"Fecha Publicacion: {req.Query["fechaPublicacion"]}");
        var consulta = new ObtenerSintesisManualRequest()
        {
            FechaPublicacion = Convert.ToDateTime(req.Query["fechaPublicacion"])
        };

        return await _processor.ExecuteAsync<ObtenerSintesisManualRequest, List<ObtenerSintesisManualDTO>>(consulta);
    }

}
