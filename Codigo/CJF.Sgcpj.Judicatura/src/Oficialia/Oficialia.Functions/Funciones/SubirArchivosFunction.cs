using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.CargaMasivaComando;
using CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.SubirArchivoComando;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Promociones.Comandos.CargaMasivaComando;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Promociones.Comandos.SubirArchivoComando;
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
    public class SubirArchivosFunction
    {

        private readonly IHttpRequestProcessor _processor;

        public SubirArchivosFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }
        /// <summary>
        /// Recibe archivos en pdf y los vicula a los expedientes
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("SubirArchivos")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "SubirArchivos" }, Summary = " Operación de carga de archivos")]
        [OpenApiRequestBody(contentType: "multipart/form-data", bodyType: typeof(OficialiaArchivoDto), Required = true, Description = "Parámetros requeridos para hacer la carga de archivos.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Indica TRUE si se pudo subir el archivo; en caso contrario, indica FALSE ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]

        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "promociones/archivo")] HttpRequest req,

            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var formCollection = await req.ReadFormAsync();

            
            long asuntoNeunId = long.Parse(formCollection["asuntoNeunId"]);
            int noRegistro = int.Parse(formCollection["noRegistro"]);
            int asuntoId = Convert.ToInt32(formCollection["asuntoId"]);
            int numeroOrden = int.Parse(formCollection["numeroOrden"]);
            int origen = int.Parse(formCollection["origen"]);
            int yearPromocion = int.Parse(formCollection["yearPromocion"]);
            short fojas = 0;
            Int16.TryParse(formCollection["fojas"],out fojas);
            var tipoAsunto = formCollection["tipoAsunto"].ToString();
            var tipoProcedimiento = formCollection["tipoProcedimiento"].ToString();
            var numeroExpediente = formCollection["numeroExpediente"].ToString();
            var mesa = formCollection["mesa"].ToString();
            var secretarioId = formCollection["secretarioId"].ToString();
            var enviarAlerta = bool.TryParse((formCollection["enviarAlerta"].ToString()), out bool _);

            var subirArchivosComando = new SubirArchivoCommando()
            {
                AsuntoNeunId = asuntoNeunId,  //30302119,
                NumeroOrden = numeroOrden,
                Origen = origen,
                AsuntoID = asuntoId,
                YearPromocion = yearPromocion,
                NumeroRegistro = noRegistro,
                Fojas = fojas,
                TipoAsunto = tipoAsunto,
                TipoProcedimiento = tipoProcedimiento,
                Mesa = mesa,
                SecretarioId = secretarioId,
                NumeroExpediente = numeroExpediente,
                EnviarAlerta = enviarAlerta
            };

            subirArchivosComando.Archivos = new List<SubirArchivoDto>();

            var index = 0;
            if (formCollection.Files.Count > 0)
            {
                foreach (var file in formCollection.Files)
                {
                    using var fileStream = file.OpenReadStream();
                    byte[] bytes = new byte[file.Length];
                    fileStream.Read(bytes, 0, (int)file.Length);

                    var archivo = new SubirArchivoDto
                    {
                        NombreArchivo = file.FileName,
                        Data = bytes,
                        Clase = 0,
                        Descripcion = 1,
                        Caracter = 0
                    };

                    subirArchivosComando.Archivos.Add(archivo);
                    index++;
                }
            }
            else
            {
                var archivo = new SubirArchivoDto
                {
                    NombreArchivo = "",
                    Clase = 0,
                    Descripcion = 1,
                    Caracter = 0
                };
                subirArchivosComando.Archivos.Add(archivo);
            }

            return await _processor.ExecuteAsync<SubirArchivoCommando, List<string>>(subirArchivosComando);
        }

        /// <summary>
        /// Recibe archivos en pdf y los vincula a los expedientes
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("CargaMasiva")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "CargaMasiva" }, Summary = " Operación de carga de archivos")]
        [OpenApiRequestBody(contentType: "multipart/form-data", bodyType: typeof(OficialiaCargaMasivaDto), Required = true, Description = "Parámetros requeridos para hacer la carga de archivos.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<ResultadoCargaMasivaArchivoDto>), Description = "Indica TRUE si se pudo subir el archivo; en caso contrario, indica FALSE ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        public async Task<IActionResult> Run2(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "promociones/archivos")] HttpRequest req,

            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var formCollection = await req.ReadFormAsync();

            int yearPromocion = Convert.ToInt32(formCollection["yearPromocion"]);
            var promocionesValidas = formCollection["promociones"];
            List<PromocionesValidasDto>? listaPromocionesValidas = null;
            if (!string.IsNullOrEmpty(promocionesValidas))
                listaPromocionesValidas = JsonConvert.DeserializeObject<List<PromocionesValidasDto>>(promocionesValidas);
            var subirArchivosComando = new CargaMasivaComando()
            {
                PromocionesValidas = listaPromocionesValidas,
                YearPromocion = yearPromocion,
                NombreArchivoReal = "CargaMasiva-x",
            };

            subirArchivosComando.Archivos = new List<CargaMasivaArchivoDto>();

            var index = 0;
            foreach (var file in formCollection.Files)
            {
                using var fileStream = file.OpenReadStream();
                byte[] bytes = new byte[file.Length];
                fileStream.Read(bytes, 0, (int)file.Length);

                var archivo = new CargaMasivaArchivoDto
                {
                    NombreArchivo = file.FileName,
                    Data = bytes,
                    Clase = 0,
                    Descripcion = 1,
                };

                subirArchivosComando.Archivos.Add(archivo);
                index++;
            }

            return await _processor.ExecuteAsync<CargaMasivaComando, List<ResultadoCargaMasivaArchivoDto>>(subirArchivosComando);
        }
    }
}

