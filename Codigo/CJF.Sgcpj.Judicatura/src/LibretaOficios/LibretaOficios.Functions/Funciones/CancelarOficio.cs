using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Common.Functions;
using CJF.Sgcpj.Judicatura.LibretaOficios.Application.Common.Models;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using System.IO;

namespace LibretaOficios.Functions.Funciones;


public class CancelarOficioFunction
{
    private readonly IHttpRequestProcessor _processor;
    public CancelarOficioFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }
    [FunctionName("CancelarOficio")]
    [OpenApiRequestBody(contentType: "json", bodyType: typeof(CancelarOficioFiltro), Description = "Objeto que se usa para cancelar un acuerdo", Example = typeof(LibretaOficioFiltro))]
    [OpenApiOperation(operationId: "Run", tags: new[] { "CancelarOficio" }, Summary = "Cancela el oficio")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Respuesta exitosa para la Cancelacion de Oficios")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "Error interno en el servidor al cancelar el oficio")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor al cancelar el oficio")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "libretaOficio/CancelarOficio")] HttpRequest req,
        ILogger log)
    {

        log.LogInformation("C# HTTP trigger function processed a request. LibretaOficio");
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        CancelarOficioFiltro oficio = JsonConvert.DeserializeObject<CancelarOficioFiltro>(requestBody);

        return await _processor.ExecuteAsync<CancelarOficioFiltro, bool>(oficio);
    }
}

