using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.IndicadoresTipo;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using Common.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Actuaria.Functions.Funciones
{
    public class NotificacionesTipoYMesFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public NotificacionesTipoYMesFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("NotificacionesTipoYMesFunction")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Notificaciones" }, Summary = "Obtiene las notificaciones por tipo y mes")]
        [OpenApiParameter(name: "CatOrganismoId", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Identificador del organismo")]
        [OpenApiParameter(name: "FiltroActuarioId", In = ParameterLocation.Query, Required = true, Type = typeof(long), Description = "Identificador del actuario (puede ser 0 para no filtrar)")]
        [OpenApiParameter(name: "FechaInicial", In = ParameterLocation.Query, Required = true, Type = typeof(DateTime), Description = "Fecha inicial del rango de búsqueda")]
        [OpenApiParameter(name: "FechaFinal", In = ParameterLocation.Query, Required = true, Type = typeof(DateTime), Description = "Fecha final del rango de búsqueda")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(NotificacionesPorTipoYMesResponseDto), Description = "Se obtuvieron correctamente las notificaciones")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "actuaria/notificacionesTipoYMesFunction")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var consulta = new NotificacionesPorTipoYMesRequestDto
            {
                CatOrganismoId = Convert.ToInt32(req.Query["CatOrganismoId"]),
                FiltroActuarioId = Convert.ToInt64(req.Query["FiltroActuarioId"]),
                FechaInicial = DateTime.ParseExact(req.Query["FechaInicial"], "dd/MM/yyyy", CultureInfo.InvariantCulture),
                FechaFinal = DateTime.ParseExact(req.Query["FechaFinal"], "dd/MM/yyyy", CultureInfo.InvariantCulture)
            };

            return await _processor.ExecuteAsync<NotificacionesPorTipoYMesRequestDto, NotificacionesPorTipoYMesResponseDto>(consulta);
        }
    }
}
