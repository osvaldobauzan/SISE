using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using Common.Functions;
using Documentos.Application.FirmadorDocumentos.Comandos.GuardarArchivoFirmado;
using Documentos.Application.FirmadorDocumentos.Consulta.LecturaArchivoFirmador;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Documentos.Functions
{
    public class DocumentosFunction
    {
        private readonly IHttpRequestProcessor _httpRequestProcessor;

        public DocumentosFunction(IHttpRequestProcessor httpRequestProcessor)
        {
            _httpRequestProcessor = httpRequestProcessor;
        }

        [FunctionName("ObtenerArchivo")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "ObtenerDocumento" }, Summary = " Obtiene el documento por GUID proporcionado")]
        [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(Guid), Description = "Identifiador del archivo deseado")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(RespuestaLecturaDto), Description = "Error interno en el servidor")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "Error interno en el servidor")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "archivo")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"Id: {req.Query["id"]}");
            log.LogInformation($"modulo: {req.Query["modulo"]}");

            var lecturaDocumentoQuery = new LecturaDocumentoQuery()
            {
                Id = (req.Query["id"]).ToString(),
                modulo = int.Parse(req.Query["modulo"])
            };

            return await _httpRequestProcessor.ExecuteAsync<LecturaDocumentoQuery, RespuestaLecturaDto>(lecturaDocumentoQuery);
        }

        [FunctionName("GuardarDocumento")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "GuardarDocumento" }, Summary = "Guarda el documento que se recibe en base64")]
        [OpenApiRequestBody(contentType: "json", bodyType: typeof(PeticionGuardadoDto), Description = "Objeto que se usa para transferir los archivos", Example = typeof(PeticionGuardadoDto))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(RespuestaGuardadoDto), Description = "Respuesta de éxito")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "Error interno en el servidor")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]

        public async Task<IActionResult> Run2(
         [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "archivo")] HttpRequest req,
         ILogger log)
        {
            FunctionsAssemblyResolver.RedirectAssembly();
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var data = JsonConvert.DeserializeObject<PeticionGuardadoDto>(requestBody);
            var guardarArchivoCommand = new GuardarArchivoFirmadoCommand()
            {
                PeticionGuardado = data
            };

            return await _httpRequestProcessor.ExecuteAsync<GuardarArchivoFirmadoCommand, RespuestaGuardadoDto>(guardarArchivoCommand);
        }
    }
}