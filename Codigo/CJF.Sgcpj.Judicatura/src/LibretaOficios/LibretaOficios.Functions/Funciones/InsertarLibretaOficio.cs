using System;
using CJF.Sgcpj.Judicatura.LibretaOficios.Application.Common.Models;
using Common.Functions;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;

namespace LibretaOficios.Functions.Funciones;
public class InsertarOficioFunction
{
    private readonly IHttpRequestProcessor _processor;
    public InsertarOficioFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }
    [FunctionName("InsertarOficio")]
    [OpenApiRequestBody(contentType: "json", bodyType: typeof(InsertarOficioFiltro), Description = "Objeto que se usa para insertar un oficio", Example = typeof(LibretaOficioFiltro))]
    [OpenApiOperation(operationId: "Run", tags: new[] { "InsertarOficio" }, Summary = "Inserta el Oficio")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Respuesta exitosa para la creación de Oficios")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "Error interno en el servidor al crear el oficio")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor al crear el oficio")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "libretaOficio/InsertarOficio")] HttpRequest req,
        ILogger log)
    {

        log.LogInformation("C# HTTP trigger function processed a request. LibretaOficio");
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        InsertarOficioFiltro oficio = JsonConvert.DeserializeObject<InsertarOficioFiltro>(requestBody);

        return await _processor.ExecuteAsync<InsertarOficioFiltro, bool>(oficio);
    }
}
