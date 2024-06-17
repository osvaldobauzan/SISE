using System;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerPromocion;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtieneExpediente;
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

namespace Oficialia.Functions.Funciones
{
    public class ObtenerExpedienteFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public ObtenerExpedienteFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("ObtenerBase64")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "ObtenerBase64" }, Summary = " Obtiene los archivos en formato pdf-base64")]
        [OpenApiParameter(name: "asuntoNeunId", In = ParameterLocation.Query, Required = true, Type = typeof(long), Description = "asuntoNeunID")]
        [OpenApiParameter(name: "anioPromocion", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "año de la promoción")]
        [OpenApiParameter(name: "numeroOrden", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "número de orden")]
        [OpenApiParameter(name: "tipoModulo", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "tipo de módulo")]
        [OpenApiParameter(name: "origen", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "origen")]
        [OpenApiParameter(name: "nombre", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "nombre del archivo")]
        [OpenApiParameter(name: "kIdElectronica", In = ParameterLocation.Query, Required = false, Type = typeof(long), Description = "")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(DocumentoBase64Dto), Description = "Respuesta exitosa se obtiene los expediente en formato pdf-base64 ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "promociones/documento")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"ruta: {req.Query["ruta"]}");
            long aux;
            long? kIdElectronicaAux = long.TryParse(req.Query["kIdElectronica"].ToString(), out aux) ? Convert.ToInt64(req.Query["kIdElectronica"]) : null;

            var consulta = new ObtenerDocumentoConsulta()
            {
                AsuntoNeunId = long.Parse( req.Query["asuntoNeunId"]),
                 YearPromocion= int.Parse( req.Query["anioPromocion"]),
                NumeroOrden = int.Parse(req.Query["numeroOrden"]),
                TipoModulo = int.Parse(req.Query["tipoModulo"]),
                Origen = int.Parse(req.Query["origen"]),
                NombreArchivo = req.Query["nombre"],
                kIdElectronica = kIdElectronicaAux,
            };
            return await _processor.ExecuteAsync<ObtenerDocumentoConsulta, DocumentoBase64Dto>(consulta);
        }

        [FunctionName("ObtenerArchivos")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "ObtenerArchivos" }, Summary = "Obtiene los nombres de los archivos y su path")]
        [OpenApiParameter(name: "asuntoNeunId", In = ParameterLocation.Query, Required = true, Type = typeof(long), Description = "asuntoneumID")]
        [OpenApiParameter(name: "numeroOrden", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Número de orden de la promoción")]
        [OpenApiParameter(name: "anioPromocion", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Año de la promoción")]
        [OpenApiParameter(name: "tipoModulo", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Tipo Modulo (oficialia, trámite)")]
        [OpenApiParameter(name: "origen", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "")]
        [OpenApiParameter(name: "kIdElectronica", In = ParameterLocation.Query, Required = false, Type = typeof(long), Description = "")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ArchivosPromocion), Description = "Respuesta exitosa, se obtiene la lista de archivos y anexos ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run2(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "promociones/archivos")] HttpRequest req,
        ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"AsuntoNeunId: {req.Query["AsuntoNeunId"]}");
            long aux;
            long? kIdElectronicaAux = long.TryParse(req.Query["kIdElectronica"].ToString(), out aux) ? Convert.ToInt64(req.Query["kIdElectronica"]) : null;

            var consulta = new ObtenerArchivosConsulta()
            {
                AsuntoNeunId = Convert.ToInt64(req.Query["asuntoNeunId"]),
                YearPromocion = Convert.ToInt32(req.Query["anioPromocion"]),
                NumeroOrden = Convert.ToInt32(req.Query["numeroOrden"]),
                TipoModulo = Convert.ToInt32(req.Query["tipoModulo"]),
                Origen = Convert.ToInt32(req.Query["origen"]),
                kIdElectronica = kIdElectronicaAux,
            };
            return await _processor.ExecuteAsync<ObtenerArchivosConsulta, ArchivosPromocion>(consulta);
        }
    }
}
