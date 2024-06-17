using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerIndicadores;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerNumeroPromocion;
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

namespace Oficialia.Functions.Funciones
{
    public class ObtenerDetalleIndicadoresFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public ObtenerDetalleIndicadoresFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("Indicadores")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Indicadores" }, Summary = "Obtiene el detalle de los indicadores iniciales para oficialía")]
        [OpenApiParameter(name: "FechaInicial", In = ParameterLocation.Query, Required = true, Type = typeof(DateTime), Description = "Fecha de inicio rango de búsqueda")]
        [OpenApiParameter(name: "FechaFinal", In = ParameterLocation.Query, Required = true, Type = typeof(DateTime), Description = "Fecha de fin rango de búsqueda")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<DetalleIndicadoresDto>), Description = "Se obtuvo correctamente el numero de la promocion")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "promociones/indicadores")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"NumeroRegistro: {req.Query["numeroRegistro"]} YearPromocion: {req.Query["yearPromocion"]}");

            var consulta = new DetalleIndicadoresRequest
            {
                FechaInicioBusqueda = DateTime.ParseExact(req.Query["FechaInicial"], "dd/MM/yyyy", CultureInfo.InvariantCulture),
                FechaFinBusqueda = DateTime.ParseExact(req.Query["FechaFinal"], "dd/MM/yyyy", CultureInfo.InvariantCulture)
            };

            return await _processor.ExecuteAsync<DetalleIndicadoresRequest, List<DetalleIndicadoresDto>>(consulta);
        }
    }
}

