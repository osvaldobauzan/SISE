using System;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using Common.Functions;
using ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.DatosGenerales;
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
    public class DatosGeneralesFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public DatosGeneralesFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("DatosGenerales")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "DatosGenerales" }, Summary = "Obtiene los datos generales ")]
        [OpenApiParameter(name: "asuntoNeunId", In = ParameterLocation.Query, Required = true, Type = typeof(long), Description = "Recibe el identificador del Asunto")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(DatosGeneralesDto), Description = "Respuesta exitosa, contiene un objeto 'DatosGeneralesDto' en formato JSON.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "datosgenerales")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"{req.Query["asuntoNeunId"]}");

            var consulta = new DatosGeneralesConsulta
            {
                AsuntoNeunId = Convert.ToInt64(req.Query["asuntoNeunId"])
            };

            return await _processor.ExecuteAsync<DatosGeneralesConsulta, DatosGeneralesDto>(consulta);
        }
    }
}

