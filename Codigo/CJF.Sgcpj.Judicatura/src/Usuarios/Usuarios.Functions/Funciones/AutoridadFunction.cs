using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Usuarios.Application.Usuarios.AutoridadJudicial.Consulta.AutoridadTablero;
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
    public class AutoridadFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public AutoridadFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("Autoridad")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Autoridad" }, Summary = " Obtiene una lista de autoridades de acuerdo a sus promoción")]
        [OpenApiParameter(name: "AsuntoNeunId", In = ParameterLocation.Query, Required = true, Type = typeof(long), Description = "Representa el identificador del AsuntoNeun")]
        [OpenApiParameter(name: "NoExp", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "Representa el número del expediente")]
        [OpenApiParameter(name: "Texto", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "Texto que se usa para filtrar las autoridades")]
        [OpenApiParameter(name: "Modulo", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "Parametro que nos ayuda a identificar en que modulo estamos Oficialia = 1 , Tramite = 2")]
        [OpenApiParameter(name: "Tipo parte", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "Parametro que nos ayuda a identificar que tipo de persona es la queremos ver: Persona = 1,Autoridad = 2")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<ObtieneAutoridadDto>), Description = "Autoridades obtenidas con éxito")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "autoridad")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"AsuntoNeunId: {req.Query["AsuntoNeunId"]} NoExp: {req.Query["NoExp"]} " +
                               $"texto: {req.Query["Texto"]} Modulo: {req.Query["Modulo"]} " +
                               $"TipoParte: {req.Query["TipoParte"]}");

            var consulta = new ObtieneAutoridadConsulta
            {
                AsuntoNeunId = Convert.ToInt64(req.Query["AsuntoNeunId"]),
                NoExp = req.Query["NoExp"],
                Texto = req.Query["Texto"],
                Modulo = Convert.ToInt32(req.Query["Modulo"]),
                TipoParte = Convert.ToInt32(req.Query["TipoParte"]),
            };

            return await _processor.ExecuteAsync<ObtieneAutoridadConsulta, List<ObtieneAutoridadDto>>(consulta);
        }
    }
}

