using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Usuarios.Application.Usuarios.PromoventeExistente.Consulta;
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
    public class CatalogoPromoventeExistenteFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public CatalogoPromoventeExistenteFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("CatalogoPromoventeExistente")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "CatalogoPromoventeExistente" }, Summary = " Obtiene los promoventes existentes de un asunto")]
        [OpenApiParameter(name: "AsuntoNeunId", In = ParameterLocation.Query, Required = true, Type = typeof(long), Description = "Representa el identificador del Asunto")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<CatalogoPromoventeExistenteDto>), Description = "Catálogo para Promovente Existente obtenido con éxito")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "promoventeexistente")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"AsuntoNeunId: {req.Query["AsuntoNeunId"]}");
            var consulta = new ObtieneCatalogoPromoventeExistente
            {
                AsuntoNeunId = Convert.ToInt64(req.Query["AsuntoNeunId"])
            };

            return await _processor.ExecuteAsync<ObtieneCatalogoPromoventeExistente, List<CatalogoPromoventeExistenteDto>>(consulta);
        }
    }
}

