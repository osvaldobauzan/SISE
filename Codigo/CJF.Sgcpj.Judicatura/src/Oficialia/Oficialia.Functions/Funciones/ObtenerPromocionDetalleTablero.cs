using CJF.Sgcpj.Judicatura.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.CalcularRegistro;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerPromocion;
using System.Net;
using System.Threading.Tasks;
using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtienePromocionesConFiltro;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerPromocionDetalle;
using System.Runtime.Intrinsics.X86;
using Common.Functions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;

namespace Oficialia.Functions.Funciones;
public class ObtenerPromocionDetalleTablero
{
    private readonly IHttpRequestProcessor _processor;

    public ObtenerPromocionDetalleTablero(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }
    [FunctionName("ObtenerPromocionDetalle")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "PromocionDetalle" }, Summary = " Obtiene el detalle de la promocion del tablero")]
    [OpenApiParameter(name: "AsuntoNeunId", In = ParameterLocation.Query, Required = true, Type = typeof(long), Description = "")]
    [OpenApiParameter(name: "UsuariId", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "")]
    [OpenApiParameter(name: "Origen", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "")]
    [OpenApiParameter(name: "NumeroOrden", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "")]
    [OpenApiParameter(name: "YearPromocion", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "")]
    [OpenApiParameter(name: "kIdElectronica", In = ParameterLocation.Query, Required = false, Type = typeof(long?), Description = "")]
    [OpenApiParameter(name: "horaPromocionElectronica", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(PromocionDetalleTableroDto), Description = "Respuesta exitosa se obtuvo el detalle de la promocion")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "Error interno en el servidor")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "promociondetalle")] HttpRequest req,
            ILogger log)
    {

        log.LogInformation("C# HTTP trigger function processed a request.");
        long aux;
        long? kIdElectronicaParametro = long.TryParse(req.Query["kIdElectronica"].ToString(), out aux) ? Convert.ToInt64(req.Query["kIdElectronica"]) : null;
        string? horaPromocionElectronica = req.Query["horaPromocionElectronica"].ToString();
        string? tipo = req.Query["tipo"].ToString();
        string? subTipo = req.Query["subTipo"].ToString();

        var consulta = new ObtieneLecturaPromocionDetalle
        {
            AsuntoNeunId = Convert.ToInt32(req.Query["AsuntoNeunId"]),
            UsuariId = Convert.ToInt32(req.Query["UsuariId"]),
            Origen = Convert.ToInt32(req.Query["Origen"]),
            NumeroOrden = Convert.ToInt32(req.Query["NumeroOrden"]),
            YearPromocion = Convert.ToInt32(req.Query["YearPromocion"]),
            Estado= Convert.ToInt32(req.Query["Estado"]),
            EsPromocionE= Convert.ToBoolean(req.Query["EsPromocionE"]),
            kIdElectronica = kIdElectronicaParametro,
            HoraPresentacion = horaPromocionElectronica,
            Tipo = tipo,
            SubTipo = subTipo,

            
        };
        return await _processor.ExecuteAsync<ObtieneLecturaPromocionDetalle, ListaDetallePromocionTablero<PromocionDetalleTableroDto, AnexoListaDto>>(consulta);

    }
}
