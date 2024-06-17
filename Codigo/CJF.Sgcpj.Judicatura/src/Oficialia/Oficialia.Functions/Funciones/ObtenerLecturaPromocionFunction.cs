using System;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.CalcularRegistro;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerPromocion;
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
    public class ObtenerLecturaPromocionFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public ObtenerLecturaPromocionFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("Obtenerlecturapromocion")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Obtenerlecturapromocion" }, Summary = " Obtiene la promocion en edicion")]
        [OpenApiParameter(name: "AsuntoNeunId", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "")]
        [OpenApiParameter(name: "AsuntoID", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "")]
        [OpenApiParameter(name: "YearPromocion", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "")]
        [OpenApiParameter(name: "NumeroOrden", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "")]
        [OpenApiParameter(name: "CatIdOrganismo", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "")]
        [OpenApiParameter(name: "NumeroRegistro", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "")]
        [OpenApiParameter(name: "OrigenPromocion", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "")]
        [OpenApiParameter(name: "TipoModulo", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(RegistroDto), Description = "Respuesta exitosa se obtuvo la promocion para su edición")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "Error interno en el servidor")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "obtenerlecturapromocion")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"AsuntoNeunId: {req.Query["AsuntoNeunId"]} AsuntoID: {req.Query["AsuntoID"]} YearPromocion: {req.Query["YearPromocion"]} NumeroOrden: {req.Query["NumeroOrden"]} CatIdOrganismo: {req.Query["CatIdOrganismo"]} NumeroRegistro: {req.Query["NumeroRegistro"]} OrigenPromocion: {req.Query["OrigenPromocion"]} TipoModulo: {req.Query["TipoModulo"]}");

            var consulta = new ObtenerArchivosConsulta
            {
                AsuntoNeunId = Convert.ToInt32(req.Query["AsuntoNeunId"]),
                YearPromocion = Convert.ToInt32(req.Query["YearPromocion"]),
                NumeroOrden = Convert.ToInt32(req.Query["NumeroOrden"]),
                CatIdOrganismo = Convert.ToInt32(req.Query["CatIdOrganismo"]),
                NumeroRegistro = Convert.ToInt32(req.Query["NumeroRegistro"]),
                TipoModulo = Convert.ToInt32(req.Query["TipoModulo"])
            };
            return await _processor.ExecuteAsync<ObtenerArchivosConsulta, ArchivosPromocion>(consulta);
        }
    }
}
