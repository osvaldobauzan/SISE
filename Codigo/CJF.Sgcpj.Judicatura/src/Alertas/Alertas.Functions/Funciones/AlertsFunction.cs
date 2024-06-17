using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Alertas.Application.Alertas.Comandos.ConexionesHandler;
using CJF.Sgcpj.Judicatura.Alertas.Application.Alertas.Comandos.EliminarAlertas;
using CJF.Sgcpj.Judicatura.Alertas.Application.Alertas.Comandos.RecuperarAlertas;
using CJF.Sgcpj.Judicatura.Alertas.Application.Alertas.Models;
using Common.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Alertas.Functions.Funciones;

public class AlertsFunction
{
    private readonly IHttpRequestProcessor _httpRequestProcessor;

    public AlertsFunction(IHttpRequestProcessor httpRequestProcessor)
    {
        _httpRequestProcessor = httpRequestProcessor;
    }

    [FunctionName("ConexionesHandler")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "HandleConnections" }, Summary = " Maneja las conexiones a SignalR por usuario y organismo")]
    [OpenApiRequestBody(contentType: "json", bodyType: typeof(UserConnectionsDTO), Description = "Objeto que contiene la conexión actual a SignalR, más las anteriores", Example = typeof(UserConnectionsDTO))]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK, Description = "Se manejaron correctamente las conexiones")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiRequestBody("multipart/form-data", typeof(UserConnectionsDTO), Description = "Upload a file to the SFTP Server.")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> HandleConnectionsAsync
        ([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "connections")] HttpRequest req)
    {
        var body = await new StreamReader(req.Body).ReadToEndAsync();
        var connections = JsonConvert.DeserializeObject<UserConnectionsDTO>(body);

        var alertsRequest = new ConexionesHandlerCommand();
        alertsRequest.UserConnections = connections;

        return await _httpRequestProcessor.ExecuteAsync<ConexionesHandlerCommand, bool>(alertsRequest);
    }

    [FunctionName("AlertasPorUsuario")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "AlertasPorUsuario" }, Summary = "Recupera las alertas por usuario y organismo")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NoContent, Description = "No existen alertas para ese usuario/organismo")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "No fue posible recuperar las alertas")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(IEnumerable<AlertQueryDTO>), Description = "Alertas por usuario/organismo")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> GetAlertsByUserIdAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, methods: "get", Route = "alertas")] HttpRequest req)
    {
        var alertsRequest = new RecuperarAlertasCommand();

        return await _httpRequestProcessor.ExecuteAsync<RecuperarAlertasCommand, IEnumerable<AlertQueryDTO>?>(alertsRequest);
    }

    [FunctionName("EliminarAlerta")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "EliminarAlerta" }, Summary = "Elimina alerta por id único")]
    [OpenApiParameter(name: "alertaId", In = ParameterLocation.Query, Type = typeof(string), Description = "Id de la alerta")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "No fue posible eliminar la alerta")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK, Description = "Alerta eliminada")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> DeleteAlertAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, methods: "delete", Route = "alertas")] HttpRequest req)
    {

        var alertsRequest = new EliminarAlertasCommand();
        alertsRequest.AlertId = req.Query["alertaId"];

        return await _httpRequestProcessor.ExecuteAsync<EliminarAlertasCommand, bool>(alertsRequest);
    }
}
