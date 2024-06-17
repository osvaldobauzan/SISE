using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Usuarios.Application.Usuarios.BusquedaParteExistente.Consulta;
using CJF.Sgcpj.Judicatura.Usuarios.Application.Usuarios.ParteExistente.Consulta;
using Common.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Usuarios.Functions.Funciones
{
    public class CatalogoParteExistenteFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public CatalogoParteExistenteFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("CatalogoParteExistente")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "CatalogoParteExistente" }, Summary = " Obtiene el promovente cuando hay una Parte Existente")]
        [OpenApiParameter(name: "AsuntoNeunId", In = ParameterLocation.Query, Required = true, Type = typeof(long), Description = "Representa el identificador del AsuntoNeun")]
        [OpenApiParameter(name: "NoExp", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "Representa el número del expediente")]
        [OpenApiParameter(name: "Texto", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "Texto que se usa para filtrar las partes")]
        [OpenApiParameter(name: "Modulo", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "Parametro que nos ayuda a identificar en que modulo estamos Oficialia = 1 , Tramite = 2")]
        [OpenApiParameter(name: "Tipo parte", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "Parametro que nos ayuda a identificar que tipo de persona es la queremos ver: Persona = 1,Autoridad = 2")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<CatalogoParteExistenteDto>), Description = "Catálogo para Parte Existente obtenido con éxito")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "parteexistente")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"AsuntoNeunId: {req.Query["AsuntoNeunId"]} NoExp: {req.Query["NoExp"]} " +
                               $"texto: {req.Query["Texto"]} Modulo: {req.Query["Modulo"]} " +
                               $"TipoParte: {req.Query["TipoParte"]}");

            var consulta = new ObtieneCatalogoParteExistente
            {
                AsuntoNeunId = Convert.ToInt64(req.Query["AsuntoNeunId"]),
                NoExp = req.Query["NoExp"],
                Texto = req.Query["Texto"],
                Modulo = Convert.ToInt32(req.Query["Modulo"]),
                TipoParte = Convert.ToInt32(req.Query["TipoParte"]),
            };

            return await _processor.ExecuteAsync<ObtieneCatalogoParteExistente, List<CatalogoParteExistenteDto>>(consulta);
        }

        [FunctionName("BusquedaPartes")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "BusquedaPartes" }, Summary = " Obtiene las partes a partir de varios criterios")]

        [OpenApiParameter(name: "CatOrganismoId", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Representa los organismos donde puede encontrarse una parte")]
        [OpenApiParameter(name: "CatTipoPersonaId", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Representa el Tipo de Persona de una parte")]
        [OpenApiParameter(name: "Nombre", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Representa el Nombre de una parte")]
        [OpenApiParameter(name: "APaterno", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Representa el Apellido Paterno de una parte")]
        [OpenApiParameter(name: "AMaterno", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Representa el Apellido Materno de una parte")]
        [OpenApiParameter(name: "FechaInicial", In = ParameterLocation.Query, Required = true, Type = typeof(DateTime), Description = "Representa la fecha inicial de alta  de una parte")]
        [OpenApiParameter(name: "FechaFinal", In = ParameterLocation.Query, Required = true, Type = typeof(DateTime), Description = "Representa la fecha final de alta de una parte")]
        [OpenApiParameter(name: "Anio", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Representa elaño de alta de una parte")]
        [OpenApiParameter(name: "CatTipoAsuntoId", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Representa el Tipo de Asunto del expediente al que pertenece la parte")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<BusquedaParteDTO>), Description = "Catálogo para Parte Existente obtenido con éxito")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run2(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "busquedaParte")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            DateTime? FechaInicial = string.IsNullOrEmpty(req.Query["FechaInicial"]) ? null : Convert.ToDateTime(req.Query["FechaInicial"]);
            DateTime? FechaFinal = string.IsNullOrEmpty(req.Query["FechaFinal"]) ? null : Convert.ToDateTime(req.Query["FechaFinal"]);
            var consulta = new BusquedaParteFiltro
            {
                CatOrganismoId = req.Query["CatOrganismoId"],
                CatTipoPersonaId = Convert.ToInt32(req.Query["CatTipoPersonaId"]),
                Nombre = req.Query["Nombre"],
                APaterno = req.Query["APaterno"],
                AMaterno = req.Query["AMaterno"],
                FechaInicial = FechaInicial,
                FechaFinal = FechaFinal,
                Anio = Convert.ToInt32(req.Query["Anio"]),
                CatTipoAsuntoId = Convert.ToInt32(req.Query["CatTipoAsuntoId"]),
            };
            return await _processor.ExecuteAsync<BusquedaParteFiltro, List<BusquedaParteDTO>>(consulta);
        }
    }
}

