using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Usuarios.Application.Usuarios.AutoridadJudicial.Consulta;
using CJF.Sgcpj.Judicatura.Usuarios.Application.Usuarios.AutoridadJudicial.Consulta.AutoridadJudicialExistente;
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
    public class AutoridadJudicialFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public AutoridadJudicialFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("AutoridadJudicial")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "AutoridadJudicial" }, Summary = " Obteniene la busqueda para una Autoridad Judicial")]
        [OpenApiParameter(name: "Nombre", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Busca una autoridad judicial por el Nombre")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<AutoridadJudicialDto>), Description = "Autoridades judiciales obtenidos con éxito")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "autoridadjudicial")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"Nombre: {req.Query["Nombre"]}");
            var consulta = new ObtieneAutoridadJudicial
            {
                Nombre = req.Query["Nombre"]
            };

            return await _processor.ExecuteAsync<ObtieneAutoridadJudicial, List<AutoridadJudicialDto>>(consulta);
        }

        [FunctionName("AutoridadJudicialPorAsunto")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "AutoridadJudicialPorAsunto" }, Summary = " Obteniene la busqueda para una Autoridad Judicial por asunto")]
        [OpenApiParameter(name: "AsuntoNeunId", In = ParameterLocation.Query, Required = true, Type = typeof(long), Description = "Busca una autoridad judicial por el Asunto y Expediente")]
        [OpenApiParameter(name: "NumeroExpediente", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Busca una autoridad judicial por el Asunto y Expediente")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<ObtieneAutoridadJudicialExistenteDto>), Description = "Autoridades judiciales obtenidos con éxito")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run2(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "autoridadjudicialasunto")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"AsuntoNeunId: {req.Query["AsuntoNeunId"]} NumeroExpediente: {req.Query["NumeroExpediente"]}");
            var consulta = new ObtieneAutoridadJudicialExistenteConsulta
            {
                AsuntoNeunId = Convert.ToInt64(req.Query["AsuntoNeunId"]),
                NoExp = req.Query["NumeroExpediente"]
            };

            return await _processor.ExecuteAsync<ObtieneAutoridadJudicialExistenteConsulta, List<ObtieneAutoridadJudicialExistenteDto>>(consulta);
        }
    }
}
