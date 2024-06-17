using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Oficialia.Consultas.ObtenerPromociones;
using Common.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
namespace Oficialia.Functions.Funciones.OficialiaPartes
{

    public class TableroFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public TableroFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }
        /// <summary>
        /// Obtiene las promociones con los filtros especificados
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("TableroFunction")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "TableroOficialia" }, Summary = " Obtiene las promociones con los filtros especificados")]
        [OpenApiParameter(name: "fechaInicio", In = ParameterLocation.Query, Required = true, Type = typeof(DateTime), Description = "Fecha inicial de donde comieza el rango a buscar de las promociones")]
        [OpenApiParameter(name: "fechaFin", In = ParameterLocation.Query, Required = true, Type = typeof(DateTime), Description = "Fecha final  de donde se termina el rango a buscar de las promociones")]
        [OpenApiParameter(name: "usuarioId", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "Identificador de un empelado específico")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<OficialiaPartesDTO>), Description = "Respuesta exitosa, contiene una lista de objetos 'PromocionDto' en formato JSON.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "oficialia/tablero")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            DateTime fechaInicio = DateTime.Now;
            DateTime fechaFin = DateTime.Now;
            int idUsuario = 0;

            DateTime.TryParse(req.Query["fechaInicio"].ToString(), out fechaInicio);
            DateTime.TryParse(req.Query["fechaFin"].ToString(), out fechaFin);
            if (req.Query.Where(x => x.Key == "usuarioId").Count() > 0)
                int.TryParse(req.Query["usuarioId"].ToString(), out idUsuario);

            var consulta = new OficialiaPartesFiltro()
            {
                FechaInicio = fechaInicio,
                FechaFin = fechaFin,
                IdUsuario = idUsuario == 0 ? null : idUsuario,
            };

            return await _processor.ExecuteAsync<OficialiaPartesFiltro, List<OficialiaPartesDTO>>(consulta);
        }
    }
}
