using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.TipoPromovente.Consulta;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using Common.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Catalogos.Functions.Funciones
{
    public class CatalogoTipoPromoventeFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public CatalogoTipoPromoventeFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }
        [FunctionName("TipoPromovente")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "CatalogoTipoPromovente" }, Summary = " Obtiene los tipos de promoventes para las promociones")]
        [OpenApiParameter(name: "CatTipoAsuntoId", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "REPRESENTA EL IDENTIFICADOR DEL TIPO DE ASUNTO")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<CatalogoTipoPromoventeDto>), Description = "Catálogo de tipo de promovente obtenido con éxito")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
                    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tipopromovente")] HttpRequest req,
                    ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var consulta = new ObtieneCatalogoTipoPromovente
            {
                CatTipoAsuntoId = Convert.ToInt32(req.Query["catTipoAsuntoId"])
            };
            return await _processor.ExecuteAsync<ObtieneCatalogoTipoPromovente, List<CatalogoTipoPromoventeDto>>(consulta);
        }
    }
}
