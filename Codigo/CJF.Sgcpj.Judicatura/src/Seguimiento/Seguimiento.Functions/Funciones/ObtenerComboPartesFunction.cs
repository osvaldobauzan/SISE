using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Seguimientos.Consulta;
using Common.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Seguimientos = CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;

namespace Seguimiento.Functions.Funciones
{
    public class ObtenerComboPartesFunction
    {
        private readonly IHttpRequestProcessor _processor;
        private readonly ISesionService _sesionService;
        public ObtenerComboPartesFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        /// <summary>
        /// Inserta el expediente
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("ObtenerComboPartes")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "obtenerComboPartes" }, Summary = " Obtiene el el catalogo de  Partes para consulta de seguimiento")]
        [OpenApiParameter(name: "Expediente", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Representa el identificador número de expediente")]
        [OpenApiParameter(name: "TipoAsunto", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Representa el tipo asunto del tipo del expediente")]
        [OpenApiParameter(name: "TipoDocumento", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Representa el tipodocumento  del expediente")]
        [OpenApiParameter(name: "Fecha", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Representa la fecha de la búsqueda")]
        [OpenApiParameter(name: "TipoProcedimiento", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Representa el tipo procedimiento del expediente")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Seguimientos.Seguimiento), Description = "Objeto que se usa para enviar la petición")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(Seguimientos.Seguimiento), Description = "Los datos ingresados no son correctos. Favor de verificar.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(Seguimientos.Seguimiento), Description = "Error interno en el servidor")]
        [OpenApiRequestBody(contentType: "json", bodyType: typeof(Seguimientos.Seguimiento), Description = "Objeto que se usa para guardar el seguimiento", Example = typeof(Seguimientos.Seguimiento))]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ObtenerComboPartes")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"Expediente: {req.Query["Expediente"]} TipoAsunto {req.Query["TipoAsunto"]} TipoDocumento {req.Query["TipoDocumento"]} Fecha {req.Query["Fecha"]}");

            var seguimiento = new Seguimientos.Seguimiento()
            {
                Expediente = req.Query["Expediente"].ToString(),
                TipoAsunto = req.Query["TipoAsunto"].ToString(),
                TipoDocumento = req.Query["TipoDocumento"].ToString(),
                Fecha = req.Query["Fecha"].ToString(),
                TipoProcedimiento = req.Query["TipoProcedimiento"].ToString()
            };
            
            var seguimientoComando = new ObtenerComboPartes();
            seguimientoComando.seguimiento = seguimiento;

            return await _processor.ExecuteAsync<ObtenerComboPartes, List<Seguimientos.Seguimiento>>(seguimientoComando);
        }
    }
}
