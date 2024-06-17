using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerPromocionXExpediente;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using Common.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Oficialia.Functions.Funciones
{
    public class ObtenerPromocionXExpedienteFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public ObtenerPromocionXExpedienteFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("PromocionXExpediente")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "PromocionXExpediente" }, Summary = "Se uliliza para listar las promociones por expediente")]
        [OpenApiParameter(name: "AsuntoNeunId", In = ParameterLocation.Query, Required = true, Type = typeof(long), Description = "Representa el AsuntoNeaunId")]
        [OpenApiParameter(name: "NoExpediente", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Representa el numero del expediente")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<ObtienePromocionXExpedienteDto>), Description = "Promociones obtenidas con éxito")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "promocion/expediente")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"AsuntoNeunId: {req.Query["asuntoNeunId"]} NoExpediente {req.Query["noExpediente"]}");
            var consulta = new ObtienePromocionXExpediente
            {
                AsuntoNeunId = Convert.ToInt64(req.Query["asuntoNeunId"]),
                NoExpediente = req.Query["noExpediente"]
            };

            return await _processor.ExecuteAsync<ObtienePromocionXExpediente, List<ObtienePromocionXExpedienteDto>>(consulta);
        }
    }
}

