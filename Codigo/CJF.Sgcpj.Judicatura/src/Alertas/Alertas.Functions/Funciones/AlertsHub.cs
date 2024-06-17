using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.OpenApi.Models;

namespace Alertas.Functions.Funciones
{
    public class AlertsHub
    {
        [FunctionName("negotiate")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "NegotiateToken" }, Summary = "Permite conectarse al hub de Azure SignalR")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SignalRConnectionInfo), Description = "Datos de conexión a SignalR")]
        public SignalRConnectionInfo Negotiate(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
            [SignalRConnectionInfo(HubName = "AlertsHub")] 
                                   SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }
    }
}