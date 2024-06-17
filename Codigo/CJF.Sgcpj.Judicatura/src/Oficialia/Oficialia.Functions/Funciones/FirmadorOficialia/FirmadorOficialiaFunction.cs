using System;
using System.IO;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Azure.Identity;
using Azure.Data.Tables;
using CJF.Sgcpj.Judicatura.Tramite.Application.FirmadorDocumentos;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Tramite.Application.Models;
using CJF.Sgcpj.Judicatura.Tramite.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Common.Functions;
using CJF.Sgcpj.Judicatura.Application.Common.Repositories;

namespace Oficialia.Functions.Funciones.FirmadorOficialia
{
    public class FirmadorOficialiaFunction
    {
        private readonly IConfiguration _configuration;
        private readonly IPromocionesRepository _promocionesRepository;

        public FirmadorOficialiaFunction(IConfiguration configuration, IPromocionesRepository promocionesRepository)
        {
            _configuration = configuration;
            _promocionesRepository = promocionesRepository;
        }

        /// <summary>
        /// Obtiene los datos requeridos para iniciar el firmador de promociones
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("FirmadorOficialiaFunction")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Iniciar firmador" }, Summary = "Inicia flujo del firmador")]
        [OpenApiRequestBody(contentType: "json", bodyType: typeof(FirmadorInicioPeticionDto), Description = "Objeto que se usa para transferir los archivos", Example = typeof(FirmadorInicioPeticionDto))] //trae la accion (pre, autorizar o cancelar y la lista de documentos)
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Error interno en el servidor")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "Error interno en el servidor")] //mensaje de error si falla
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
                 [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "firmadoroficialia/inicio")] HttpRequest req,
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
                var urlRetorno = _configuration["SISE3:BackEnd:FirmadorUrlRetornoOficialia"];
                string lecturaDocumento = string.Format(urlLectuaDocumento, "1");

                httpClientInicio.BaseAddress = new Uri(urlFirmador);

                httpClientInicio.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var setting = _configuration["SISE3:BackEnd:AlertasUrlTabla"];
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var documentosRequest = JsonConvert.DeserializeObject<FirmadorInicioPeticionDto>(requestBody);
                var tableClient = new TableClient(new Uri(setting), "RelacionAcuerdos", new DefaultAzureCredential());

                var oficiosDocumento = new List<FirmadorInicioDocumentoPeticionDto>();

                
                //foreach (var acuerdo in documentosRequest.Documentos)
                //{
                    //var oficiosYAcuerdo = await _promocionesRepository.ObtenerPromociones(acuerdo.Id);
                    //acuerdo.Nombre = acuerdo.Nombre + ".p7m";
                    //MapOficios(oficiosDocumento, oficiosYAcuerdo);
                //}

                if (oficiosDocumento.Any())
                {
                    documentosRequest.Documentos.AddRange(oficiosDocumento);
                }

                foreach (var promocion in documentosRequest.Documentos)
                {
                    promocion.Id = "1-" + promocion.Id;
                    var index = promocion.Nombre.IndexOf(".pdf");
                    promocion.Nombre = promocion.Nombre.Substring(0, (index));
                    //promocion.Nombre = promocion.Nombre.Replace("pdf", "PDF");
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
