using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Seguridad.Application.Seguridad.Consultas.ObtieneOrganismosConsulta;
using Common.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Seguridad.Functions.Funciones
{
    public class ObtieneOrganismosFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public ObtieneOrganismosFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("OrganismosUsuarios")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "OrganismosUsuarios" }, Summary = " Obtiene la información de los organismos asignados a un identificador de usuario.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<OrganismoDto>), Description = "Catálogo de organismos asociados con un id de usuario generado con éxito")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(OrganismoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "organismos")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            return await _processor.ExecuteAsync<ObtieneOrganismosConsulta, List<OrganismoDto>>(new ObtieneOrganismosConsulta());
        }
    }
}
