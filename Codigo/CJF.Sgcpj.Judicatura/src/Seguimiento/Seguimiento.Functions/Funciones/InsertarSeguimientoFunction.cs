using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Common.Functions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System.Net;
using Seguimientos=CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Seguimientos.InsertarSeguimiento.Comando;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;

namespace Seguimiento.Functions.Funciones
{
    public class InsertarSeguimientoFunction
    {
        private readonly IHttpRequestProcessor _processor;
        private readonly ISesionService _sesionService;
        public InsertarSeguimientoFunction(IHttpRequestProcessor processor, ISesionService sesionService)
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
        [FunctionName("InsertarSeguimiento")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "InsertarSeguimiento" }, Summary = " Inserta un seguimiento")]
        [OpenApiParameter(name: "QrString", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Representa el Json leido  de un QR correspondiente a un  expediente")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(int), Description = "1")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(int), Description = "0")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiRequestBody(contentType: "json", bodyType: typeof(Seguimientos.Seguimiento), Description = "Objeto que se usa para guardar el seguimiento", Example = typeof(Seguimientos.Seguimiento))]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "InsertarSeguimiento")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var seguimiento = new Seguimientos.Seguimiento()
            {
                QrString = JsonConvert.DeserializeObject<Seguimientos.Seguimiento>(requestBody)?.QrString.ToString(),
            };

            InsertarSeguimientoConFiltroHandler seguimientoComando = new InsertarSeguimientoConFiltroHandler();
            seguimientoComando.seguimiento = seguimiento;


            return await _processor.ExecuteAsync<InsertarSeguimientoConFiltroHandler, int>(seguimientoComando);
        }
    }
}
