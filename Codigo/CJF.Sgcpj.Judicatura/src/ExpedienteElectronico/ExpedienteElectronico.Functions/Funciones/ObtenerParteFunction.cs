using System;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.Common.Models;
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

namespace ExpedienteElectronico.Functions.Funciones
{
    public class ObtenerParteFunction
    {
        private readonly IHttpRequestProcessor _processor;
        public ObtenerParteFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }
        [FunctionName("ObtenerParte")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "ObtenerParte" }, Summary = " Obtiene el detalle de una parte")]
        [OpenApiParameter(name: "personaId", In = ParameterLocation.Query, Required = false, Type = typeof(long), Description = "")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(FichaTecnicaExpedienteElectronico), Description = "Respuesta exitosa")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "Error interno en el servidor")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ObtenerParte")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request. ExpedienteElectronico");
            var consulta = new PersonaAsuntoFiltro
            {
                PersonaId = (req.Query != null && req.Query.ContainsKey("personaId")) ? Convert.ToInt32(req.Query["personaId"]) : 0,
            };
            return await _processor.ExecuteAsync<PersonaAsuntoFiltro, PersonaAsuntoDTO>(consulta);
        }
    }
}
