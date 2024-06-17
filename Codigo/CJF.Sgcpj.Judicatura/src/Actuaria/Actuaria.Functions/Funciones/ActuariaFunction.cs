using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerNotificacionesConFiltro;
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

namespace Actuaria.Functions.Funciones
{
    public class ActuariaFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public ActuariaFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        /// <summary>
        /// Obtiene las Notificaciones con los filtros especificados
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("Actuaria")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Actuaria" }, Summary = " Obtiene las notificaciones con los filtros especificados")]
        [OpenApiParameter(name: "fechaInicial", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Fecha inicial de donde comieza el rango a buscar de las notificaciones, el fortmato es dd/MM/YYYY")]
        [OpenApiParameter(name: "fechaFinal", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Fecha final  de donde se termina el rango a buscar de las notificaciones, el fortmato es dd/MM/YYYY")]
        [OpenApiParameter(name: "tipoFiltro", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "Recibe parámetro del tipo de filtro 0=VerTodas, 1=2días, 2=+3días, 3=1día ,4=Notificados")]
        [OpenApiParameter(name: "texto", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "Texto que se usa para filtrar las notificaciones")]
        [OpenApiParameter(name: "ordenarPor", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Campo por el que se ordena ")]
        [OpenApiParameter(name: "descendente", In = ParameterLocation.Query, Required = false, Type = typeof(bool), Description = "Indica si el ordenamiento es ascendente o descendente ")]
        [OpenApiParameter(name: "pagina", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Número de página")]
        [OpenApiParameter(name: "registrosPorPagina", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Número de registros por página")]
        [OpenApiParameter(name: "estado", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "Recibe estado(Sin asignar, Pendiente, Notificados)")]
        [OpenApiParameter(name: "contenido", In = ParameterLocation.Query, Required = false, Type = typeof(int?), Description = "ID del contenido")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<NotificacionDto>), Description = "Respuesta exitosa, contiene una lista de objetos 'NotificacionDto' en formato JSON.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "actuaria")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"Fecha inicial: {req.Query["fechaInicial"]} Fecha final: {req.Query["fechaFinal"]} tipoFiltro:{req.Query["tipoFiltro"]} texto: {req.Query["texto"]} origen: {req.Query["origen"]} ");

            var aux = 0;
            int? contenido = int.TryParse(req.Query["contenido"].ToString(), out aux) ? Convert.ToInt32(req.Query["contenido"]) : null;
            var consulta = new ObtieneNotificacionesConFiltroConsulta()
            {
                FechaInicial = req.Query["fechaInicial"],
                FechaFinal = req.Query["fechaFinal"],
                TipoFiltro = Convert.ToInt32(req.Query["tipoFiltro"]),
                Texto = req.Query["texto"],
                OrdenarPor = req.Query["ordenarPor"],
                Descendente = Convert.ToBoolean(req.Query["descendente"]),
                Pagina = Convert.ToInt32(req.Query["pagina"]),
                RegistrosPorPagina = Convert.ToInt32(req.Query["registrosPorPagina"]),
                Estado = req.Query["estado"],
                Contenido = contenido
            };

            return await _processor.ExecuteAsync<ObtieneNotificacionesConFiltroConsulta, ListaPaginada<NotificacionDto, MetaDataEstadosDto>>(
                                                               consulta);
        }

    }
}

