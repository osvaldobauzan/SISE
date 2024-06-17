using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Net;
using Common.Functions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.TipoPersona.Consulta;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;

namespace Catalogos.Functions.Funciones
{
    public class CatalogoTipoPersonaFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public CatalogoTipoPersonaFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("TipoPersona")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "CatalogoTipoPersona" }, Summary = " Obtiene los cuadernos para las promociones")]
        [OpenApiParameter(name: "TipoAsuntoId", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "El id del tipoasunto")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<CatalogoTipoPersonaDto>), Description = "Catálogo de tipo persona obtenido con éxito")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tipopersona")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var consulta = new ObtieneCatalogoTipoPersona
            {
                CatTipoAsuntoId = Convert.ToInt32(req.Query["TipoAsuntoId"])
            };

            return await _processor.ExecuteAsync<ObtieneCatalogoTipoPersona, List<CatalogoTipoPersonaDto>>(consulta); 
        }
    }
}
