using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using Common.Functions;
using ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.InformacionParte;
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
    public class InformacionParteFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public InformacionParteFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("InformacionParte")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "InformacionParte" }, Summary = "Obtiene los datos especificos de la captura por Parte")]
        [OpenApiParameter(name: "asuntoNeunId", In = ParameterLocation.Query, Required = true, Type = typeof(long), Description = "Recibe el identificador del Asunto")]
        [OpenApiParameter(name: "personaId", In = ParameterLocation.Query, Required = false, Type = typeof(long), Description = "Recibe el identificador Persona")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<InformacionParteDto>), Description = "Respuesta exitosa, contiene una lista de objetos 'InformacionParteDto' en formato JSON.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "parte/captura")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"{req.Query["asuntoNeunId"]} {req.Query["personaId"]}");

            var consulta = new InformacionParteConsulta
            {
                AsuntoNeunId = Convert.ToInt64(req.Query["asuntoNeunId"]),
                PersonaId = Convert.ToInt64(req.Query["personaId"])
            };

            return await _processor.ExecuteAsync<InformacionParteConsulta, List<InformacionParteDto>>(consulta);
        }
    }
}

