using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.Audiencia;
using Common.Functions;
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
    public class AudienciaFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public AudienciaFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("Audiencia")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Audiencia" }, Summary = "Obtienen la información de la audiencia")]
        [OpenApiParameter(name: "asuntoNeunId", In = ParameterLocation.Query, Required = true, Type = typeof(long), Description = "Recibe el identificador del Asunto")]
        [OpenApiParameter(name: "cuadernoId", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Recibe el identificador del cuaderno")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<AudienciaDto>), Description = "Respuesta exitosa, contiene una lista de objetos 'AudienciaDto' en formato JSON.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "audiencia")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"{req.Query["asuntoNeunId"]} {req.Query["cuadernoId"]}");

            var consulta = new AudienciaConsulta
            {
                AsuntoNeunId = Convert.ToInt64(req.Query["asuntoNeunId"]),
                CuadernoId = Convert.ToInt32(req.Query["cuadernoId"])
            };

            return await _processor.ExecuteAsync<AudienciaConsulta, AudienciaDto>(consulta);
        }
    }
}

