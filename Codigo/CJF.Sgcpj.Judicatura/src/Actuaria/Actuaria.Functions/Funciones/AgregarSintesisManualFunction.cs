using System.IO;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.AgregarSintesisManual;
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

namespace Actuaria.Functions.Funciones;
public class AgregarSintesisManualFunction
{
    private readonly IHttpRequestProcessor _httpRequestProcessor;


    public AgregarSintesisManualFunction(IHttpRequestProcessor httpRequestProcessor)
    {
        _httpRequestProcessor = httpRequestProcessor;
    }

    [FunctionName("AgregarSintesisManual")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "Actuaria" }, Summary = " Inserta una sintesis manual")]
    [OpenApiRequestBody(contentType: "json", bodyType: typeof(AgregarSintesisManualRequest), Description = "Objeto que se usa para guardar la sintasis manual", Example = typeof(AgregarSintesisManualRequest))]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Respuesta exitosa, true.")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]

    public async Task<IActionResult> Run(
                    [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "actuaria/agregarsintesismanual")] HttpRequest request, ILogger log)
    {
        string requestBody = await new StreamReader(request.Body).ReadToEndAsync();
        AgregarSintesisManualRequest consulta = JsonConvert.DeserializeObject<AgregarSintesisManualRequest>(requestBody);

        log.LogInformation($"{request.Body}");
        return await _httpRequestProcessor.ExecuteAsync<AgregarSintesisManualRequest, bool>(consulta);
    }


}
