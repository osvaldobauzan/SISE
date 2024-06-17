using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Contenido.Consulta;
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
    public class CatalogoContenidoFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public CatalogoContenidoFunction(IHttpRequestProcessor processor)
        {   
            _processor = processor;
        }

        [FunctionName("CatalogoContenido")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "CatalogoContenido" }, Summary = " Obtiene el contenido de las promociones")]
        [OpenApiParameter(name: "CatTipoOrganismoId", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Representa el identificador del tipo de organismo")]
        [OpenApiParameter(name: "CatTipoAsuntoId", In = ParameterLocation.Query, Required = false, Type = typeof(int?), Description = "Representa el identificador del tipo de asunto")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<ContenidoDto>), Description = "Catálogo de contenido obtenido con éxito")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "contenido")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"CatTipoOrganismoId: {req.Query["catTipoOrganismoId"]} CatTipoAsuntoId {req.Query["catTipoAsuntoId"]}");
            var consulta = new ObtieneCatalogoContenido
            {
                CatTipoOrganismoId = Convert.ToInt32(req.Query["catTipoOrganismoId"]),
                CatTipoAsuntoId = Convert.ToInt32(req.Query["catTipoAsuntoId"])
            };

            return await _processor.ExecuteAsync<ObtieneCatalogoContenido, List<ContenidoDto>>(consulta);
        }
    }
}
