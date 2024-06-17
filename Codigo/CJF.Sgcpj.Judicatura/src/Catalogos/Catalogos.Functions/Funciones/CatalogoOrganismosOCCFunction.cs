using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Common.Functions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Net;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.OrganismosOCC.Consulta;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Generico.Consulta;

namespace Seguridad.Functions.Funciones
{
    public class CatalogoOrganismosOCCFunction
    {
        private readonly IHttpRequestProcessor _processor;
        public CatalogoOrganismosOCCFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("ObtieneOrganismosCompletoFunction")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "OrganismosCompleto" }, Summary = " Obtiene la información de los organismos asignados a un identificador de usuario.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<OrganismoCompletoDto>), Description = "Catálogo de organismos asociados con un id de usuario generado con éxito")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(OrganismoCompletoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "organismosocc")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            return await _processor.ExecuteAsync<ObtieneOrganismosCompletoConsulta, List<OrganismoCompletoDto>>(new ObtieneOrganismosCompletoConsulta());
        }


        [FunctionName("ObtieneOrganosJurisdiccionalesFunction")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "OrganosJurisdiccionalesDTO" }, Summary = " Obtiene la información de los organismos jurisdiccionales.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<OrganismoCompletoDto>), Description = "Catálogo de organismos jurisdiccionales con éxito")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(OrganismoCompletoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run2(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "organosJurisdiccionales")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            return await _processor.ExecuteAsync<OrganosJurisdiccionalesFiltro, List<OrganosJurisdiccionalesDTO>>(new OrganosJurisdiccionalesFiltro());
        }
    }
}
