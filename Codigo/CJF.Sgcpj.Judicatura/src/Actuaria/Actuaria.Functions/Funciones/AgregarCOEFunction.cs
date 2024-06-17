using System.IO;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.AgregarCOE;
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
    public class AgregarCOEFunction
    {
        private readonly IHttpRequestProcessor _processor;
        public AgregarCOEFunction(IHttpRequestProcessor processor){
            _processor = processor;
        }

        [FunctionName("AgregarCOE")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "AgregarCOE" }, Summary = "Agrega un COE")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.NoContent, contentType: "application/json", bodyType: typeof(bool), Description = "Indica TRUE si se pudo agregar a un COE; en caso contrario, indica FALSE")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiRequestBody(contentType: "json", bodyType: typeof(AgregarCOEM), Description = "Objeto que se usa para agregar un COE", Example = typeof(AgregarCOEDto))]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "actuaria/agregarCOE")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            AgregarCOEDto COE = JsonConvert.DeserializeObject<AgregarCOEDto>(requestBody);
            AgregarCOEComando COEComando = new AgregarCOEComando();

            COEComando.COE = COE;

            return await _processor.ExecuteAsync<AgregarCOEComando, bool>(COEComando);
        }
    }

}
