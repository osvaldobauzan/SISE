using System;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Promociones.Consulta.ObtenerDetalleCargaMasiva;
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
    public  class ObtenerDetalleAlertaFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public ObtenerDetalleAlertaFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("ObtenerDetalleAlerta")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "ObtenerDetalleAlerta" }, Summary = "Obtiene el detalle de la alerta")]
        [OpenApiParameter(name: "YearPromocion", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Año de la promocion")]
        [OpenApiParameter(name: "NumeroRegistro", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Numero de registro de la promoción")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ObtenerDetalleAlertaDto), Description = "Se obtuvo correctamente el detalle de la alerta")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "alerta/detalle")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var consulta = new ObtenerDetalleCargaMasivaRequest
            {
                YearPromocion = Convert.ToInt32(req.Query["YearPromocion"]),
                NumeroRegistro = Convert.ToInt32(req.Query["NumeroRegistro"]),

            };
            return await _processor.ExecuteAsync<ObtenerDetalleCargaMasivaRequest, ObtenerDetalleAlertaDto>(consulta);
        }
    }
}
