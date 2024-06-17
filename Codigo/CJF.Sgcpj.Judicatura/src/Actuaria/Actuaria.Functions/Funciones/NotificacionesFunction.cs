using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.AgregarActuarioMasivo;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.SubirAcuse;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.DetalleNotificaciones;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.NotificacionDetalle;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Comandos.SubirAcuerdoComando;
using Common.Functions;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SixLabors.ImageSharp.Drawing;

namespace Actuaria.Functions.Funciones;
public class NotificacionesFunction
{
    private readonly IHttpRequestProcessor _processor;

    public NotificacionesFunction(IHttpRequestProcessor processor)
    {
        _processor = processor;
    }


    [FunctionName("NotificacionDetalleFunction")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "NotificacionesDetalle" }, Summary = " Obtiene la información para el tablero de notificaciones")]
    [OpenApiParameter(name: "tamanioPagina", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Tamaño de página")]
    [OpenApiParameter(name: "numeroPagina", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Número de página")]
    [OpenApiParameter(name: "asuntoNeunId", In = ParameterLocation.Query, Required = true, Type = typeof(long), Description = "AsuntoNeunId")]
    [OpenApiParameter(name: "asuntoDocumentoID", In = ParameterLocation.Query, Required = true, Type = typeof(long), Description = "asuntoDocumentoId")]
    [OpenApiParameter(name: "texto", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "texto para filtrar resultados ")]
    [OpenApiParameter(name: "ordenarPor", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "Campo por el que se ordenará el dataset")]
    [OpenApiParameter(name: "tipoOrden", In = ParameterLocation.Query, Required = false, Type = typeof(bool?), Description = "Tipo de órden ascendente o descendente")]
    [OpenApiParameter(name: "filtroTipo", In = ParameterLocation.Query, Required = false, Type = typeof(int?), Description = "Tipo de filtro")]
    [OpenApiParameter(name: "filtroTipoParteID", In = ParameterLocation.Query, Required = false, Type = typeof(int?), Description = "Id del tipo parte")]
    [OpenApiParameter(name: "filtroTipoNotificacionID", In = ParameterLocation.Query, Required = false, Type = typeof(int?), Description = "ID del tipo de notificación")]
    [OpenApiParameter(name: "filtroActuarioID", In = ParameterLocation.Query, Required = false, Type = typeof(int?), Description = "ID del actuario")]
    [OpenApiParameter(name: "primeraCarga", In = ParameterLocation.Query, Required = false, Type = typeof(bool), Description = "Carga de notificaciones")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ListaPaginada<NotificacionDetalleDto, NotificacionDetalleMetaDataEstadosDto>), Description = "Respuesta exitosa, contiene la información para el tablero de notificaciones")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]

    public async Task<IActionResult> Run(
      [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "actuaria/notificaciones")] HttpRequest req,
      ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        var aux = 0;
        var auxl = 0l;
        bool tipoOrden = Convert.ToBoolean(req.Query["tipoOrden"]);
        int? filtroTipo = int.TryParse(req.Query["filtroTipo"].ToString(), out aux) ? Convert.ToInt32(req.Query["filtroTipo"]) : null;
        int? filtroTipoParteID = int.TryParse(req.Query["filtroTipoParteID"].ToString(), out aux) ? Convert.ToInt32(req.Query["filtroTipoParteID"]) : null;
        int? filtroTipoNotificacionID = int.TryParse(req.Query["filtroTipoNotificacionID"].ToString(), out aux) ? Convert.ToInt32(req.Query["filtroTipoNotificacionID"]) : null;
        int? filtroActuarioID = int.TryParse(req.Query["filtroActuarioID"].ToString(), out aux) ? Convert.ToInt32(req.Query["filtroActuarioID"]) : null;

        log.LogInformation($"{tipoOrden}, {filtroTipo}, {filtroTipoParteID}, {filtroTipoNotificacionID}, {filtroActuarioID}, " +
                           $"{req.Query["TamanioPagina"]}, {req.Query["NumeroPagina"]}, {req.Query["AsuntoNeunId"]}, " +
                           $"{req.Query["AsuntoDocumentoID"]}, {req.Query["texto"]}, {req.Query["ordenarPor"]}");

        var notificaciones = new NotificacionDetalleConsulta()

        {
            TamanioPagina = Convert.ToInt32(req.Query["TamanioPagina"]),
            NumeroPagina = Convert.ToInt32(req.Query["NumeroPagina"]),
            AsuntoNeunId = Convert.ToInt64(req.Query["AsuntoNeunId"]),
            AsuntoDocumentoID = Convert.ToInt64(req.Query["AsuntoDocumentoID"]),
            Texto = req.Query["texto"],
            OrdenarPor = req.Query["ordenarPor"],
            TipoOrden = tipoOrden,
            FiltroTipo = filtroTipo,
            FiltroTipoParteID = filtroTipoParteID,
            FiltroTipoNotificacionID = filtroTipoNotificacionID,
            FiltroActuarioID = filtroActuarioID,
            PrimeraCarga = Convert.ToBoolean(req.Query["primeraCarga"]),
        };
        return await _processor.ExecuteAsync<NotificacionDetalleConsulta, ListaPaginada<NotificacionDetalleDto, NotificacionDetalleMetaDataEstadosDto>>(notificaciones);
    }

    [FunctionName("NotificacionAcuse")]
    [OpenApiOperation(operationId: "Run2", tags: new[] { "SuirAcuse" }, Summary = "Registra el acuse")]
    [OpenApiRequestBody(contentType: "multipart/form-data", bodyType: typeof(SubirAcuseDto), Required = true, Description = "Parámetros requeridos para subir el documento del acuse.")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Respuesta exitosa, contiene la información para el tablero de notificaciones")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]

    public async Task<IActionResult> Run2(
      [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "actuaria/notificaciones/acuse")] HttpRequest req,
      ILogger log)
    {
        var formCollection = await req.ReadFormAsync();

        SubirAcuseComando subirAcuseComando = new SubirAcuseComando();
        int aux;
        int? tipoNotificacion =  int.TryParse(formCollection["tipoNotificacion"], out aux) ? aux : (int?)null;


        SubirAcuseDto subirAcuerdoDto = new SubirAcuseDto()
        {
            AsuntoNeunId = long.Parse(formCollection["asuntoNeunId"]),
            SintesisCitatorio = formCollection["sintesisCitatorio"],
            TipoAcuse = int.Parse(formCollection["tipoAcuse"]),
            SintesisOrden = int.Parse(formCollection["sintesisOrden"]),
            FechaNotificacion = formCollection["fechaNotificacion"],
            FechaNotificacionCitatorio = formCollection["fechaNotificacionCitatorio"],
            PersonaId = long.Parse(formCollection["personaId"]),
            TipoNotificacion = tipoNotificacion
        };

        subirAcuseComando.AcuseDto = subirAcuerdoDto;

        if (formCollection.Files.Count > 0)
        {
            foreach (var file in formCollection.Files)
            {
                using var fileStream = file.OpenReadStream();
                byte[] bytes = new byte[file.Length];
                fileStream.Read(bytes, 0, (int)file.Length);
                subirAcuerdoDto.Archivo = bytes;
                subirAcuerdoDto.NombreArchivo = file.FileName;
            }


        }
        return await _processor.ExecuteAsync<SubirAcuseComando, bool>(subirAcuseComando);
    }

    [FunctionName("NotificacionAcuseMasivo")]
    [OpenApiOperation(operationId: "Run3", tags: new[] { "SuirAcuseMasivo" }, Summary = "Registra el acuse a varias partes")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.NoContent, contentType: "application/json", bodyType: typeof(bool), Description = "Indica TRUE si se pudo agregar a un actuario; en caso contrario, indica FALSE")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
    [OpenApiRequestBody(contentType: "json", bodyType: typeof(AgregarAcuseMasivoM), Description = "Objeto que se usa para agregar a un actuario")]
    [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
    public async Task<IActionResult> Run3(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "actuaria/notificaciones/acusemultiple")] HttpRequest req,
            ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");


        var formCollection = await req.ReadFormAsync();

        var subirAcuseComando = new SubirAcuseMasivoComando();
        int aux;
        int? tipoNotificacion = int.TryParse(formCollection["tipoNotificacion"], out aux) ? aux : (int?)null;

        var ParteNotificacionAcuse = formCollection["ParteNotificacionAcuse"];

        var fechasNotificacion = JsonConvert.DeserializeObject<List<ParteNotificacionAcuse>>(ParteNotificacionAcuse);

        var subirAcuseMasivoDto = new SubirAcuseMasivoDto()
        {
            AsuntoNeunId = long.Parse(formCollection["asuntoNeunId"]),
            SintesisCitatorio = formCollection["sintesisCitatorio"],
            TipoAcuse = int.Parse(formCollection["tipoAcuse"]),
            SintesisOrden = int.Parse(formCollection["sintesisOrden"]),
            FechaNotificacionCitatorio = formCollection["fechaNotificacionCitatorio"],
            TipoNotificacion = tipoNotificacion,
            PartesNotificacionesAcuse = fechasNotificacion,
            ArchivosAcuse = new List<ArchivosAcuseDto>()
        };
        subirAcuseComando.AcusePartesDto = subirAcuseMasivoDto;

        
        if (formCollection.Files.Count > 0)
        {
            foreach (var file in formCollection.Files)
            {
                var archivosAcuse = new ArchivosAcuseDto();
                using var fileStream = file.OpenReadStream();
                byte[] bytes = new byte[file.Length];
                fileStream.Read(bytes, 0, (int)file.Length);
                archivosAcuse.Archivo = bytes;
                archivosAcuse.NombreArchivo = file.FileName;
                subirAcuseMasivoDto.ArchivosAcuse.Add(archivosAcuse);
            }
        }


        return await _processor.ExecuteAsync<SubirAcuseMasivoComando, List<bool>>(subirAcuseComando);

    }
}
