using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.EditarSintesis;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.GuardarSintesis;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.DetalleSintesis;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using Common.Functions;
using iTextSharp.text;
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
    public class SintesisFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public SintesisFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }
        /// <summary>
        /// Guardar sintesis acuerdo
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("GuardarSintesis")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "GuardarSintesis" }, Summary = "Guarda la sintesis")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Respuesta de guardar sintesis")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiRequestBody(contentType: "json", bodyType: typeof(GuardarSintesisDto), Description = "Objeto que se usa para guardar la sintesis", Example = typeof(GuardarSintesisDto))]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous,"post", Route = "actuaria/sintesis")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            GuardarSintesisDto sintesis = JsonConvert.DeserializeObject<GuardarSintesisDto>(requestBody);
            GuardarSintesisComando sintesisGuardarComando = new GuardarSintesisComando();

            sintesisGuardarComando.SintesisAcuerdo = sintesis;

            return await _processor.ExecuteAsync<GuardarSintesisComando, bool>(sintesisGuardarComando);
        }
        /// <summary>
        /// Editar sintesis acuerdo
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("EditarSintesis")]
        [OpenApiOperation(operationId: "Run2", tags: new[] { "EditarSintesis" }, Summary = "Editar la sintesis")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Respuesta de editar sintesis")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiRequestBody(contentType: "json", bodyType: typeof(GuardarSintesisDto), Description = "Objeto que se usa para guardar la sintesis", Example = typeof(GuardarSintesisDto))]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run2(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "actuaria/sintesis")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            EditarSintesisDto sintesis = JsonConvert.DeserializeObject<EditarSintesisDto>(requestBody);
            EditarSintesisComando sintesisEditarComando = new EditarSintesisComando();

            sintesisEditarComando.SintesisAcuerdo = sintesis;
            

            return await _processor.ExecuteAsync<EditarSintesisComando, bool>(sintesisEditarComando);
        }

        /// <summary>
        /// Obtiene el detalle de la sintesis acuerdo
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("ObtenerSintesis")]
        [OpenApiOperation(operationId: "Get", tags: new[] { "ObtenerSintesis" }, Summary = "Obtiene la sintesis")]
        [OpenApiParameter(name: "asuntoNeunId", In = ParameterLocation.Query, Required = true, Type = typeof(long), Description = "asuntoneumID")]
        [OpenApiParameter(name: "sintesisOrden", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "Sintesis Orden")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Respuesta de obtener sintesis")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Get(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "actuaria/sintesis")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            FiltroDetalleSintesis filtro = new FiltroDetalleSintesis()
            {
                AsuntoNeunId = Convert.ToInt64(req.Query["asuntoNeunId"]),
                SintesisOrden = Convert.ToInt32(req.Query["sintesisOrden"])
            };         

            return await _processor.ExecuteAsync<FiltroDetalleSintesis, List<DetalleSintesisDTO>>(filtro);
        }
    }
}
