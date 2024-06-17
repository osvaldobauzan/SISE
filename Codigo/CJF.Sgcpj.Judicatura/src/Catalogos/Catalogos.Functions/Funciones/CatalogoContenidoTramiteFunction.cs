using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Common.Functions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Net;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Anexo.Consulta;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.ContenidoTramite.Consulta;

namespace Catalogos.Functions.Funciones
{
    public class CatalogoContenidoTramiteFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public CatalogoContenidoTramiteFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }
        [FunctionName("CatalogoContenidoTramite")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "CatalogoContenidoTramite" }, Summary = " Obtiene el catalogo de contenido para tramite")]     
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "contenidoTramite")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"{req.Query["catTipoCatalogoAsuntoId"]} {req.Query["catTipoAsuntoId"]}");
            var consulta = new ObtieneCatalogoContenidoTramite
            {
                CatTipoCatalogoAsuntoId = Convert.ToInt32(req.Query["catTipoCatalogoAsuntoId"]),
                CatTipoAsuntoId = Convert.ToInt32(req.Query["catTipoAsuntoId"])
            };
            return await _processor.ExecuteAsync<ObtieneCatalogoContenidoTramite, List<ContenidoTramiteDto>>(consulta);
        }
    }
}
