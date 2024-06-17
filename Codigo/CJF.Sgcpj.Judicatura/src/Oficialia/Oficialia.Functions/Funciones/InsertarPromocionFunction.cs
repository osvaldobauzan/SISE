using System.IO;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.InsertarPromocion;
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

namespace Oficialia.Functions.Funciones
{

    public class InsertarPromocionFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public InsertarPromocionFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }
        /// <summary>
        /// Obtiene las promociones con los filtros especificados
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("InsertarPromocion")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "InsertarPromocion" }, Summary = " Inserta una promoción")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(long), Description = "Confirma el guardado exitoso de la promoción")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiRequestBody(contentType: "json", bodyType: typeof(InsertarPromocionDto), Description = "Objeto que se usa para guardar la promocion", Example = typeof(InsertarPromocionDto))]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "promocionesinsertar")]
            HttpRequest req,
            ILogger log)
        {

            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            InsertarPromocionDto promocion = JsonConvert.DeserializeObject<InsertarPromocionDto>(requestBody);
            InsertarPromocionComando promocionComando = new InsertarPromocionComando();
            promocionComando.Promocion = promocion;

            return await _processor.ExecuteAsync<InsertarPromocionComando, int>(promocionComando);
        }
    }
}
