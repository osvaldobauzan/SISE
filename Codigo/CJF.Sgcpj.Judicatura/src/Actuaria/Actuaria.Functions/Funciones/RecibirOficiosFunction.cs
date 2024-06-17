using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Common.Functions;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.Filtros;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System.Net;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.RecibirOficios;
using System.Collections.Generic;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.RecibirOficios;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.GuardarSintesis;
using Newtonsoft.Json;
using System.IO;
using System;
using Documentos.Application.FirmadorDocumentos.Consulta.LecturaArchivoFirmador;

namespace Actuaria.Functions.Funciones
{
    public class RecibirOficiosFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public RecibirOficiosFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }
        [FunctionName("RecibirOficiosConsultaFunction")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "RecibirOficiosConsulta" }, Summary = " Obtiene el oficio relacionado al folio")]
        [OpenApiParameter(name: "folio", In = ParameterLocation.Query, Required = true, Type = typeof(int?), Description = "folio del oficio")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<RecibirOficiosDto>), Description = "Respuesta exitosa, contiene una lista de oficios que corresponden al folio")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]

        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "actuaria/oficios")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var consulta = new RecibirOficiosConsulta();
            consulta.Folio = req.Query["folio"];
            return await _processor.ExecuteAsync<RecibirOficiosConsulta, List<RecibirOficiosDto>>(consulta);
        }

        [FunctionName("RecibirOficiosFunction")]
        [OpenApiOperation(operationId: "Post", tags: new[] { "RecibirOficios" }, Summary = "Recibe los oficios")]
        [OpenApiRequestBody(contentType: "json", bodyType: typeof(List<RecibirOficiosDto>), Description = "Lista de oficios a recibir", Example = typeof(GuardarSintesisDto))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<RecibirOficiosDto>), Description = "Respuesta exitosa, contiene una lista de oficios que corresponden al folio")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]

        public async Task<IActionResult> Post(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "actuaria/oficios")] HttpRequest req,
        ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var comando = new RecibirOficiosComando();
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    
            comando.Oficios = JsonConvert.DeserializeObject<List<RecibirOficiosDto>>(requestBody);
            return await _processor.ExecuteAsync<RecibirOficiosComando, List<RecibirOficiosDto>>(comando);
        }

        [FunctionName("ObtenerBase64")]
        [OpenApiOperation(operationId: "Get", tags: new[] { "ObtenerBase64" }, Summary = " Obtiene los archivos en formato word-base64")]
        [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Identifiador del archivo deseado")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(DocumentoBase64Dto), Description = "Respuesta exitosa se obtiene los expediente en formato word-base64 ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]

        public async Task<IActionResult> Get(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "actuaria/documento")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"ruta: {req.Query["id"]}");
            var consulta = new ObtenerDocumentoConsulta()
            {
                Id = req.Query["id"],
                tipoArchivo = "oficio"
            };
            return await _processor.ExecuteAsync<ObtenerDocumentoConsulta, DocumentoBase64Dto>(consulta);
        }
    }
}
