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
    public class ObtenerSeguimientoXAsuntoAliasTipoAsuntoFunction
    {
        private readonly IHttpRequestProcessor _processor;
        private readonly ISesionService _sesionService;
        public ObtenerSeguimientoXAsuntoAliasTipoAsuntoFunction(IHttpRequestProcessor processor, ISesionService sesion)
        {
            _processor = processor;
            _sesionService = sesion;
        }

        /// <summary>
        /// Inserta el expediente
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("ObtenerSeguimientosXaliasyAsunto")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "obtenerSeguimientosXaliasyAsunto" }, Summary = " Obtiene el seguimiento por  Expediente")]
        [OpenApiParameter(name: "Expediente", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Representa n�mero de expediente de la consulta")]
        [OpenApiParameter(name: "TipoAsunto", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Representa tipo asunto de la consulta")]
        [OpenApiParameter(name: "TipoProcedimiento", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Representa la tipo procedimiento de la consulta")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Seguimientos.Seguimiento), Description = "Objeto que se usa para enviar la petici�n")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(Seguimientos.Seguimiento), Description = "Los datos ingresados no son correctos. Favor de verificar.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(Seguimientos.Seguimiento), Description = "Error interno en el servidor")]
        [OpenApiRequestBody(contentType: "json", bodyType: typeof(Seguimientos.Seguimiento), Description = "Objeto que se usa para guardar el seguimiento", Example = typeof(Seguimientos.Seguimiento))]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ObtenerComboXAliasTipoAsunto")] HttpRequest req,
        ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string expediente = req.Query["Expediente"].ToString();
            string tipoAsunto = req.Query["TipoAsunto"].ToString();
            string tipoProcedimiento = req.Query["TipoProcedimiento"].ToString();

            log.LogInformation($"Expediente: {expediente} TipoAsunto: {tipoAsunto} TipoProcedimiento: {tipoProcedimiento}");

            var seguimientoComando = new ObtenerSeguimientoConsultaXAliasAsunto()
            {
                seguimiento = new Seguimientos.Seguimiento()
                {
                    Expediente = req.Query["Expediente"].ToString(),
                    TipoAsunto = req.Query["TipoAsunto"].ToString(),
                    TipoProcedimiento = req.Query["TipoProcedimiento"].ToString(),
                    CatOrganismoId = _sesionService.SesionActual.CatOrganismoId,
                    
                }
            };

                return await _processor.ExecuteAsync<ObtenerSeguimientoConsultaXAliasAsunto, IEnumerable<Seguimientos.Seguimiento>>(seguimientoComando);
            }
    }
}
