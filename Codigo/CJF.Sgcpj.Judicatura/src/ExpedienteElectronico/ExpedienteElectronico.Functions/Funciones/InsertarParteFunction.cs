using System;
using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.Common.Models;
using System.Net;
using Common.Functions;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExpedienteElectronico.Application.Common.Models;
using System.ComponentModel;
using System.IO;

namespace ExpedienteElectronico.Functions.Funciones
{
    public class InsertarParteFunction
    {
        private readonly IHttpRequestProcessor _processor;
        public InsertarParteFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }
        [FunctionName("InsertarParte")]
        [OpenApiRequestBody(contentType: "json", bodyType: typeof(PersonaAsuntoInsert), Description = "Objeto que se usa para insertar un oficio", Example = typeof(PersonaAsuntoInsert))]
        [OpenApiOperation(operationId: "Run", tags: new[] { "InsertarParte" }, Summary = "Inserta una parte")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Int64), Description = "Respuesta exitosa")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "Error interno en el servidor")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "InsertarParte")] HttpRequest req,
        ILogger log)
        {

            log.LogInformation("C# HTTP trigger function processed a request. InsertarParte");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            try
            {
                PersonaAsuntoInsert param = JsonConvert.DeserializeObject<PersonaAsuntoInsert>(requestBody);
                return await _processor.ExecuteAsync<PersonaAsuntoInsert, Int64>(param);
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }
    }
}
