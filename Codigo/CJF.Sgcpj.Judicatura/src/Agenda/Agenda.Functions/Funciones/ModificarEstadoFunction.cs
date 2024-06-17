using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Comandos.ModificarEstado;
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
public class ModificarEstadoFunction
{
    private readonly IHttpRequestProcessor _processor;
    public ModificarEstadoFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }

    [FunctionName("ModificarEstadoAudiencia")]
    [OpenApiRequestBody(contentType: "json", bodyType: typeof(ModificarEstadoRequest), Description = "Objeto que se usa para modificar el estado de una audiencia", Example = typeof(ModificarEstadoRequest))]
    [OpenApiOperation(operationId: "Run", tags: new[] { "ModificarEstado" }, Summary = "Modificar estado audiencia")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Int64), Description = "Respuesta exitosa")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "Error interno en el servidor")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Estado/Audiencia")] HttpRequest req,
        ILogger log)
    {

        log.LogInformation("C# HTTP trigger function processed a request. ActualizaParte");

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        ModificarEstadoRequest param = JsonConvert.DeserializeObject<ModificarEstadoRequest>(requestBody);
        return await _processor.ExecuteAsync<ModificarEstadoRequest, ResultadoModificarEstadoDto>(param);


    }

}
