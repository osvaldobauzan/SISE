using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.Sentencia;
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
    public class EstadoSentenciaFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public EstadoSentenciaFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("EstadoSentencia")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "EstadoSentencia" }, Summary = "Obtienen el estado general de un expediente")]
        [OpenApiParameter(name: "asuntoNeunId", In = ParameterLocation.Query, Required = true, Type = typeof(long), Description = "Recibe el identificador del Asunto")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<EstadoSentenciaDto>), Description = "Respuesta exitosa, contiene una lista de objetos 'EstadoSentenciaDto' en formato JSON.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inv�lidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "sentencia")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"{req.Query["asuntoNeunId"]}");

            var consulta = new EstadoSentenciaConsulta
            {
                AsuntoNeunId = Convert.ToInt64(req.Query["asuntoNeunId"])
            };

            return await _processor.ExecuteAsync<EstadoSentenciaConsulta, EstadoSentenciaDto>(consulta);
        }
    }
}

