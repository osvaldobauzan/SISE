using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Comandos.ActualizarEstadoProyectoComando;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Comandos.SubirProyectoConAudienciaComando;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerProyectos;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ValidarExpediente;
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

namespace Proyectos.Functions.Funciones;

public class ProyectosFunction
{
    private readonly IHttpRequestProcessor _processor;

    public ProyectosFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }

    /// <summary>
    /// Función para obtener el listado de proyectos
    /// </summary>
    /// <param name="req"></param>
    /// <param name="log"></param>
    /// <returns></returns>
    [FunctionName("ProyectosFunction")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "Proyectos" }, Summary = " Obtiene los proyectos con los filtros especificados")]
    [OpenApiParameter(name: "fechaInicial", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Fecha inicial del rango de búsqueda, formato dd/MM/YYYY")]
    [OpenApiParameter(name: "fechaFinal", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Fecha final  del rango de búsqueda, formato dd/MM/YYYY")]
    [OpenApiParameter(name: "estatus", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "Estado de los proyectos (0 - indica todos)")]
    [OpenApiParameter(name: "ordenarPor", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "Campo por el que se ordena ")]
    [OpenApiParameter(name: "descendente", In = ParameterLocation.Query, Required = false, Type = typeof(bool), Description = "Indica si el ordenamiento es ascendente o descendente ")]
    [OpenApiParameter(name: "pagina", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Número de página")]
    [OpenApiParameter(name: "registrosPorPagina", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Número de registros por página")]
    [OpenApiParameter(name: "texto", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "Texto que se usa para filtrar")]

    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<ProyectoDto>), Description = "Respuesta exitosa, contiene una lista de objetos 'ProyectoDto' en formato JSON.")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "proyectos")] HttpRequest req, ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        var consulta = new ObtenerProyectosConFiltro()
        {
            FechaInicial = req.Query["fechaInicial"],
            FechaFinal = req.Query["fechaFinal"],
            Estado = Convert.ToInt32(req.Query["estatus"]),
            OrdenarPor = req.Query["ordenarPor"],
            Descendente = Convert.ToBoolean(req.Query["descendente"]),
            Pagina = Convert.ToInt32(req.Query["pagina"]),
            RegistrosPorPagina = Convert.ToInt32(req.Query["registrosPorPagina"]),
            Texto = req.Query["texto"]
        };

        return await _processor.ExecuteAsync<ObtenerProyectosConFiltro,
                                             ListaPaginada<ProyectoDto, MetaDataEstadoProyectoDto>>
                                             (consulta);
    }

    /// <summary>
    /// Función para registrar un proyecto
    /// </summary>
    /// <param name="req"></param>
    /// <param name="log"></param>
    /// <returns></returns>
    [FunctionName("SubirProyectoConAudiencia")]
    [OpenApiOperation(operationId: "Run2", tags: new[] { "SubirProyectoConAudiencia" }, Summary = " Operación para registrar un proyecto con su archivo")]
    [OpenApiRequestBody(contentType: "multipart/form-data", bodyType: typeof(ArchivoProyectoDto), Required = true, Description = "Parámetros requeridos para subir el documento del proyecto.")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Indica TRUE si se pudo subir el archivo; en caso contrario, indica FALSE ")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run2([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "proyecto/subirConAudiencia")] HttpRequest req, ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        var formCollection = await req.ReadFormAsync();

        var catOrganismoId = int.Parse(formCollection["catOrganismoId"]);
        long asuntoNeunId = long.Parse(formCollection["asuntoNeunId"]);
        var titularId = long.Parse(formCollection["titularId"]);
        var secretarioId = long.Parse(formCollection["secretarioId"]);
        var tipoSentenciaId = int.Parse(formCollection["tipoSentenciaId"]);
        var sintesis = formCollection["sintesis"];
        var motivosPartes = formCollection["motivosPartes"];

        var subirProyectoConAudienciaComando = new SubirProyectoConAudienciaComando()
        {
            CatOrganismoId = catOrganismoId,
            AsuntoNeunId = asuntoNeunId,
            TitularId = titularId,
            SecretarioId = secretarioId,
            TipoSentenciaId = tipoSentenciaId,
            Sintesis = sintesis
        };

        if (!string.IsNullOrEmpty(motivosPartes))
        {
            subirProyectoConAudienciaComando.Motivos = JsonConvert.DeserializeObject<List<MotivosParteDto>>(motivosPartes);
        }

        if (formCollection.Files.Any())
        {
            var file = formCollection.Files.FirstOrDefault();
            using var fileStream = file.OpenReadStream();
            byte[] bytes = new byte[file.Length];
            fileStream.Read(bytes, 0, (int)file.Length);
            subirProyectoConAudienciaComando.Archivo = bytes;
            subirProyectoConAudienciaComando.NombreArchivo = file.FileName;
        }

        return await _processor.ExecuteAsync<SubirProyectoConAudienciaComando, ProyectoConAudienciaDto>(subirProyectoConAudienciaComando);
    }

    /// <summary>
    /// Función para validar un expediente
    /// </summary>
    /// <param name="req"></param>
    /// <param name="log"></param>
    /// <returns></returns>
    [FunctionName("ValidarExpediente")]
    [OpenApiOperation(operationId: "Run3", tags: new[] { "ValidarExpediente" }, Summary = "Valida si un expediente es susceptible de agregar un proyecto")]
    [OpenApiParameter(name: "cuadernoId", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Identificador de cuaderno")]
    [OpenApiParameter(name: "numeroExpediente", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "Número de expediente")]
    [OpenApiParameter(name: "tipoAsuntoId", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "Identificador de tipo de asunto")]
    [OpenApiParameter(name: "asuntoNeunId", In = ParameterLocation.Query, Required = false, Type = typeof(long), Description = "Neun")]

    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<ValidacionExpedienteDto>), Description = "Respuesta exitosa, contiene el resultado de la validación en formato JSON.")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run3([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "proyecto/validarExpediente")] HttpRequest req, ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        int cuadernoId = int.Parse(req.Query["cuadernoId"]);
        string? numeroExpediente = req.Query["numeroExpediente"];

        int? tipoAsuntoId = null;
        if (!string.IsNullOrEmpty(req.Query["tipoAsuntoId"]))
        {
            tipoAsuntoId = int.Parse(req.Query["tipoAsuntoId"]);
        }

        long? asuntoNeunId = null;
        if (!string.IsNullOrEmpty(req.Query["asuntoNeunId"]))
        {
            asuntoNeunId = long.Parse(req.Query["asuntoNeunId"]);
        }

        var validarExpedienteConsulta = new ValidarExpedienteConsulta()
        {
            CatCuadernoId = cuadernoId,
            NumeroExpediente = numeroExpediente,
            CatTipoAsuntoId = tipoAsuntoId,
            AsuntoNeunId = asuntoNeunId,
        };

        return await _processor.ExecuteAsync<ValidarExpedienteConsulta, ValidacionExpedienteDto>(validarExpedienteConsulta);
    }

    /// <summary>
    /// Función para actualizar el estado de un proyecto
    /// </summary>
    /// <param name="req"></param>
    /// <param name="log"></param>
    /// <returns></returns>
    [FunctionName("ActualizarEstadoProyecto")]
    [OpenApiOperation(operationId: "Run4", tags: new[] { "ActualizarEstadoProyecto" }, Summary = "Operación para actualizar el estado de un proyecto")]
    [OpenApiRequestBody(contentType: "multipart/form-data", bodyType: typeof(ArchivoProyectoDto), Required = false, Description = "Parámetros requeridos para actualizar el estado de un proyecto.")]

    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Indica TRUE si se pudo actualizar el estado; en caso contrario, indica FALSE ")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run4([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "proyecto/validar")] HttpRequest req, ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        var formCollection = await req.ReadFormAsync();

        var catOrganismoId = int.Parse(formCollection["catOrganismoId"]);
        long asuntoNeunId = long.Parse(formCollection["asuntoNeunId"]);
        var proyectoId = long.Parse(formCollection["proyectoId"]);
        var estadoId = int.Parse(formCollection["estadoId"]);
        var correcciones = formCollection["Correcciones"];

        var actualizarEstadoProyectoComando = new ActualizarEstadoProyectoComando()
        {
            CatOrganismoId = catOrganismoId,
            AsuntoNeunId = asuntoNeunId,
            ProyectoId = proyectoId,
            EstadoId = estadoId,
            Correcciones = correcciones
        };

        if (formCollection.Files.Any())
        {
            var file = formCollection.Files.FirstOrDefault();
            using var fileStream = file.OpenReadStream();
            byte[] bytes = new byte[file.Length];
            fileStream.Read(bytes, 0, (int)file.Length);
            actualizarEstadoProyectoComando.ArchivoCorreciones = bytes;
            actualizarEstadoProyectoComando.NombreArchivoCorrecciones = file.FileName;
        }

        return await _processor.ExecuteAsync<ActualizarEstadoProyectoComando, EstadoProyectoActualizadoDto>(actualizarEstadoProyectoComando);
    }
}
