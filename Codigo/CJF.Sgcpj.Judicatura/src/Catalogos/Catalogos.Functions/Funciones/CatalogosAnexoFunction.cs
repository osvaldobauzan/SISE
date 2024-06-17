using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Anexo.Consulta;
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
    public class CatalogosAnexoFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public CatalogosAnexoFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("CatalogosAnexo")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "CatalogosAnexo" }, Summary = " Obtiene los catalogos para Anexo(Tipo Anexo, Descripción, Carácter)")]
        [OpenApiParameter(name: "CatTipoCatalogoAsuntoId", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "ID del tipo de catálogo de asunto, utilizado para obtener los catalogos. Los valores esperados son 502 (Tipo de Anexo), 17 (Descripción) y 27 (Caráter)")]
        [OpenApiParameter(name: "CatTipoAsuntoId", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Representa el identificador del tipo de asunto")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<CatalogosAnexoDto>), Description = "Catálogo para Anexo obtenido con éxito")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "anexo")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"CatTipoCatalogoAsuntoId: {req.Query["catTipoCatalogoAsuntoId"]} CatTipoAsuntoId {req.Query["catTipoAsuntoId"]}");
            var consulta = new ObtieneCatalogosAnexo
            {
                CatTipoCatalogoAsuntoId = Convert.ToInt32(req.Query["catTipoCatalogoAsuntoId"]),
                CatTipoAsuntoId = Convert.ToInt32(req.Query["catTipoAsuntoId"])
            };
            return await _processor.ExecuteAsync<ObtieneCatalogosAnexo, List<CatalogosAnexoDto>>(consulta);
        }
    }
}

