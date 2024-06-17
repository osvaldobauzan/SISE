using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System.Net;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using Common.Functions;
using CJF.Sgcpj.Judicatura.Promovente.Application.Promoventes.Comandos.AgregarPromoventes;

namespace Promovente.Functions.Funciones
{
    public class PromoventesFunction
    {
        private readonly IHttpRequestProcessor _processor;
        public PromoventesFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("GuardarPromoventes")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "GuardarPromoventes" }, Summary = "Guarda los promoventes")]
        [OpenApiRequestBody(contentType: "json", bodyType: typeof(AgregarPromoventesDto), Description = "Objeto promovente", Example = typeof(AgregarPromoventesDto))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(long), Description = "Confirma el guardado exitoso del Promovente")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "promoventes")] HttpRequest req,
            ILogger log)
        {

            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            AgregarPromoventesDto promoventesDto = JsonConvert.DeserializeObject<AgregarPromoventesDto>(requestBody);
            AgregarPromoventesComando comando = new AgregarPromoventesComando();

            comando.Promovente = promoventesDto;

            return await _processor.ExecuteAsync<AgregarPromoventesComando, long>(comando);

        }
    }
}
