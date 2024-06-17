using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.OficialAdministrativo.Consulta;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.OficialAdministrativo.Consulta;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using Common.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Catalogos.Functions.Funciones;

/// <summary>
/// Function para obtener el catálogo de oficiales.
/// </summary>
public class CatalogoOficialesFunction
{
    private readonly IHttpRequestProcessor _processor;

    /// <summary>
    /// Constructor de la clase.
    /// </summary>
    /// <param name="processor">Procesador de solicitudes HTTP.</param>
    public CatalogoOficialesFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }

    /// <summary>
    /// Function para obtener el catálogo de oficiales.
    /// </summary>
    /// <param name="req">Solicitud HTTP.</param>
    /// <param name="log">Logger para registrar información.</param>
    /// <returns>Resultado de la solicitud HTTP.</returns>
    [FunctionName("CatalogoOficiales")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "CatalogoOficiales" }, Summary = "Obtiene el catálogo de oficiales por organismo y cargo")]
    [OpenApiParameter(name: "cargoId", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "Representa el identificador del cargo. Valor por defecto es 17.")]
    [OpenApiParameter(name: "organismoId", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Representa el identificador del organismo")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<CatalogoOficialDto>), Description = "Catálogo de oficiales obtenido con éxito")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "oficiales")] HttpRequest req, ILogger log)
    {
        log.LogInformation("Procesando solicitud para obtener el catálogo de oficiales.");

        if (!int.TryParse(req.Query["cargoId"], out int cargoId))
        {
            cargoId = 0; // Valor por defecto si no se proporciona
        }

        if (!int.TryParse(req.Query["cargoId"], out int organismoId))
        {
            organismoId = 0; // Valor por defecto si no se proporciona
        }


        var consulta = new ObtieneCatalogoOficiales
        {
            CargoId = cargoId,
            CatOrganismoId = organismoId
        };

        return await _processor.ExecuteAsync<ObtieneCatalogoOficiales, List<CatalogoOficialDto>>(consulta);
    }
}
