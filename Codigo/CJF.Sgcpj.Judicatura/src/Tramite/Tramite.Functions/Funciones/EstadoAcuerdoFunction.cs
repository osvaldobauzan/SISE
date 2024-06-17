using System.IO;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Comandos.EstadoAcuerdoComando;
using CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Comandos.EstadoAcuerdoComandoRE;
using Common.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Tramites.Functions.Funciones
{
    public class EstadoAcuerdoFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public EstadoAcuerdoFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("EstadoAcuerdoRE")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "EstadoAcuerdo" }, Summary = "Actualiza el estado del acuerdo")]
        [OpenApiRequestBody("application/json", typeof(EstadoAcuerdoDto))]
        [OpenApiParameter(name: "estado", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "estado")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Indica TRUE si se pudo actualizar el estado del acuerdo; en caso contrario, indica FALSE")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> RunRE(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "tramite/estado/{estado:int}")] HttpRequest req,
            ILogger log, int estado)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            EstadoAcuerdoREDto acuerdo = JsonConvert.DeserializeObject<EstadoAcuerdoREDto>(requestBody);
            EstadoAcuerdoREComando acuerdoComando = new EstadoAcuerdoREComando();
            acuerdoComando.Acuerdo = acuerdo;
            acuerdoComando.Acuerdo.Estado = estado;

            return await _processor.ExecuteAsync<EstadoAcuerdoREComando, bool>(acuerdoComando);
        }
    }
}

