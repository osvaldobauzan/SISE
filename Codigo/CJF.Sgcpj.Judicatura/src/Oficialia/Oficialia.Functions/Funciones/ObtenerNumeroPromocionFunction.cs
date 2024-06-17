using System;
using System.Net;
using System.Threading.Tasks;
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
    public class ObtenerNumeroPromocionFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public ObtenerNumeroPromocionFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("Estatus")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Estatus" }, Summary = " Obtiene 1 si existe el numero de registro en caso contrario 0")]
        [OpenApiParameter(name: "NumeroRegistro", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Representa el numero de registro")]
        [OpenApiParameter(name: "YearPromocion", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Representa año de la promoción")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ObtieneNumeroPromocionDto), Description = "Se obtuvo correctamente el numero de la promocion")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "promociones/estatus")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"NumeroRegistro: {req.Query["numeroRegistro"]} YearPromocion: {req.Query["yearPromocion"]}");

            var consulta = new ObtieneNumeroPromocion
            {
                NumeroRegistro = Convert.ToInt32(req.Query["numeroRegistro"]),
                YearPromocion = Convert.ToInt32(req.Query["yearPromocion"])
            };

            return await _processor.ExecuteAsync<ObtieneNumeroPromocion, ObtieneNumeroPromocionDto>(consulta);
        }
    }
}

