using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Azure.Identity;
using System.Net.Http;
using System.Net.Mime;
using Microsoft.Extensions.Configuration;
using Azure.Data.Tables;
using System.Net.Http.Headers;
using System.Web.Http;
using CJF.Sgcpj.Judicatura.Tramite.Application.FirmadorDocumentos;
using CJF.Sgcpj.Judicatura.Tramite.Application.Firmador;
using static System.Net.WebRequestMethods;

namespace CJF.Sgcpj.Judicatura.Sentencias.Functions.Funciones.FirmadorSentencias;
internal class FirmadorSentenciasFunction
{
    private readonly IConfiguration _configuration;

    public FirmadorSentenciasFunction(IConfiguration configuration) 
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Función para iniciar firmador sentencias
    /// </summary>
    /// <param name="req"></param>
    /// <param name="log"></param>
    /// <returns></returns>
    [FunctionName("FirmadorSentencias")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "Iniciar firmador" }, Summary = "Inicia flujo del firmador")]
    [OpenApiRequestBody(contentType: "json", bodyType: typeof(FirmadorInicioPeticionDto), Description = "Objeto que se usa para transferir los archivos", Example = typeof(FirmadorInicioPeticionDto))]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Error interno en el servidor")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "Error interno en el servidor")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run3(
                 [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "firmadorsentencias/inicio")] HttpRequest req,
                 ILogger log)
    {
        try
        {
            var token = await GetToken();

            //INICIAR
            using var httpClientInicio = new HttpClient();
            var urlFirmador = _configuration["SISE3:BackEnd:FirmadorUrlIniciar"];
            var urlGuardarDocumento = _configuration["SISE3:BackEnd:FirmadorEndPointGuardadoDocumentoSentencia"];
            var urlLectuaDocumento = _configuration["SISE3:BackEnd:FirmadorEndPointLecturaDocumentoSentencia"];
            var urlRetorno = _configuration["SISE3:BackEnd:FirmadorUrlRetornoSentencia"];

            httpClientInicio.BaseAddress = new Uri(urlFirmador);

            httpClientInicio.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var setting = _configuration["SISE3:BackEnd:AlertasUrlTabla"];
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var documentosRequest = JsonConvert.DeserializeObject<FirmadorInicioPeticionDto>(requestBody);
            var tableClient = new TableClient(new Uri(setting), "RelacionAcuerdos", new DefaultAzureCredential());

            var oficiosDocumento = new List<FirmadorInicioDocumentoPeticionDto>();
          

            var inicioPayload = JsonConvert.SerializeObject(new FirmadorInicioPeticion()
            {
                DocumentosInfo = documentosRequest.Documentos,
                UrlGuardarDocumento = urlGuardarDocumento,
                UrlObtenerDocumento = urlLectuaDocumento,
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

            return new OkObjectResult(JsonConvert.DeserializeObject<IEnumerable<FirmadorStatusDto>>(content));
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
}
