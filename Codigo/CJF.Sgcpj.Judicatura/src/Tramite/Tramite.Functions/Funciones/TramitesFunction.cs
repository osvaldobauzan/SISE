using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Comandos.EliminarAcuerdoComando;
using CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerTramitesConFiltro;
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

namespace Tramites.Functions.Funciones
{
    public class TramitesFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public TramitesFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        /// <summary>
        /// Obtiene los trámites con los filtros especificados
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("Tramites")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Tramites" }, Summary = " Obtiene los trámites con los filtros especificados")]
        [OpenApiParameter(name: "fechaInicial", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Fecha inicial de donde comieza el rango a buscar de las promociones, el fortmato es dd/MM/YYYY")]
        [OpenApiParameter(name: "fechaFinal", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Fecha final  de donde se termina el rango a buscar de las promociones, el fortmato es dd/MM/YYYY")]
        [OpenApiParameter(name: "estado", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "Estado de las promocociones (0 - indica todos 1-Sin captura)")]
        [OpenApiParameter(name: "texto", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "Texto que se usa para filtrar las promociones")]
        [OpenApiParameter(name: "ordenarPor", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Campo por el que se ordena ")]
        [OpenApiParameter(name: "descendente", In = ParameterLocation.Query, Required = false, Type = typeof(bool), Description = "Indica si el ordenamiento es ascendente o descendente ")]
        [OpenApiParameter(name: "pagina", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Número de página")]
        [OpenApiParameter(name: "registrosPorPagina", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Número de registros por página")]
        [OpenApiParameter(name: "secretario", In = ParameterLocation.Query, Required = false, Type = typeof(long?), Description = "ID del Secretario")]
        [OpenApiParameter(name: "origen", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "Origen")]
        [OpenApiParameter(name: "asunto", In = ParameterLocation.Query, Required = false, Type = typeof(int?), Description = "ID del Catalogo Asunto)")]
        [OpenApiParameter(name: "capturo", In = ParameterLocation.Query, Required = false, Type = typeof(long?), Description = "ID de Capturo")]
        [OpenApiParameter(name: "preautorizo", In = ParameterLocation.Query, Required = false, Type = typeof(long?), Description = "ID de Preautorizo")]
        [OpenApiParameter(name: "autorizo", In = ParameterLocation.Query, Required = false, Type = typeof(long?), Description = "ID de Autorizo")]
        [OpenApiParameter(name: "cancelo", In = ParameterLocation.Query, Required = false, Type = typeof(long?), Description = "ID de Cancelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<TramiteDto>), Description = "Respuesta exitosa, contiene una lista de objetos 'PromocionDto' en formato JSON.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tramites")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"Fecha inicial: {req.Query["fechaInicial"]} Fecha final: {req.Query["fechaFinal"]} estado:{req.Query["estado"]} texto: {req.Query["texto"]} origen: {req.Query["origen"]} ");

            var aux = 0;
            var auxl = 0l;
            int? asuntoId = int.TryParse(req.Query["asunto"].ToString(), out aux) ? Convert.ToInt32(req.Query["asunto"]) : null;
            long? secretarioId = long.TryParse(req.Query["secretario"].ToString(), out auxl) ? Convert.ToInt64(req.Query["secretario"]) : (long?)null;
            long? capturoId = long.TryParse(req.Query["capturo"].ToString(), out auxl) ? Convert.ToInt64(req.Query["capturo"]) : (long?)null;
            long? preautorizoId = long.TryParse(req.Query["preautorizo"].ToString(), out auxl) ? Convert.ToInt64(req.Query["preautorizo"]) : (long?)null;
            long? autorizoId = long.TryParse(req.Query["autorizo"].ToString(), out auxl) ? Convert.ToInt64(req.Query["autorizo"]) : (long?)null;
            long? canceloId = long.TryParse(req.Query["cancelo"].ToString(), out auxl) ? Convert.ToInt64(req.Query["cancelo"]) : (long?)null;
            var consulta = new ObtieneTramitesConFiltroConsulta()
            {
                FechaInicial = req.Query["fechaInicial"],
                FechaFinal = req.Query["fechaFinal"],
                Estado = Convert.ToInt32(req.Query["estado"]),
                Texto = req.Query["texto"],
                OrdenarPor = req.Query["ordenarPor"],
                Descendente = Convert.ToBoolean(req.Query["descendente"]),
                Pagina = Convert.ToInt32(req.Query["pagina"]),
                RegistrosPorPagina = Convert.ToInt32(req.Query["registrosPorPagina"]),
                SecretarioId = secretarioId,
                Origen = req.Query["origen"],
                CatTipoAsuntoId = asuntoId,
                CapturoId = capturoId,
                PreautorizoId = preautorizoId,
                AutorizoId = autorizoId,
                CanceloId = canceloId
            };

            return await _processor.ExecuteAsync<ObtieneTramitesConFiltroConsulta, 
                                                 ListaPaginada<TramiteDto, MetaDataEstadosTramiteDto>>
                                                 (consulta);
        }

        /// <summary>
        /// Obtiene las promociones con los filtros especificados
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("EliminarAcuerdo")]
        [OpenApiOperation(operationId: "Run2", tags: new[] { "EliminarAcuerdo" }, Summary = " Elimina un acuerdo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.NoContent, contentType: "application/json", bodyType: typeof(bool), Description = "Indica TRUE si se pudo eliminar el acuerdo; en caso contrario, indica FALSE")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiRequestBody(contentType: "json", bodyType: typeof(EliminarAcuerdo), Description = "Objeto que se usa para eliminar una promocion", Example = typeof(EliminarAcuerdoDto))]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run2(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "tramites")]
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            EliminarAcuerdoDto acuerdo = JsonConvert.DeserializeObject<EliminarAcuerdoDto>(requestBody);
            EliminarAcuerdoComando acuerdoComando = new EliminarAcuerdoComando();
          
            acuerdoComando.Acuerdo = acuerdo;

            return await _processor.ExecuteAsync<EliminarAcuerdoComando, bool>(acuerdoComando);
        }
    }
}
