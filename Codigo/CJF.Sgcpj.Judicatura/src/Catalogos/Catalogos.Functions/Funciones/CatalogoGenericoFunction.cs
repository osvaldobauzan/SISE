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
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Net;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Generico.Consulta;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;

namespace Catalogos.Functions.Funciones
{
    public class CatalogoGenericoFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public CatalogoGenericoFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("CatalogoGenerico")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "CatalogoGenerico" }, Summary = "Obtiene el catálogo dado un identificador de catálogo")]
        [OpenApiParameter(name: "catalogoId", In = ParameterLocation.Query, Required = true, Type = typeof(int?), Description = "Representa el identificador del catálogo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<CatalogoGenericoDTO>), Description = "Catálogo con éxito")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "generico")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"{req.Query["catalogoId"]}");

            CatalogoGenericoFiltro filtro = new CatalogoGenericoFiltro()
            {
                CatalogoId = Convert.ToInt32(req.Query["catalogoId"])
            };
            
            return await _processor.ExecuteAsync<CatalogoGenericoFiltro, List<CatalogoGenericoDTO>>(filtro);
        }
    }
}
