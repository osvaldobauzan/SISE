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
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.DetalleNotificacionesFiltros;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;

namespace Actuaria.Functions.Funciones
{
    public class FiltrosFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public FiltrosFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("FiltrosFunction")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "ActuariaFiltros" }, Summary = " Obtiene los filtros para el tablero de actuaria")]
       
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(FiltroRootDto), Description = "Respuesta exitosa, contiene una lista de los diferentes filtros contenidos en dto 'FiltroRootDto' en formato JSON.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]

        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "actuaria/filtros")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            return await _processor.ExecuteAsync<FiltrosConsulta, FiltroRootDto>(new FiltrosConsulta());
        }

        [FunctionName("FiltroDetalleNotificaciones")]
        [OpenApiOperation(operationId: "Run2", tags: new[] { "DetalleNotificacionesFiltros" }, Summary = " Obtienen el detalle de los filtros del tablero de detalle notificaciones")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "Respuesta exitosa, contiene una lista de los diferentes filtros contenidos en dto para el tablero de detalle notificaciones")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]

        public async Task<IActionResult> Run2(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "actuaria/notificaciones/filtros")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            return await _processor.ExecuteAsync<DetalleNotificacionesFiltrosConsulta, 
                FiltroDetalleNotificaciones<FiltroTipoParteDto, FiltroTipoNotificacionDto, FiltroActuarioDto>>(new DetalleNotificacionesFiltrosConsulta());
        }
    }
}
