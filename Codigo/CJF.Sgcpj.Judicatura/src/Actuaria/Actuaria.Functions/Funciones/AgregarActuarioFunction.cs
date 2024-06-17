using System.IO;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.AgregarActuario;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
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

namespace Actuaria.Functions.Funciones
{
    public class AgregarActuarioFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public AgregarActuarioFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("AgregarActuario")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "AgregarActuario" }, Summary = "Agrega un actuario")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.NoContent, contentType: "application/json", bodyType: typeof(bool), Description = "Indica TRUE si se pudo agregar a un actuario; en caso contrario, indica FALSE")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiRequestBody(contentType: "json", bodyType: typeof(AgregarActuarioM), Description = "Objeto que se usa para agregar a un actuario", Example = typeof(AgregarActuarioDto))]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "actuaria/actuario")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            AgregarActuarioDto actuario = JsonConvert.DeserializeObject<AgregarActuarioDto>(requestBody);
            AgregarActuarioComando actuarioComando =new AgregarActuarioComando();

            actuarioComando.Actuario = actuario;

            return await _processor.ExecuteAsync<AgregarActuarioComando, bool>(actuarioComando);
        }

        [FunctionName("EditarActuario")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "EditarActuario" }, Summary = "Edita un actuario")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.NoContent, contentType: "application/json", bodyType: typeof(bool), Description = "Indica TRUE si se pudo editar a un actuario; en caso contrario, indica FALSE")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiRequestBody(contentType: "json", bodyType: typeof(AgregarActuarioM), Description = "Objeto que se usa para agregar a un actuario", Example = typeof(AgregarActuarioDto))]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run2(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "actuaria/actuario")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            AgregarActuarioDto actuario = JsonConvert.DeserializeObject<AgregarActuarioDto>(requestBody);
            AgregarActuarioComando actuarioComando = new AgregarActuarioComando();

            actuarioComando.Actuario = actuario;

            return await _processor.ExecuteAsync<AgregarActuarioComando, bool>(actuarioComando);
        }
    }
}

