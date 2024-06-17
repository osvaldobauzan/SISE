using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Usuarios.Application.Usuarios.Secretarios;
using Common.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Usuarios.Functions.Funciones
{
    public class CatalogoSecretarioFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public CatalogoSecretarioFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("CatalogoSecretarioFunction")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "CatalogoSecretario" }, Summary = " Obteniene el cat�logo de Secretarios")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<CatalogoSecretarioDto>), Description = "Cat�logos de secretarios obtenidos con �xito")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inv�lidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "secretario")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var consulta = new ObtieneCatalogoSecretario();
            return await _processor.ExecuteAsync<ObtieneCatalogoSecretario, List<CatalogoSecretarioDto>>(consulta);
        }
    }
}