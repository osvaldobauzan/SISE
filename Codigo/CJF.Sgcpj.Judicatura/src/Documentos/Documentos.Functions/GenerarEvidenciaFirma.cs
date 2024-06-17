using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using Common.Functions;
using Documentos.Application.Common.Model.GenerarEvidenciaFirma;
using Documentos.Application.FirmadorDocumentos.Comandos.GuardarArchivoFirmado;
using Documentos.Application.FirmadorDocumentos.Consulta.LecturaArchivoFirmador;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Documentos.Functions
{
    public class GenerarEvidenciaFirma
    {

        private readonly IHttpRequestProcessor _httpRequestProcessor;

        public GenerarEvidenciaFirma(IHttpRequestProcessor httpRequestProcessor)
        {
            _httpRequestProcessor = httpRequestProcessor;
        }

        [FunctionName("GenerarEvidenciaFirma")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "GenerarEvidenciaFirma" }, Summary = " Obtiene las hojas de firma para evidencia criptografica")]
        [OpenApiParameter(name: "p7m", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Base64 del la firma")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(GenerarEvidienciaFirmaDTO), Description = "Error interno en el servidor")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(GenerarEvidienciaFirmaDTO), Description = "Error interno en el servidor")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]

        public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "GenerarEvidenciaFirma")] HttpRequest req,
        ILogger log)
        {
            FunctionsAssemblyResolver.RedirectAssembly();
            log.LogInformation("C# HTTP trigger function processed a request. GenerarEvidenciaFirma");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            GenerarEvidienciaFirmaFiltro param = JsonConvert.DeserializeObject<GenerarEvidienciaFirmaFiltro>(requestBody);
            return await _httpRequestProcessor.ExecuteAsync<GenerarEvidienciaFirmaFiltro, GenerarEvidienciaFirmaDTO>(param);
        }
    }
}
