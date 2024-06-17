using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerUltimaVersionProyecto;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerVersionesProyecto;
using Common.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Proyectos.Application.Common.Models;

namespace Proyectos.Functions.Funciones
{
    public class ObtenerVersionesProyectoFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public ObtenerVersionesProyectoFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        /// <summary>
        /// Función para validar un expediente
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("ObtenerVersiones")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "ObtenerVersiones" }, Summary = "Obtiene las versiones de un proyecto")]
        [OpenApiParameter(name: "asuntoNeunId", In = ParameterLocation.Query, Required = true, Type = typeof(long), Description = "Neun")]

        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ListadoVersionesDto), Description = "Respuesta exitosa, contiene el resultado de la validación en formato JSON.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]

        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "proyecto/versiones")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            long asuntoNeunId = long.Parse(req.Query["asuntoNeunId"]);


            var validarExpedienteConsulta = new ObtenerListadoVersiones()
            {
                AsuntoNeunId = asuntoNeunId,
            };

            return await _processor.ExecuteAsync<ObtenerListadoVersiones, ListadoVersionesDto>(validarExpedienteConsulta);
        }

        /// <summary>
        /// Función para obtener última versión
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("ObtenerUltimaVersion")]
        [OpenApiOperation(operationId: "Run2", tags: new[] { "ObtenerUltimaVersion" }, Summary = "Obtiene la última versión de un proyecto")]
        [OpenApiParameter(name: "asuntoNeunId", In = ParameterLocation.Query, Required = true, Type = typeof(long), Description = "Neun")]

        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(VersionDto), Description = "Respuesta exitosa, contiene el resultado de la validación en formato JSON.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]

        public async Task<IActionResult> Run2([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "proyecto/ultimaVersion")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            long asuntoNeunId = long.Parse(req.Query["asuntoNeunId"]);


            var consulta = new ObtenerVersion()
            {
                AsuntoNeunId = asuntoNeunId,
            };

            return await _processor.ExecuteAsync<ObtenerVersion, VersionDto>(consulta);
        }
    }
}
