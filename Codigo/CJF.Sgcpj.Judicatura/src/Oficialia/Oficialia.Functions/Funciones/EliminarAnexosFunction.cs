using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Application.Common.Models;

using CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.RollBackAnexo;
using CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.RollbackArchivoComando;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using Common.Functions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Oficialia.Functions.Funciones;
public class EliminarAnexosFunction
{
    private readonly IHttpRequestProcessor _processor;

    public EliminarAnexosFunction(IHttpRequestProcessor processor)
    {

        _processor = processor;
    }

    [FunctionName("EliminarAnexo")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "EliminarAnexo" }, Summary = "Elimina un anexo")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(RollBackArchivoDto), Description = "Se elimino correctamente el anexo")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiRequestBody(contentType: "json", bodyType: typeof(RollBackAnexoDto), Description = "Json que se usa para eliminar un anexo", Example = typeof(RollBackAnexoDto))]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "anexos")] HttpRequest req,

            ILogger log)
    {

        log.LogInformation("C# HTTP trigger function processed a request.");


        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        RollBackAnexoDto anexo = JsonConvert.DeserializeObject<RollBackAnexoDto>(requestBody);
        RollBackComando anexoComando = new RollBackComando();

        anexoComando.Anexo = anexo;

        return await _processor.ExecuteAsync<RollBackComando, long>(anexoComando);
    }
}

