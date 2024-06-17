using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Azure.Data.Tables;
using Azure.Identity;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Tramite.Application.Firmador;
using CJF.Sgcpj.Judicatura.Tramite.Application.FirmadorDocumentos;
using CJF.Sgcpj.Judicatura.Tramite.Application.Models;
using CJF.Sgcpj.Judicatura.Tramite.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Tramite.Functions.Funciones
{
    public class FirmadorFunction
    {
        private const int ESTADO_PREAUTORIZAR = 1;
        private const int ESTADO_AUTORIZAR = 2;
        private readonly IConfiguration _configuration;
        private readonly ITramitesRepository _tramitesRepository;

        public FirmadorFunction(IConfiguration configuration, ITramitesRepository tramitesRepository)
        {
            _configuration = configuration;
            _tramitesRepository = tramitesRepository;
        }

        [FunctionName("IniciarFirmador")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Iniciar firmador" }, Summary = "Inicia flujo del firmador")]
        [OpenApiRequestBody(contentType: "json", bodyType: typeof(FirmadorInicioPeticionDto), Description = "Objeto que se usa para transferir los archivos", Example = typeof(FirmadorInicioPeticionDto))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Error interno en el servidor")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "Error interno en el servidor")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run3(
                 [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "firmador/inicio")] HttpRequest req,
                 ILogger log)
        {
            try
            {
                var token = await GetToken();

                //INICIAR
                using var httpClientInicio = new HttpClient();
                var urlFirmador = _configuration["SISE3:BackEnd:FirmadorUrlIniciar"];
                var urlGuardarDocumento = _configuration["SISE3:BackEnd:FirmadorEndPointGuardadoDocumento"];
                var urlLectuaDocumento = _configuration["SISE3:BackEnd:FirmadorEndPointLecturaDocumento"];
                var urlRetorno = _configuration["SISE3:BackEnd:FirmadorUrlRetorno"];
                string lecturaDocumento = string.Format(urlLectuaDocumento, "2");

                httpClientInicio.BaseAddress = new Uri(urlFirmador);

                httpClientInicio.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var setting = _configuration["SISE3:BackEnd:AlertasUrlTabla"];
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var documentosRequest = JsonConvert.DeserializeObject<FirmadorInicioPeticionDto>(requestBody);
                var tableClient = new TableClient(new Uri(setting), "RelacionAcuerdos", new DefaultAzureCredential());

                var oficiosDocumento = new List<FirmadorInicioDocumentoPeticionDto>();

                if (documentosRequest.FirmarOficios && documentosRequest.Accion == ESTADO_PREAUTORIZAR)
                {

                    foreach (var acuerdo in documentosRequest.Documentos)
                    {
                        var oficiosYAcuerdo = await _tramitesRepository.ObtenerAcuerdosOficios(new Guid(acuerdo.Id));
                        MapOficios(oficiosDocumento, oficiosYAcuerdo);
                    }
                }

                if (documentosRequest.Accion == ESTADO_AUTORIZAR)
                {
                    foreach (var acuerdo in documentosRequest.Documentos)
                    {
                        var oficiosYAcuerdo = await _tramitesRepository.ObtenerAcuerdosOficios(new Guid (acuerdo.Id));
                        acuerdo.Nombre = acuerdo.Nombre + ".p7m";

                        if (oficiosYAcuerdo != null)
                        {
                            oficiosYAcuerdo = oficiosYAcuerdo.Where(s => !s.Firmado)?.ToList();
                        }

                        MapOficios(oficiosDocumento, oficiosYAcuerdo);
                    }

                }
                if (oficiosDocumento.Any())
                {
                    documentosRequest.Documentos.AddRange(oficiosDocumento);
                }

                if (!documentosRequest.FirmarOficios && documentosRequest.Documentos.Any())
                {
                    var urlStorage = _configuration["SISE3:BackEnd:AlertasUrlTabla"];
                    var tableClientDocumentos = new TableClient(new Uri(urlStorage), "Documentos", new DefaultAzureCredential());

                    var tasks = documentosRequest.Documentos.Select(async (d) =>
                    {
                        await tableClientDocumentos.UpsertEntityAsync<DocumentoTableEntity>(new DocumentoTableEntity()
                        {
                            RowKey = d.Id.ToString(),
                            PartitionKey = d.Modulo.ToString(),
                            Nombre = d.Nombre,
                            TipoArchivo = d.TipoArchivo
                        });
                    });

                    await Task.WhenAll(tasks);
                }

                foreach (var acuerdo in documentosRequest.Documentos)
                {
                    acuerdo.Id = "2-"+acuerdo.Id;
                }

                var inicioPayload = JsonConvert.SerializeObject(new FirmadorInicioPeticion()
                {
                    DocumentosInfo = documentosRequest.Documentos,
                    UrlGuardarDocumento = urlGuardarDocumento,
                    UrlObtenerDocumento = lecturaDocumento,
                    UrlRetorno = urlRetorno,
                    Rubrica = false
                });

                var inicioResult = await httpClientInicio.PostAsync(urlFirmador,
                    new StringContent(inicioPayload, Encoding.UTF8, MediaTypeNames.Application.Json));

                if (!inicioResult.IsSuccessStatusCode)
                {
                    return new BadRequestObjectResult("Inicio API no disponible");
                }

                var inicioContent = await inicioResult.Content.ReadAsStringAsync();
                var inicioRespuesta = JsonConvert.DeserializeObject<FirmadorInicioDto>(inicioContent);

                return new OkObjectResult(inicioRespuesta);
            }
            catch (Exception ex)
            {
                log?.LogError(ex.ToString());
                return new InternalServerErrorResult();
            }
        }


        [FunctionName("StatusFirmador")]
        [OpenApiOperation(operationId: "StatusFirmadorAsync", tags: new[] { "Status firmador" }, Summary = "Inicia flujo del chequeo de status de archivos firmador")]
        [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "GUID de la operación")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Error interno en el servidor")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "Error interno en el servidor")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> StatusFirmadorAsync(
                 [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "firmador/estado/documentos")] HttpRequest req,
                 ILogger log)
        {
            try
            {
                var id = req.Query["id"];
                log?.LogInformation($"Id: {id}");

                var token = await GetToken();

                var setting = _configuration["SISE3:BackEnd:FirmadorUrlEstadoDocumentos"];

                using var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(setting);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var requestResult = await httpClient.GetAsync($"{id}");
                var content = await requestResult.Content.ReadAsStringAsync();

                if (!requestResult.IsSuccessStatusCode)
                {
                    log?.LogError($"Api status error: {content}");
                    return new BadRequestObjectResult("API Status no disponible");
                }

                var aux = JsonConvert.DeserializeObject<IEnumerable<FirmadorStatusAux>>(content);

                var result = new List<FirmadorStatusDto>();
                foreach(var reg in aux)
                {
                    var firmador = new FirmadorStatusDto(){
                        Id = new Guid(reg.Id.Substring(2)),
                        Nombre = reg.Nombre,
                        ContentType = reg.ContentType,
                        Estatus = reg.Estatus,
                        MensajeError = reg.MensajeError,
                    };
                    result.Add(firmador);
                    
                }

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                log?.LogError(ex.ToString());
                return new InternalServerErrorResult();
            }
        }

        private async Task<string> GetToken()
        {
            using var httpClientToken = new HttpClient();
            var urlToken = _configuration["SISE3:BackEnd:FirmadorUrlToken"];
            httpClientToken.BaseAddress = new Uri(urlToken);
            var usuarioToken = _configuration["SISE3:BackEnd:FirmadorTokenUsuario"];
            var passwordToken = _configuration["SISE3:BackEnd:FirmadorTokenClave"];

            var credentials = JsonConvert.SerializeObject(new { usuario = usuarioToken, clave = passwordToken });

            var tokenResult = await httpClientToken.PostAsync(urlToken,
                new StringContent(credentials, Encoding.UTF8, MediaTypeNames.Application.Json));

            if (!tokenResult.IsSuccessStatusCode)
            {
                throw new AuthenticationFailedException("Token API no disponible");
            }

            var token = await tokenResult.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<string>(token);
        }

        private static void MapOficios(List<FirmadorInicioDocumentoPeticionDto> oficiosDocumento, List<InfoDocumentos> documentos)
        {
            if (documentos != null && documentos.Any())
            {
                foreach (var item in documentos.Where(a => a.EsAcuerdo == 0))
                {
                    var oficio = new FirmadorInicioDocumentoPeticionDto
                    {
                        Id = item.uGuid.ToString(),
                        Modulo = 2,
                        Nombre = item.NombreArchivo + item.ExtensionDocumento,
                        TipoArchivo = "oficio"
                    };
                    oficiosDocumento.Add(oficio);
                }
            }
        }
    }
}
