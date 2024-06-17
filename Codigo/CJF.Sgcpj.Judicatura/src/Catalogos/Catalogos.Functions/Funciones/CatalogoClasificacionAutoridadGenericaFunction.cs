using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.ClasificacionAutoridadGenerica.Consulta;
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
    public class CatalogoClasificacionAutoridadGenericaFunction
    {
        private readonly IHttpRequestProcessor _processor;
        public CatalogoClasificacionAutoridadGenericaFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }
        [FunctionName("CatalogoClasificacionAutoridadGenerica")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "CatalogoClasificacionAutoridadGenerica" }, Summary = "Obtiene el catálogo de Clasificación Autoridad Genérica")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<CatalogoClasificacionAutoridadGenericaDTO>), Description = "Catálogo de Clasificación Autoridad Genérica con éxito")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "clasificacionautoridadgenerica")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            return await _processor.ExecuteAsync<CatalogoClasificacionAutoridadGenericaFiltro, List<CatalogoClasificacionAutoridadGenericaDTO>>(new CatalogoClasificacionAutoridadGenericaFiltro());
        }
    }
}
