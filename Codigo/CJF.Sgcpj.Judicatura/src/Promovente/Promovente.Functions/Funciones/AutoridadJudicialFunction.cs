using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Promovente.Application.Usuarios.AutoridadJudicial.Comandos;
using Common.Functions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Promovente.Functions.Funciones
{
    public class AutoridadJudicialFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public AutoridadJudicialFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("GuardarAutoridadJudicial")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "GuardarAutoridadJudicial" }, Summary = " Guardan una Autoridad Judicial")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(long), Description = "Confirma el guardado exitoso de la Autoridad Judicial")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiRequestBody(contentType: "json", bodyType: typeof(AgregarAutoridadJudicialDto), Description = "Objeto que se usa para guardar una Autoridad Judicial", Example = typeof(AgregarAutoridadJudicialDto))]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "autoridadjudicial")] HttpRequest req,
            ILogger log)
        {

            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            AgregarAutoridadJudicialDto autoridadJudicialDto = JsonConvert.DeserializeObject<AgregarAutoridadJudicialDto>(requestBody);
            AgregarAutoridadJudicialComando comando = new AgregarAutoridadJudicialComando();

            comando.AutoridadJudicial = autoridadJudicialDto;
            return await _processor.ExecuteAsync<AgregarAutoridadJudicialComando, long>(comando);

        }
    }
}

