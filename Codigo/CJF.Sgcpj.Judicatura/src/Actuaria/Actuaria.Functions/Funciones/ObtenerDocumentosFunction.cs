using System;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerDocumentos;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using Common.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Actuaria.Functions.Funciones;
public class ObtenerDocumentosFunction
{
    private readonly IHttpRequestProcessor _processor; 
    public ObtenerDocumentosFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }

    [FunctionName("ObtenerArchivos")]
    [OpenApiOperation(operationId: "Get", tags: new[] { "ObtenerArchivos" }, Summary = "Obtiene los nombres de los archivos y su path")]
    [OpenApiParameter(name: "asuntoNeunId", In = ParameterLocation.Query, Required = true, Type = typeof(long), Description = "asuntoneumID")]
    [OpenApiParameter(name: "numeroOrden", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Número de orden de la promoción")]
    [OpenApiParameter(name: "anioPromocion", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Año de la promoción")]
    [OpenApiParameter(name: "tipoModulo", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Tipo Modulo (oficialia, trámite)")]
    [OpenApiParameter(name: "origen", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "")]
    [OpenApiParameter(name: "asuntoDocumentoId", In = ParameterLocation.Query, Required = false, Type = typeof(long), Description = "Asunto DocumentoId")]
    [OpenApiParameter(name: "sintesisOrden", In = ParameterLocation.Query, Required = false, Type = typeof(long), Description = "Sintesis Orden")]
    [OpenApiParameter(name: "nombre", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Nombre del archivo")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(DocumentoBase64Dto), Description = "Respuesta exitosa, se obtiene la lista de archivos y anexos ")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Get(
       [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "actuaria/archivos")] HttpRequest req,
       ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");
        log.LogInformation($"AsuntoNeunId: {req.Query["AsuntoNeunId"]}");
        long aux;

        var consulta = new ObtenerDocumentoConsulta()
        {
            NombreArchivo = req.Query["nombre"],
            AsuntoNeunId = Convert.ToInt64(req.Query["asuntoNeunId"]),
            YearPromocion = Convert.ToInt32(req.Query["anioPromocion"]),
            NumeroOrden = Convert.ToInt32(req.Query["numeroOrden"]),
            TipoModulo = Convert.ToInt32(req.Query["tipoModulo"]),
            Origen = Convert.ToInt32(req.Query["origen"]),
            AsuntoDocumentoId = Convert.ToInt32(req.Query["asuntoDocumentoId"]),
            SintesisOrden = Convert.ToInt32(req.Query["sintesisOrden"])
        };
        return await _processor.ExecuteAsync<ObtenerDocumentoConsulta, DocumentoBase64Dto>(consulta);
    }
}
