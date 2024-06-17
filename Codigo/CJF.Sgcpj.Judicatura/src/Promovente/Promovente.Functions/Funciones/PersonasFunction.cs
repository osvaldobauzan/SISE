using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MediatR;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using System.Net;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Common.Functions;
using CJF.Sgcpj.Judicatura.Promovente.Application.Promoventes.Comandos.AgregarPersonas;

namespace Promovente.Functions.Funciones
{
    public  class PersonasFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public PersonasFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("GuardarPersonasAsuntos")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "GuardarPersonasAsuntos" }, Summary = "Guarda la parte asociada")]
        [OpenApiRequestBody(contentType: "json", bodyType: typeof(AgregarPersonasAsuntosDto), Description = "Objeto Persona Asociada",Example = typeof(AgregarPersonasAsuntosDto))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(long), Description = "Confirma el guardado exitoso de la Parte")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "personas")] HttpRequest req,
            ILogger log)
        {
                log.LogInformation("C# HTTP trigger function processed a request.");
                
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                AgregarPersonasAsuntosDto personaAsuntoDto = JsonConvert.DeserializeObject<AgregarPersonasAsuntosDto>(requestBody);
                AgregarPersonasAsuntosComando comando = new AgregarPersonasAsuntosComando();
                comando.PersonasAsuntos = personaAsuntoDto;
                return await _processor.ExecuteAsync<AgregarPersonasAsuntosComando, long>(comando);
          
        }
    }
}
