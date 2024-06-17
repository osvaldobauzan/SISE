using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.CalcularRegistro;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Common.Functions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;

namespace Oficialia.Functions.Funciones
{
    public class CalcularRegistroFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public CalcularRegistroFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("Registro")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Registro" }, Summary = " Calcula el numero de registro")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(RegistroDto), Description = "Se genero correctamente el numero de registro")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "registro")] HttpRequest req,

            ILogger log)
        {
                log.LogInformation("C# HTTP trigger function processed a request.");
                return await _processor.ExecuteAsync<ObtieneCalculoRegistro, RegistroDto>(new ObtieneCalculoRegistro());
        }
    }
}
