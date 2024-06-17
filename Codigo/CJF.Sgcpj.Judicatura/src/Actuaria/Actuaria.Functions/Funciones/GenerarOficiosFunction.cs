using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System.Net;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using Newtonsoft.Json;
using System.IO;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.GenerarOficio;
using Common.Functions;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;

namespace Actuaria.Functions.Funciones;
public class GenerarOficiosFunction
{
    private readonly IHttpRequestProcessor _processor;

    public GenerarOficiosFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }

    [FunctionName("GenerarOficiosFunction")]
    [OpenApiOperation(operationId: "Post", tags: new[] { "GenerarOficios" }, Summary = "Genera Oficio")]
    [OpenApiRequestBody(contentType: "json", bodyType: typeof(GenerarOficioRequestDto), Description = "Metodo para generar oficio", Example = typeof(GenerarOficioRequestDto))]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ResponseDatosGenerarFolioM), Description = "Respuesta exitosa, contiene una lista de oficios que corresponden al folio")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Post(
       [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "actuaria/generaroficios")] HttpRequest req,
       ILogger log)
    {

        log.LogInformation("C# HTTP trigger function processed a request.");

        var request = new GenerarOficioRequestDto();

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

        request = JsonConvert.DeserializeObject<GenerarOficioRequestDto>(requestBody);

        return await _processor.ExecuteAsync<GenerarOficioRequestDto, ResponseDatosGenerarFolioM>(request);

    }

}
