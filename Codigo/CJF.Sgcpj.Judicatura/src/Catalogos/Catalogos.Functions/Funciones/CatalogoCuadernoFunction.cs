using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Cuadernos.Consulta;
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
    public class CatalogoCuadernoFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public CatalogoCuadernoFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("CatalogoCuaderno")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "CatalogoCuaderno" }, Summary = " Obtiene los cuadernos para las promociones")]
        [OpenApiParameter(name: "TipoAsuntoId", In = ParameterLocation.Query, Required = false, Type = typeof(int?), Description = "El id del tipoasunto")]
        [OpenApiParameter(name: "CuadernoId", In = ParameterLocation.Query, Required = false, Type = typeof(int?), Description = "El id del cuaderno")]
        [OpenApiParameter(name: "AsuntoNeunId", In = ParameterLocation.Query, Required = false, Type = typeof(long?), Description = "El id del Expediente necesario para amparo en revisión")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<CuadernoDto>), Description = "Catálogo de cuaderno obtenido con éxito")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "cuaderno")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"Tipo asunto: {req.Query["TipoAsuntoId"]} Id cuaderno: {req.Query["CuadernoId"]}");
            var aux = 0;
            int? tipoAsuntoId = int.TryParse(req.Query["tipoAsuntoId"].ToString(), out aux) ? Convert.ToInt32(req.Query["tipoAsuntoId"]) : null;
            int? cuadernoid = int.TryParse(req.Query["cuadernoId"].ToString(), out aux) ? Convert.ToInt32(req.Query["cuadernoId"]) : null;
            long? asuntoNeunId = int.TryParse(req.Query["asuntoNeunId"].ToString(), out aux) ? Convert.ToInt32(req.Query["asuntoNeunId"]) : null;
            var consulta = new ObtieneCatalogoCuaderno
            {
                TipoAsuntoId = tipoAsuntoId,
                CuadernoId = cuadernoid, 
                AsuntoNeunId = asuntoNeunId
            };

            return await _processor.ExecuteAsync<ObtieneCatalogoCuaderno, List<CuadernoDto>>(consulta);
        }
    }
}
