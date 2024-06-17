using System.IO;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.AgregarActuarioMasivo;
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
    public class AgregarActuarioMasivoFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public AgregarActuarioMasivoFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("AgregarActuarioMasivo")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "AgregarActuarioMasivo" }, Summary = "Asigna Actuario y tipo de notificaciones a varias partes")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.NoContent, contentType: "application/json", bodyType: typeof(bool), Description = "Indica TRUE si se pudo agregar a un actuario; en caso contrario, indica FALSE")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiRequestBody(contentType: "json", bodyType: typeof(AgregarActuarioMasivoM), Description = "Objeto que se usa para agregar a un actuario", Example = typeof(AgregarActuarioMasivoDto))]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "actuaria/actuariomultiple")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            AgregarActuarioMasivoDto actuario = JsonConvert.DeserializeObject<AgregarActuarioMasivoDto>(requestBody);
            AgregarActuarioMasivoComando actuarioComando = new AgregarActuarioMasivoComando();

            actuarioComando.Actuario = actuario;

            return await _processor.ExecuteAsync<AgregarActuarioMasivoComando, bool>(actuarioComando);

        }
    }
}

