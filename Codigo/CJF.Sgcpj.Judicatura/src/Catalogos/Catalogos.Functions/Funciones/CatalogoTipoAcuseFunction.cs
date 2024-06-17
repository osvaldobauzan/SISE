using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.TipoAcuse.Consulta;
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
    public class CatalogoTipoAcuseFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public CatalogoTipoAcuseFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("CatalogoTipoAcuse")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "CatalogoTipoAcuse" }, Summary = " Obtiene el catalogo de tipo acuse para las promociones")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<>), Description = "Catálogo de tipo acuse para las promociones obtenido con éxito")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tipoAcuse")] HttpRequest req,
        ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            return await _processor.ExecuteAsync<ObtieneCatalogoTipoAcuse, List<CatalogoTipoAcuseDto>>(new ObtieneCatalogoTipoAcuse());
        }
    }
}
