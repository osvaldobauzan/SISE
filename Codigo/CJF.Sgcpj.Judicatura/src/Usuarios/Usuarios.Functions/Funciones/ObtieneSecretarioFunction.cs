using System;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Usuarios.Application.Usuarios.Secretarios.Consulta.ObtenerSecretario;
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
    public class ObtieneSecretarioFunction
    {
        private readonly IHttpRequestProcessor _processor;
        public ObtieneSecretarioFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("ObtieneSecretarioFunction")]

        [OpenApiOperation(operationId: "Run", tags: new[] { "ObtieneSecretario" }, Summary = " Obteniene el id del ultimo secretario de la ultima promocion (con ultima fecha y con archivo) asociada al expediente")]
        [OpenApiParameter(name: "AsuntoNeunId", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Id del expediente")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ObtieneSecretarioDto), Description = "Id del secretario obtenido con éxito")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "secretario/sugerido")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var consulta = new ObtieneSecretario 
            {                
                AsuntoNeunId = Convert.ToInt64(req.Query["AsuntoNeunId"]),
            };
            return await _processor.ExecuteAsync<ObtieneSecretario, ObtieneSecretarioDto>(consulta);
        }
    }
}
