using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MediatR;
using System.Net;
using System.Collections.Generic;
using CJF.Sgcpj.Judicatura.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.AsociaExpediente;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Common.Functions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;

namespace Oficialia.Functions.Funciones
{
    public class VinculaExpedienteFunction
    {
        private readonly IHttpRequestProcessor _processor;
        public VinculaExpedienteFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("AsociarExpediente")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "AsociarExpediente" }, Summary = " Asocia Expediente")]
        [OpenApiParameter(name: "asuntoAlias", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Representa el asunto alias por el cual se desea obtener")]
        [OpenApiParameter(name: "catTipoAsuntoId", In = ParameterLocation.Query, Required = false, Type = typeof(int?), Description = "Representa el identificador del tipo de asunto")]        
        [OpenApiParameter(name: "modulo", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "Valor para saber en que modulo nos encontramos, 1 = Oficialia , 2 = Tramite")]
        [OpenApiParameter(name: "catTipoProcedimiento", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "Valor para identificar que tipo procedimiento es la promocion, en caso de ser Medidas Precautorias")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(AsociarExpedienteDto), Description = "Respuesta exitosa, se asocio correctamente el expediente")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "asociarexpediente")] HttpRequest req,
            ILogger log)
        {
                log.LogInformation("C# HTTP trigger function processed a request.");
                log.LogInformation($"asuntoAlias: {req.Query["asuntoAlias"]} catTipoAsuntoId: {req.Query["catTipoAsuntoId"]} modulo: {req.Query["modulo"]}");
                var aux = 0;
                int? catTipoAsuntoId = int.TryParse(req.Query["catTipoAsuntoId"].ToString(), out aux) ? Convert.ToInt32(req.Query["catTipoAsuntoId"]) : null;
                int? catTipoProcedimiento = int.TryParse(req.Query["catTipoProcedimiento"].ToString(), out aux) ? Convert.ToInt32(req.Query["catTipoProcedimiento"]) : null;
                
                var consulta = new AsociarExpediente() { 
                    AsuntoAlias = req.Query["asuntoAlias"],
                    CatTipoAsuntoId = catTipoAsuntoId,
                    Modulo = Convert.ToInt32(req.Query["modulo"]),
                    CatTipoProcedimiento = catTipoProcedimiento
                };

                return await _processor.ExecuteAsync<AsociarExpediente, List<AsociarExpedienteDto>>(consulta);
        }
    }
}
