using System.IO;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Comandos.PlantillaBlobComando;
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

namespace Tramites.Functions.Funciones
{
    public class PlantillaFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public PlantillaFunction(IHttpRequestProcessor httpRequestProcessor, IWordsUtil wordsUtil)
        {
            _processor = httpRequestProcessor;
        }

        [FunctionName("Plantilla")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Plantilla" }, Summary = " Genera un Oficio tomando como base una Plantilla")]
        [OpenApiRequestBody("application/json", typeof(PlantillaBlobDto))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Indica TRUE si se genero el Oficio; en caso contrario, indica FALSE")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "plantilla")] HttpRequest req,
            ILogger log)
        {

            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            PlantillaBlobDto plantilla = JsonConvert.DeserializeObject<PlantillaBlobDto>(requestBody);
            PlantillaBlobComando plantillaComando = new PlantillaBlobComando();
            plantillaComando.Plantilla = plantilla;
            return await _processor.ExecuteAsync<PlantillaBlobComando, bool>(plantillaComando);
        }
    }
}

