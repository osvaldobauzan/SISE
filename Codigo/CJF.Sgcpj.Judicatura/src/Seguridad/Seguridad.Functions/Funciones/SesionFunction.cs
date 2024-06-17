using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Seguridad.Application.Seguridad.Comandos.CerrarSesion;
using CJF.Sgcpj.Judicatura.Seguridad.Application.Seguridad.Comandos.CrearSesion;
using CJF.Sgcpj.Judicatura.Seguridad.Application.Seguridad.Comandos.RefrescarSesion;
using Common.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Seguridad.Functions.Funciones
{

    public class SesionFunction
    {
        private readonly IHttpRequestProcessor _httpRequestProcessor;

        public SesionFunction(IHttpRequestProcessor httpRequestProcessor)
        {
            _httpRequestProcessor = httpRequestProcessor;
        }
        [FunctionName("CrearSesion")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "CrearSesion" }, Summary = " Obtiene la información de una sesión, si la cuenta de usuario existe por identificador de empleado y el identificador del organismo")]
        [OpenApiRequestBody(contentType: "json", bodyType: typeof(LlaveSesionDto), Description = "Objeto de parametros de sesión.", Example = typeof(LlaveSesionDto))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<SesionDto>), Description = "Inicio de sesión asociado con un id de usuario generado con éxito")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(SesionDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "sesion")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            LlaveSesionDto llaveSesion = JsonConvert.DeserializeObject<LlaveSesionDto>(requestBody);
            CrearSesionComando sesionConsulta = new CrearSesionComando();
            sesionConsulta.OrganismoId = llaveSesion.IdCatOrganismo;
            sesionConsulta.RefreshToken = llaveSesion.RefreshToken;

            return await _httpRequestProcessor.ExecuteAsync<CrearSesionComando, SesionDto>(sesionConsulta);
        }

        [FunctionName("CerrarSesion")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "CerrarSesion" }, Summary = " Cerrar la sesion")]
        [OpenApiRequestBody("application/json", typeof(LlaveSesionDto))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Delete(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "sesion")] HttpRequest req,
        ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            LlaveSesionDto llaveSesion = JsonConvert.DeserializeObject<LlaveSesionDto>(requestBody);
            CerrarSesionComando cerrarSesionComando = new CerrarSesionComando();
            cerrarSesionComando.RefreshToken = llaveSesion.RefreshToken;;
            return await _httpRequestProcessor.ExecuteAsync<CerrarSesionComando, bool>(cerrarSesionComando);
        }

        [FunctionName("RefrescarSesion")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "RefrescarSesion" }, Summary = " Refresca la sesion")]
        [OpenApiRequestBody("application/json", typeof(LlaveSesionDto))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Put(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "sesion")] HttpRequest req,
        ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            LlaveSesionDto llaveSesion = JsonConvert.DeserializeObject<LlaveSesionDto>(requestBody);
            RefrescarSesionComando sesionConsulta = new RefrescarSesionComando();
            sesionConsulta.RefreshToken = llaveSesion.RefreshToken;
            return await _httpRequestProcessor.ExecuteAsync<RefrescarSesionComando, bool>(sesionConsulta);
        }
    }
}
