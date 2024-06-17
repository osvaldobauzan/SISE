using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerMotivosPartes;
using Common.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Proyectos.Functions.Funciones
{
    public class ObtenerDetallePartesFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public ObtenerDetallePartesFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        /// <summary>
        /// Función para obtener los detalles de las partes y sus motivos
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("ObtenerDetallePartesFunction")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "ObtenerDetallePartes" }, Summary = "Obtiene el detalle de los motivos por parte")]
        [OpenApiParameter(name: "proyectoId", In = ParameterLocation.Query, Required = true, Type = typeof(long), Description = "Identificador de proyecto")]

        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ListadoMotivosPartesDto), Description = "Respuesta exitosa, contiene el resultado en formato JSON.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "proyectos/motivosPartes")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var request = new ObtenerMotivosPartes
            {
                IdProyecto = long.Parse(req.Query["proyectoId"])
            };

            return await _processor.ExecuteAsync<ObtenerMotivosPartes, ListadoMotivosPartesDto>(request);
        }
    }
}
