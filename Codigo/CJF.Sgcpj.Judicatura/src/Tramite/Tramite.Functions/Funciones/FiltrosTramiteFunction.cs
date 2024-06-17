using System;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerFiltrosTramite;
using Common.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Tramite.Functions.Funciones
{
    public class FiltrosTramiteFunction
    {
        private readonly IHttpRequestProcessor _processor;
        public FiltrosTramiteFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("ObtenerFiltrosFunction")]
        [OpenApiOperation(operationId: "ObtenerFiltros", tags: new[] { "ObtenerFiltros" }, Summary = "Obtienen el detalle de los filtros del tablero de tramite.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "Respuesta exitosa, se obtiene la lista de los acuerdo ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> GetObtenerAcuerdo([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tramite/filtros")] HttpRequest req,
          ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            
            return await _processor.ExecuteAsync<ObtieneFiltroTramiteConsulta,FiltroTramite<ObtieneFiltroSecretarioDto,
                ObtieneFiltroOrigenDto, ObtieneFiltroTipoAsuntoDto, ObtieneFiltroCapturoDto, ObtieneFiltroPreautorizoDto,
                ObtieneFiltroAutorizoDto, ObtieneFiltroCanceloDto>>(new ObtieneFiltroTramiteConsulta());
        }
    }
}

