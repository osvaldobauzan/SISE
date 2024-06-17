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
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.TipoPersonaCaracter.Consulta;

namespace Catalogos.Functions.Funciones
{
    public class CatalogoTipoPersonaCaracterFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public CatalogoTipoPersonaCaracterFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("CatalogoTipoPersonaCaracter")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "CatalogoTipoPersonaCaracter" }, Summary = " Obtiene el catalogo del caracter para la persona.")]
        [OpenApiParameter(name: "TipoAsuntoId", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "El id del tipoasunto")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<CatalogoTipoPersonaCaracterDto>), Description = "Catálogo de cuaderno obtenido con éxito")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "personacaracter")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"Tipo asunto: {req.Query["TipoAsuntoId"]}");
            var consulta = new ObtieneCatalogoTipoPersonaCaracter
            {
                TipoAsuntoId = Convert.ToInt32(req.Query["tipoAsuntoId"])
            };
               
            return await _processor.ExecuteAsync<ObtieneCatalogoTipoPersonaCaracter, List<CatalogoTipoPersonaCaracterDto>>(consulta);
        }
    }
}
