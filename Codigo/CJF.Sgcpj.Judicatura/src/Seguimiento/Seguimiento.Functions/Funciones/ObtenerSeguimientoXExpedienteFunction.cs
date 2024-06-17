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
    public class ObtenerSeguimientoXExpedienteFunction
    {
        private readonly IHttpRequestProcessor _processor;
        private readonly ISesionService _sesionService;
        public ObtenerSeguimientoXExpedienteFunction(IHttpRequestProcessor processor, ISesionService sesionService)
        {
            _processor = processor;
            _sesionService = sesionService;
        }

        /// <summary>
        /// Inserta el expediente
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("ObtenerSeguimientos")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "obtenerSeguimientos" }, Summary = " Obtiene el seguimiento por  Expediente")]
        [OpenApiParameter(name: "expediente", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Representa el identificador n�mero de expediente")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Seguimientos.Seguimiento), Description = "Objeto que se usa para enviar la petici�n")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(Seguimientos.Seguimiento), Description = "Los datos ingresados no son correctos. Favor de verificar.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(Seguimientos.Seguimiento), Description = "Error interno en el servidor")]
        [OpenApiRequestBody(contentType: "json", bodyType: typeof(Seguimientos.Seguimiento), Description = "Objeto que se usa para guardar el seguimiento", Example = typeof(Seguimientos.Seguimiento))]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ObtenerSeguimientoXExpediente")] HttpRequest req,
        ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var seguimiento = new Seguimientos.Seguimiento()
            {

                Expediente = req.Query["expediente"],
                CatOrganismoId = _sesionService.SesionActual.CatOrganismoId,
            };

            ObtenerSeguimientoConsultaXExpediente seguimientoComando = new ObtenerSeguimientoConsultaXExpediente();
            var Seguimiento = seguimiento;
            seguimientoComando.seguimiento = Seguimiento;

            log.LogInformation($"Valor de expediente en seguimientoComando: {seguimientoComando.seguimiento.Expediente}");

            return await _processor.ExecuteAsync<ObtenerSeguimientoConsultaXExpediente, List<Seguimientos.Seguimiento>>(seguimientoComando);
        }

    }
}
