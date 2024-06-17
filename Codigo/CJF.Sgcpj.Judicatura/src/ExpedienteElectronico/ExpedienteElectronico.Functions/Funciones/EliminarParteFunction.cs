using System.IO;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using Common.Functions;
using ExpedienteElectronico.Application.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace ExpedienteElectronico.Functions.Funciones
{
    public class EliminarParteFunction
    {
        private readonly IHttpRequestProcessor _processor;
        public EliminarParteFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }
        [FunctionName("EliminarParte")]
        [OpenApiRequestBody(contentType: "json", bodyType: typeof(PersonaAsuntoDelete), Description = "Objeto que se usa para insertar un oficio", Example = typeof(PersonaAsuntoDelete))]
        [OpenApiOperation(operationId: "Run", tags: new[] { "EliminarParte" }, Summary = "Elimina una parte")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Respuesta exitosa")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "Error interno en el servidor")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "EliminarParte")] HttpRequest req,
        ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request. EliminarParte");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            PersonaAsuntoDelete param = JsonConvert.DeserializeObject<PersonaAsuntoDelete>(requestBody);
            return await _processor.ExecuteAsync<PersonaAsuntoDelete, bool>(param);
        }
    }
}
