using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.EmpleadosProyecto.Consulta;
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

public class CatalogoEmpleadosProyectoFunction
{
    private readonly IHttpRequestProcessor _processor;

    public CatalogoEmpleadosProyectoFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }

    [FunctionName("CatalogoEmpleadosProyecto")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "CatalogoEmpleadosProyecto" }, Summary = "Obtiene el catalogo de empleados por organismo y tipo de empleado para el módulo de proyectos")]
    [OpenApiParameter(name: "tipoEmpleado", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Representa el identificador de tipo de empleado")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<CatalogoEmpleado>), Description = "Catálogo de empleados obtenido con éxito")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "proyecto/empleados")] HttpRequest req, ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");
        var consulta = new ObtieneCatalogoEmpleado
        {
            TipoEmpleado = Convert.ToInt32(req.Query["TipoEmpleado"])
        };
        return await _processor.ExecuteAsync<ObtieneCatalogoEmpleado, List<CatalogoEmpleadoDto>>(consulta);
    }
}
