using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Audiencia.Consulta;
using Common.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Azure.WebJobs;
using Microsoft.OpenApi.Models;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.ResultadoAudiencia.Consulta;
using Microsoft.Extensions.Logging;

namespace Catalogos.Functions.Funciones;
public class CatalogoResultadoAudienciaFunction
{
    private readonly IHttpRequestProcessor _processor;
    public CatalogoResultadoAudienciaFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }

    [FunctionName("CatalogoResultadoPorAudiencia")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "CatalogoResultadoAudiencia" }, Summary = " Obtiene el catalogo de resultados por audiencia")]
    [OpenApiParameter(name: "IdTipoAudiencia", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "Representa el identificador del tipo de audiencia")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<CatalogoResultadoDto>), Description = "Catálogo de resultados por audiencias obtenido con éxito")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "resultadoAudiencia")] HttpRequest req,
    ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        var consulta = new CatalogoResultadoRequest
        {
            IdTipoAudiencia = Convert.ToInt32(req.Query["IdTipoAudiencia"])
        };

        return await _processor.ExecuteAsync<CatalogoResultadoRequest, List<CatalogoResultadoDto>>(consulta);
    }

}
