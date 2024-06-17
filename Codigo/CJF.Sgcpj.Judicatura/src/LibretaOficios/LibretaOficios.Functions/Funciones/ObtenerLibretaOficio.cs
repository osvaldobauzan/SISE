using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.LibretaOficios.Application.Common.Models;
using Common.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace LibretaOficios.Functions.Funciones;
public class ObtenerLibretaOficioFunction
{
    private readonly IHttpRequestProcessor _processor;
    public ObtenerLibretaOficioFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }
    [FunctionName("ObtenerLibretaOficio")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "LibretaOficio" }, Summary = " Obtiene el detalle del tablero para la Libreta de Oficios")]
    [OpenApiParameter(name: "registrosPorPagina", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "")]
    [OpenApiParameter(name: "pagina", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "")]
    [OpenApiParameter(name: "fechaInicial", In = ParameterLocation.Query, Required = false, Type = typeof(DateTime), Description = "")]
    [OpenApiParameter(name: "fechaFinal", In = ParameterLocation.Query, Required = false, Type = typeof(DateTime), Description = "")]
    [OpenApiParameter(name: "asuntoNeunId", In = ParameterLocation.Query, Required = false, Type = typeof(long), Description = "")]
    [OpenApiParameter(name: "folio", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "")]
    [OpenApiParameter(name: "anio", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "")]

    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(LibretaOficio), Description = "Respuesta exitosa la Libreta de Oficios")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "Error interno en el servidor")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "libretaOficio/ObtenerLibretaOficio")] HttpRequest req,
        ILogger log)
    {

        log.LogInformation("C# HTTP trigger function processed a request. LibretaOficio");
        var consulta = new LibretaOficioFiltro
        {
            CantidadRegistros = Convert.ToInt32(req.Query["registrosPorPagina"]),
            NoRegistros = Convert.ToInt32(req.Query["pagina"]),
            Anio = (req.Query.ContainsKey("Anio")) ? Convert.ToInt32(req.Query["anio"]) : 0,
            AsuntoNeunId = (req.Query.ContainsKey("asuntoNeunId")) ? Convert.ToInt32(req.Query["asuntoNeunId"]) : 0,
            Folio = (req.Query.ContainsKey("folio")) ? Convert.ToInt32(req.Query["folio"]) : 0,
            FechaInicio = (req.Query.ContainsKey("fechaInicial")) ? Convert.ToDateTime(req.Query["fechaInicial"]) : null,
            FechaFin = (req.Query.ContainsKey("fechaFinal")) ? Convert.ToDateTime(req.Query["fechaFinal"]) : null
        };
        return await _processor.ExecuteAsync<LibretaOficioFiltro, List<LibretaOficio>>(consulta);
    }
}


