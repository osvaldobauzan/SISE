using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerIndicadores;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerNumeroPromocion;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerTiempTurnos;
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
    public class ObtenerGruposMesesFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public ObtenerGruposMesesFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("GruposMeses")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "GruposMeses" }, Summary = "Obtiene el detalle de los indicadores iniciales para oficialía")]
        [OpenApiParameter(name: "EmpleadoId", In = ParameterLocation.Query, Required = true, Type = typeof(DateTime), Description = "Identificador del empleado, oficial")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<DetalleGruposMesDto>), Description = "Se obtuvo correctamente el numero de la promocion")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "promociones/gruposmeses")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var consulta = new DetalleGruposMesRequest
            {
                EmpleadoId = Convert.ToInt32(req.Query["EmpleadoId"])
            };

            return await _processor.ExecuteAsync<DetalleGruposMesRequest, List<DetalleGruposMesDto>>(consulta);
        }
    }
}

