using System;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerNumeroPromocion;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtienePromocionesConFiltro;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Promociones.Consulta.ObtenerNumeroExpediente;
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
    public class ObtenerNumeroExpedienteFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public ObtenerNumeroExpedienteFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("ObtenerNumeroExpediente")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "ObtenerNumeroExpediente" }, Summary = "Obtiene el numero del expediente sugerido")]
        [OpenApiParameter(name: "IdTipoAsunto", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Id del catalogo de tipo asunto")]
        [OpenApiParameter(name: "IdTipoProcedimiento", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "Id del catalogo de procedimiento")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ObtieneNumeroPromocionDto), Description = "Se obtuvo correctamente el numero del expediente")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "expediente/numero")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var consulta = new ObtenerNumeroExpediente
            {
                TipoAsuntoId = Convert.ToInt32(req.Query["IdTipoAsunto"]),
                TipoProcedimientoId = Convert.ToInt32(req.Query["IdTipoProcedimiento"]),
            };
            return await _processor.ExecuteAsync<ObtenerNumeroExpediente, ExpedienteDto>(consulta);
        }
    }
}
