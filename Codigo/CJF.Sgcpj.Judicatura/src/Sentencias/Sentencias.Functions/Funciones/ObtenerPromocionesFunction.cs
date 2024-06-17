using System;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Sentencias.Application.Sentencias.Consultas.ObtenerPromociones;
using Common.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Sentencias.Functions.Funciones;

public class ObtenerPromocionesFunction
{
    private readonly IHttpRequestProcessor _processor;

    public ObtenerPromocionesFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }

    /// <summary>
    /// Función para obtener el listado de sentencias
    /// </summary>
    /// <param name="req"></param>
    /// <param name="log"></param>
    /// <returns></returns>
    [FunctionName("ObtenerPromociones")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "ObtenerPromociones" }, Summary = "Obtiene listado de promociones de un expediente")]
    [OpenApiParameter(name: "asuntoId", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Identificador del asunto")]
    [OpenApiParameter(name: "asuntoNeunId", In = ParameterLocation.Query, Required = true, Type = typeof(long), Description = "Identificador del NEUN")]
    [OpenApiParameter(name: "tipoCuaderno", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Identificador tipo de cuaderno")]
    [OpenApiParameter(name: "sintesisOrden", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "Identificador de la Sintesis orden")]

    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ListadoPromocionesDTO), Description = "Respuesta exitosa, contiene el resultado en formato JSON.")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "sentencias/promociones")] HttpRequest req, ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        var consulta = new ObtenerPromocionesExpediente()
        {
            AsuntoId = Convert.ToInt32(req.Query["asuntoId"]),
            AsuntoNeunId = long.Parse(req.Query["asuntoNeunId"]),
            TipoCuaderno = Convert.ToInt32(req.Query["tipoCuaderno"]),
            SintesisOrden = Convert.ToInt32(req.Query["sintesisOrden"]),
        };

        return await _processor.ExecuteAsync<ObtenerPromocionesExpediente, ListadoPromocionesDTO>(consulta);
    }
}
