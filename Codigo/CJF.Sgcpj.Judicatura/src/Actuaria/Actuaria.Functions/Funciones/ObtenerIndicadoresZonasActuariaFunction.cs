using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.IndicadoresActuaria;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerPersonaAsunto;
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
    public class ObtenerIndicadoresZonasActuariaFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public ObtenerIndicadoresZonasActuariaFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("ObtenerIndicadoresZonasActuaria")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "IndicadoresZonas" }, Summary = "Obtiene los totales de los actuarios relacionados a zonas")]
        [OpenApiParameter(name: "CatOrganismoId", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Identificador del organismo")]
        [OpenApiParameter(name: "FechaInicial", In = ParameterLocation.Query, Required = true, Type = typeof(DateTime), Description = "Fecha inicial del rango de búsqueda")]
        [OpenApiParameter(name: "FechaFinal", In = ParameterLocation.Query, Required = true, Type = typeof(DateTime), Description = "Fecha final del rango de búsqueda")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ObtenerNotificacionesResponseDto), Description = "Se obtuvieron correctamente los indicadores")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "actuaria/indicadoresZonas")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var consulta = new ObtenerNotificacionesZonasRequestDto
            {
                CatOrganismoId = Convert.ToInt32(req.Query["CatOrganismoId"]),
                FechaInicial = DateTime.ParseExact(req.Query["FechaInicial"], "dd/MM/yyyy", CultureInfo.InvariantCulture),
                FechaFinal = DateTime.ParseExact(req.Query["FechaFinal"], "dd/MM/yyyy", CultureInfo.InvariantCulture)
            };

            return await _processor.ExecuteAsync<ObtenerNotificacionesZonasRequestDto, ObtenerNotificacionesZonasResponseDto>(consulta);
        }
    }
}
