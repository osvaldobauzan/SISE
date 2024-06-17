using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.SubirAnexosComando;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Promociones.Comandos.SubirAnexosComando;
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

    public class SubirAnexosFunction
    {
        private readonly IHttpRequestProcessor _processor;

        public SubirAnexosFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }
        [FunctionName("SubirAnexos")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "anexos" }, Summary = " Operación para subir los anexos de la promoción")]
        [OpenApiRequestBody(contentType: "multipart/form-data", bodyType: typeof(OficialiaAnexoDto), Required = true, Description = "Parámetros requeridos para subir el documento de Anexo de la promoción.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Indica TRUE si se pudo subir el archivo; en caso contrario, indica FALSE ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "promociones/anexos")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            var formCollection = await req.ReadFormAsync();

            long asuntoNeunId = long.Parse(formCollection["asuntoNeunId"]);
            int noRegistro = int.Parse(formCollection["noRegistro"]);

            int numeroOrden = int.Parse(formCollection["numeroOrden"]);
            int origen = int.Parse(formCollection["origen"]);
            int yearPromocion = int.Parse(formCollection["yearPromocion"]);
            short fojas = Convert.ToInt16(formCollection["fojas"]);
            var archivosJson = JsonConvert.DeserializeObject<List<SubirAnexosDto>?>(formCollection["archivos"]);

            var subirAnexosComando = new SubirAnexosComando()
            {
                AsuntoNeunId = asuntoNeunId,
                NumeroOrden = numeroOrden,
                Origen = origen,
                AsuntoID = 1,
                YearPromocion = yearPromocion,
                NoRegistro = noRegistro,
                Archivos = archivosJson,
                Fojas = fojas
            };

            subirAnexosComando.Archivos = archivosJson;

            var index = 0;
            foreach (var file in formCollection.Files)
            {
                if (file.Length > 0)
                {
                    using var fileStream = file.OpenReadStream();
                    byte[] bytes = new byte[file.Length];
                    fileStream.Read(bytes, 0, (int)file.Length);
                    subirAnexosComando.Archivos[index].Data = bytes;
                    index++;
                }
            }

            return await _processor.ExecuteAsync<SubirAnexosComando, List<SubirAnexosDto>>(subirAnexosComando);
        }
    }
}
