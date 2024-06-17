using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Generico.Consulta;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.TipoSentenciaProyecto.Consulta;
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

namespace Catalogos.Functions.Funciones;

public class CatalogoTipoSentenciaFunction
{
    private readonly IHttpRequestProcessor _processor;

    public CatalogoTipoSentenciaFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }

    [FunctionName("CatalogoTipoSentenciaFunction")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "CatalogoTipoSentencia" }, Summary = "Obtiene el catalogo de tipo de sentencia para el módulo de proyectos")]
    [OpenApiParameter(name: "catTipoAsuntoId", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Representa el identificador de tipo de asunto")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<CatalogoEmpleado>), Description = "Catálogo de tipos de sentencia obtenido con éxito")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "proyecto/tipoSentencia")] HttpRequest req, ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        var consulta = new ObtieneCatalogoTipoSentencia
        {
            CatTipoAsuntoId = Convert.ToInt32(req.Query["CatTipoAsuntoId"]),
            TipoCatalogo = 1
        };

        return await _processor.ExecuteAsync<ObtieneCatalogoTipoSentencia, List<CatalogoGenericoDTO>>(consulta);
    }
}
