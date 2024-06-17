using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerDetalleAcuerdo;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
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
using Newtonsoft.Json;

namespace Actuaria.Functions.Funciones
{
    public class DetalleAcuerdoFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public DetalleAcuerdoFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("DetalleAcuerdo")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "DetalleAcuerdo" }, Summary = " Obtiene el detalle del acuerdo")]
        [OpenApiParameter(name: "asuntoNeunId", In = ParameterLocation.Query, Required = true, Type = typeof(long), Description = "Recibe el identificador del Asunto")]
        [OpenApiParameter(name: "sintesisOrden", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Recibe la sintesis orden")]
        [OpenApiParameter(name: "asuntoDocumentoId", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Recibe el identificador AsuntoDocumento")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "Respuesta exitosa, contiene una lista de objetos 'DetalleAcuerdoDto' en formato JSON.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "actuaria/acuerdodetalle")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"{req.Query["asuntoNeunId"]} {req.Query["sintesisOrden"]} {req.Query["asuntoDocumentoId"]}");

            var consulta = new ObtieneDetalleAcuerdoConsulta()
            {
                AsuntoNeunId = Convert.ToInt64(req.Query["asuntoNeunId"]),
                SintesisOrden = Convert.ToInt32(req.Query["sintesisOrden"]),
                AsuntoDocumentoId = Convert.ToInt32(req.Query["asuntoDocumentoId"])
            };

            return await _processor.ExecuteAsync<ObtieneDetalleAcuerdoConsulta, ListaDetalleAcuerdo<DetalleAcuerdoDto, PromocionDto>>(consulta);
        }
    }
}

