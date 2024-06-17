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
    public class ObtenerComboTipoAsuntoFunction
    {
        private readonly IHttpRequestProcessor _processor;
        public ObtenerComboTipoAsuntoFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        /// <summary>
        /// Inserta el expediente
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("ObtenerComboTipoAsunto")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "obtenerComboTipoAsunto" }, Summary = " Obtiene el el catalogo de  tipo asunto para consulta de seguimiento")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Seguimientos.Seguimiento), Description = "Objeto que se usa para enviar la petición")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(Seguimientos.Seguimiento), Description = "Los datos ingresados no son correctos. Favor de verificar.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(Seguimientos.Seguimiento), Description = "Error interno en el servidor")]
        [OpenApiRequestBody(contentType: "json", bodyType: typeof(Seguimientos.Seguimiento), Description = "Objeto que se usa para guardar el seguimiento", Example = typeof(Seguimientos.Seguimiento))]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ObtenerComboTipoAsunto")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var seguimientoComando = new ObtenerComboTipoAsunto();
            seguimientoComando.seguimiento = new Seguimientos.Seguimiento();

            return await _processor.ExecuteAsync<ObtenerComboTipoAsunto, List<Seguimientos.Seguimiento>>(seguimientoComando);
        }
    }
}
