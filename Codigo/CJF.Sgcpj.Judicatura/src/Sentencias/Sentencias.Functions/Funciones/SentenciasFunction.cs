using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Sentencias.Application.Sentencias.Comandos.SubirSentenciaComando;
using Common.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Sentencias.Functions.Funciones;

public class SentenciasFunction
{
    private readonly IHttpRequestProcessor _processor;

    public SentenciasFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }

    [FunctionName("SentenciasFunction")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "SubirSentenciaFunction" }, Summary = " Operación para registrar una sentencia con su archivo")]
    [OpenApiRequestBody(contentType: "multipart/form-data", bodyType: typeof(ArchivoSentenciaDto), Required = true, Description = "Parámetros requeridos para subir el documento de la sentencia.")]

    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ArchivoSentenciaDto), Description = "Indica TRUE si se pudo subir el archivo; en caso contrario, indica FALSE ")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "sentencias")] HttpRequest req, ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        var formCollection = await req.ReadFormAsync();

        var sentencia = formCollection["sentencia"];
        var sentenciaVP = formCollection["sentenciaVP"];

        var sentenciaReq = new SubirSentenciaComando
        {
            Sentencia = sentencia,
            SentenciaVP = sentenciaVP,
        };

        if (formCollection.Files.Any())
        {
            var file = formCollection.Files.FirstOrDefault();
            using var fileStream = file.OpenReadStream();
            byte[] bytes = new byte[file.Length];
            fileStream.Read(bytes, 0, (int)file.Length);
            sentenciaReq.ArchivoBytes = bytes;
            sentenciaReq.NomArchivoReal = file.FileName;
        }

        return await _processor.ExecuteAsync<SubirSentenciaComando, SentenciaDto>(sentenciaReq);
    }
}
