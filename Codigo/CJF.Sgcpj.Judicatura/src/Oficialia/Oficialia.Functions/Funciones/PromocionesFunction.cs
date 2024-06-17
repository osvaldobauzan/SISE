using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Application.Promociones.Comandos;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerPromocionesConFiltro;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtienePromocionesConFiltro;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Promociones.Comandos.AgregarPromociones;
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

namespace Oficialia.Functions.Funciones
{
    public class PromocionesFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public PromocionesFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        /// <summary>
        /// Obtiene las promociones con los filtros especificados
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("Promociones")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Promociones" }, Summary = " Obtiene las promociones con los filtros especificados")]
        [OpenApiParameter(name: "fechaInicial", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Fecha inicial de donde comieza el rango a buscar de las promociones, el fortmato es dd/MM/YYYY")]
        [OpenApiParameter(name: "fechaFinal", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Fecha final  de donde se termina el rango a buscar de las promociones, el fortmato es dd/MM/YYYY")]
        [OpenApiParameter(name: "estado", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "Estado de las promocociones (0 - indica todos 1-Sin captura)")]
        [OpenApiParameter(name: "texto", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "Texto que se usa para filtrar las promociones")]
        [OpenApiParameter(name: "ordenarPor", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Campo por el que se ordena ")]
        [OpenApiParameter(name: "descendente", In = ParameterLocation.Query, Required = false, Type = typeof(bool), Description = "Indica si el ordenamiento es ascendente o descendente ")]
        [OpenApiParameter(name: "pagina", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Número de página")]
        [OpenApiParameter(name: "registrosPorPagina", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Número de registros por página")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<PromocionDto>), Description = "Respuesta exitosa, contiene una lista de objetos 'PromocionDto' en formato JSON.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "promociones")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"Fecha inicial: {req.Query["fechaInicial"]} Fecha final: {req.Query["fechaFinal"]} estado:{req.Query["estado"]} texto: {req.Query["texto"]} ");
            long secretarioId = long.TryParse(req.Query["secretario"].ToString(), out secretarioId) ? Convert.ToInt64(req.Query["secretario"]) :0;
            var consulta = new ObtienePromocionesConFiltroConsulta()
            {
                FechaInicial = req.Query["fechaInicial"],
                FechaFinal = req.Query["fechaFinal"],
                Estado = Convert.ToInt32(req.Query["estado"]),
                Texto = req.Query["texto"],
                OrdenarPor = req.Query["ordenarPor"],
                Descendente = Convert.ToBoolean(req.Query["descendente"]),
                Pagina = Convert.ToInt32(req.Query["pagina"]),
                RegistrosPorPagina = Convert.ToInt32(req.Query["registrosPorPagina"]),
                Origen = req.Query["origen"],
                Asignado = secretarioId,
                Capturo = req.Query["capturo"],

            };

            return await _processor.ExecuteAsync<ObtienePromocionesConFiltroConsulta, ListaPaginada<PromocionDto, MetaDataEstadosDto>>(
                                                               consulta);
        }

        /// <summary>
        /// Obtiene las promociones con los filtros especificados
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("GuardarPromocion")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "GuardarPromocion" }, Summary = " Guardan una promoción")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(long), Description = "Confirma el guardado exitoso de la promoción")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiRequestBody(contentType: "json", bodyType: typeof(AgregarPromocionDto), Description = "Objeto que se usa para guardar la promocion", Example = typeof(AgregarPromocionDto))]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run2(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "promociones")]
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            AgregarPromocionDto promocionDto = JsonConvert.DeserializeObject<AgregarPromocionDto>(requestBody);
            AgregarPromocionComando comando = new AgregarPromocionComando();
            comando.Promocion = promocionDto;

            return await _processor.ExecuteAsync<AgregarPromocionComando, DatosPromocion>(comando);
        }

        /// <summary>
        /// Obtiene las promociones con los filtros especificados
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("EditarPromocion")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "EditarPromocion" }, Summary = " Edita una promoción")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(long), Description = "Confirma la edición exitosa de la promoción")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiRequestBody(contentType: "json", bodyType: typeof(EditarPromocionDto), Description = "Objeto que se usa para editar una promocion", Example = typeof(EditarPromocionDto))]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run3(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "promociones")]
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            EditarPromocionDto promocion = JsonConvert.DeserializeObject<EditarPromocionDto>(requestBody);
            EditarPromocionComando promocionComando = new EditarPromocionComando();
            promocionComando.Promocion = promocion;

            return await _processor.ExecuteAsync<EditarPromocionComando, long>(promocionComando);
        }

        /// <summary>
        /// Obtiene las promociones con los filtros especificados
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("EliminarPromocion")]
        [OpenApiOperation(operationId: "Run4", tags: new[] { "EliminarPromocion" }, Summary = " Elimina una promoción")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.NoContent, contentType: "application/json", bodyType: typeof(string), Description = "Confirma la eliminacion exitosa de la promoción")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiRequestBody(contentType: "json", bodyType: typeof(EliminarPromocion), Description = "Objeto que se usa para eliminar una promocion", Example = typeof(EliminarPromocionDto))]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run4(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "promociones")]
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            EliminarPromocionDto promocion = JsonConvert.DeserializeObject<EliminarPromocionDto>(requestBody);
            EliminarPromocionComando promocionComando = new EliminarPromocionComando();
            promocionComando.Promocion = promocion;

            return await _processor.ExecuteAsync<EliminarPromocionComando, string>(promocionComando);
        }
    }
}

