using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Sentencias.Application.Sentencias.Comandos.PreautorizarSentenciaComando;
using Common.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace CJF.Sgcpj.Judicatura.Sentencias.Functions.Funciones;

public class PreautorizarSentenciasFunction
{
    private readonly IHttpRequestProcessor _processor;

    public PreautorizarSentenciasFunction(IHttpRequestProcessor processor) => _processor = processor;

    /// <summary>
    /// Función para preautorizar sentencias
    /// </summary>
    /// <param name="req"></param>
    /// <param name="log"></param>
    /// <returns></returns>
    [FunctionName("PreautorizarSentencias")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "PreautorizarSentencias" }, Summary = "Preautoriza sentencias")]

    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "Respuesta exitosa, contiene el mensaje del resultado.")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "sentencias/preautorizar")] HttpRequest req, ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var sentencias = JsonConvert.DeserializeObject<List<PreautorizarSentenciaDto>>(requestBody);
        var preautorizarSentenciaComando = new PreautorizarSentenciaComando { Sentencias = sentencias };

        return await _processor.ExecuteAsync<PreautorizarSentenciaComando, string>(preautorizarSentenciaComando);
    }
}

