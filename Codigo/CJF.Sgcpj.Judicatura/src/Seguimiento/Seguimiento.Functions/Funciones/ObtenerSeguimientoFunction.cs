using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Seguimientos.Consulta;
using Common.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Seguimientos = CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;

namespace Oficialia.Functions.Funciones
{
    public class ObtenerSeguimientoFunction
    {
        private readonly IHttpRequestProcessor _processor;
        public ObtenerSeguimientoFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        /// <summary>
        /// Inserta el expediente
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("ObtenerSeguimiento")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "obtenerSeguimiento" }, Summary = " Obtiene los seguimientos")]
        [OpenApiParameter(name: "FechaIni", In = ParameterLocation.Query, Required = true, Type = typeof(DateTime), Description = "Representa la fecha inicial de la consulta")]
        [OpenApiParameter(name: "FechaFin", In = ParameterLocation.Query, Required = true, Type = typeof(DateTime), Description = "Representa la fecha final de la consulta")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Seguimientos.Seguimiento), Description = "Lista de parametros para obteber seguimientos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiRequestBody(contentType: "json", bodyType: typeof(List<Seguimientos.Seguimiento>), Description = "Objeto que se usa para guardar el seguimiento", Example = typeof(List<Seguimientos.Seguimiento>))]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ObtenerSeguimiento")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"FechaIni: {req.Query["FechaIni"]} FechaFin {req.Query["FechaFin"]}");

            var seguimientoComando = new ObtenerSeguimientoConsulta()
            {
                Seguimiento = new Seguimientos.Seguimiento()
                {
                    FechaIni = (req.Query["FechaIni"] != "" ? DateTime.Parse( req.Query["FechaIni"]): DateTime.Now),
                    FechaFin = (req.Query["FechaFin"] != "" ? DateTime.Parse(req.Query["FechaFin"]) : DateTime.Now)
                    
                }
            };

            return await _processor.ExecuteAsync<ObtenerSeguimientoConsulta, List<Seguimientos.Seguimiento>>(seguimientoComando);
        }
    }
}
