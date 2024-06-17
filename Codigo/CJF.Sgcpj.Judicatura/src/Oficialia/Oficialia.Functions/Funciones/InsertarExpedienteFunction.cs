using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Common.Functions;
using CJF.Sgcpj.Judicatura.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.InsertarPromocion;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System.Net;
using CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.InsertarExpediente;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;

namespace Oficialia.Functions.Funciones
{
    public class InsertarExpedienteFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public InsertarExpedienteFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        /// <summary>
        /// Inserta el expediente
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("InsertarExpediente")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "InsertarExpediente" }, Summary = " Inserta un expediente")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ResultadoInsertarExpedienteDto), Description = "Confirma el guardado exitoso del expediente")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiRequestBody(contentType: "json", bodyType: typeof(InsertarExpedienteDto), Description = "Objeto que se usa para guardar el expediente", Example = typeof(InsertarExpedienteDto))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "expedientesinsertar")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            InsertarExpedienteDto expediente = JsonConvert.DeserializeObject<InsertarExpedienteDto>(requestBody);
            InsertarExpedienteComando expedienteComando = new InsertarExpedienteComando();
            expedienteComando.Expediente = expediente;

            return await _processor.ExecuteAsync<InsertarExpedienteComando, ResultadoInsertarExpedienteDto>(expedienteComando);
        }
    }
}
