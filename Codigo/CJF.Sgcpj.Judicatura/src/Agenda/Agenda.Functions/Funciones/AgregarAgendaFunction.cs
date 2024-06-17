using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Comandos.InsertarAgenda;
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
using Newtonsoft.Json;

namespace Agenda.Functions.Funciones;
public class AgregarAgendaFunction
{
    private readonly IHttpRequestProcessor _processor;

    public AgregarAgendaFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }

    [FunctionName("AgregarAgenda")]
    [OpenApiRequestBody(contentType: "json", bodyType: typeof(InsertarAgendaRequest), Description = "Objeto que se usa para insertar un oficio", Example = typeof(InsertarAgendaRequest))]
    [OpenApiOperation(operationId: "Run", tags: new[] { "AgregarAgenda" }, Summary = " Inserta una agenda de audiencia")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ResultadoInsertarAgendaDto), Description = "Confirma el guardado exitoso de la agenda de audiencia")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run(
     [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "agenda/agregar")] HttpRequest req,
     ILogger log)
    {

        log.LogInformation("C# HTTP trigger function processed a request.");

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

        InsertarAgendaRequest requestAudiencia = JsonConvert.DeserializeObject<InsertarAgendaRequest>(requestBody);
        return await _processor.ExecuteAsync<InsertarAgendaRequest, ResultadoInsertarAgendaDto>(requestAudiencia);
    }
}
