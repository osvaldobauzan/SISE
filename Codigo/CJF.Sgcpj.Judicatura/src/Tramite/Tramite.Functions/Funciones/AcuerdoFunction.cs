using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
//using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Request;
using CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Comandos.SubirAcuerdoComando;
using CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerArchivos;
using CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerDetalleTramite;
using CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtieneExpediente;
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

namespace Tramite.Functions.Funciones
{
    public class AcuerdoFunction
    {
        private readonly IHttpRequestProcessor _processor;
        public AcuerdoFunction(IHttpRequestProcessor processor)
        {
            _processor = processor;
        }

        [FunctionName("ObtenerBase64")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "ObtenerBase64" }, Summary = " Obtiene los archivos en formato word-base64")]
        [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "id del documento")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(DocumentoBase64Dto), Description = "Respuesta exitosa se obtiene los expediente en formato word-base64 ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]

        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tramite/documento")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"ruta: {req.Query["id"]}");
            log.LogInformation($"descarga: {req.Query["esDescarga"]}");
            var consulta = new ObtenerDocumentoConsulta()
            {
                Id = req.Query["id"],
                esDescarga = req.Query["esDescarga"]
            };
            return await _processor.ExecuteAsync<ObtenerDocumentoConsulta, DocumentoBase64Dto>(consulta);
        }

        [FunctionName("ObtenerArchivoAcuerdo")]
        [OpenApiOperation(operationId: "Run2", tags: new[] { "ObtenerArchivoAcuerdo" }, Summary = "Obtiene el nombre del acuerdo y su path")]
        [OpenApiParameter(name: "asuntoNeunId", In = ParameterLocation.Query, Required = true, Type = typeof(long), Description = "AsuntoNeunId  de la promoción")]
        [OpenApiParameter(name: "anioPromocion", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Año de la promoción")]
        [OpenApiParameter(name: "numeroOrden", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Numero de orden de la promoción")]
        [OpenApiParameter(name: "tipoModulo", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Modulo actual - tramite = 2")]
        [OpenApiParameter(name: "asuntoDocumentoId", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Asunto documento Id del acuerdo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ArchivosPromocionDto), Description = "Respuesta exitosa, se obtiene la lista de los acuerdo ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run2(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tramite/acuerdo")] HttpRequest req,
        ILogger log)
        {

            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"AsuntoNeunId: {req.Query["AsuntoNeunId"]}");
            var consulta = new ObtenerArchivosConsulta()
            {
                AsuntoNeunId = Convert.ToInt64(req.Query["asuntoNeunId"]),
                YearPromocion = Convert.ToInt32(req.Query["anioPromocion"]),
                NumeroOrden = Convert.ToInt32(req.Query["numeroOrden"]),
                TipoModulo = Convert.ToInt32(req.Query["tipoModulo"]),
                AsuntoDocumentoId = Convert.ToInt32(req.Query["asuntoDocumentoId"]),
            };
            return await _processor.ExecuteAsync<ObtenerArchivosConsulta, ArchivosPromocionDto>(consulta);
        }
        /// <summary>
        /// Sube los acuerdos
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("SubirAcuerdoFunction")]
        [OpenApiOperation(operationId: "Run3", tags: new[] { "SubirAcuerdo" }, Summary = " Sube los acuerdos")]
        [OpenApiRequestBody(contentType: "multipart/form-data", bodyType: typeof(AcuerdoArchivoDto), Required = true, Description = "Parámetros requeridos para subir el documento de trámite de acuerdo.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ResultadoAcuerdoDto), Description = "Indica TRUE si se pudo subir el acuerdo; en caso contrario, indica FALSE ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run3(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "tramite/acuerdo")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var aux = 0;

            var formCollection = await req.ReadFormAsync();
            long asuntoNeunId = long.Parse(formCollection["asuntoNeunId"]);
            int? asuntoDocumentoIdOficio = int.TryParse(formCollection["asuntoDocumentoIdOficio"], out aux) ? aux : (int?)null;
            short contenido = short.Parse(formCollection["contenido"]);
            short tipoCuaderno = short.Parse(formCollection["tipoCuaderno"]);
            string fechaAuto = formCollection["fechaAcuerdo"];
            int? sintesisOrden = int.TryParse(formCollection["sintesisOrden"], out aux) ? aux : (int?)null;
            string catTipoAsunto = formCollection["catTipoAsunto"];
            string asuntoAlias = formCollection["asuntoAlias"];
            string mesa = formCollection["mesa"];
            var promociones = formCollection["promociones"];
            var promocionesAcuerdoAutoridad = formCollection["promocionesAutoridad"];
            var promocionesAcuerdoParte = formCollection["promocionesParte"];
            var promocionesPromoventes = formCollection["promocionesPromoventes"];

            string nombreArchivo = "pruebaSintesis2023API"/*Convert.ToString(formCollection["sintesis"])*/;
            string IPUsuario = "192.168.0.0"/*Convert.ToString(formCollection["sintesis"])*/;
            List<PromocionAcuerdoDto>? listaPromocionAcuerdos = null;
            List<PromocionAcuerdoAutoridadDto>? listaPromocionAcuerdosAutoridad = null;
            List<PromocionAcuerdoPersonasDto>? listaPromocionAcuerdosParte = null;
            List<PromocionAcuerdoPersonasDto>? listaPromoventeAcuerdosParte = null;

            if (!string.IsNullOrEmpty(promociones))
                listaPromocionAcuerdos = JsonConvert.DeserializeObject<List<PromocionAcuerdoDto>>(promociones);

            if (!string.IsNullOrEmpty(promocionesAcuerdoAutoridad))
                listaPromocionAcuerdosAutoridad = JsonConvert.DeserializeObject<List<PromocionAcuerdoAutoridadDto>>(promocionesAcuerdoAutoridad);

            if (!string.IsNullOrEmpty(promocionesAcuerdoParte))
                listaPromocionAcuerdosParte = JsonConvert.DeserializeObject<List<PromocionAcuerdoPersonasDto>>(promocionesAcuerdoParte);

            if (listaPromocionAcuerdosParte == null)
            {
                listaPromocionAcuerdosParte = JsonConvert.DeserializeObject<List<PromocionAcuerdoPersonasDto>>(promocionesPromoventes);
            }
            else if(!string.IsNullOrEmpty(promocionesPromoventes))
            {
                listaPromoventeAcuerdosParte = JsonConvert.DeserializeObject<List<PromocionAcuerdoPersonasDto>>(promocionesPromoventes);
                if(listaPromoventeAcuerdosParte != null)
                {
                    foreach (var promovente in listaPromoventeAcuerdosParte)
                    {
                        listaPromocionAcuerdosParte.Add(promovente);
                    }
                }
            }
            long auxLong = 0;
            long? idAgenda = long.TryParse(formCollection["idAgenda"], out auxLong) ? auxLong : (long?)null;
            int? idResultado = int.TryParse(formCollection["idResultado"], out aux) ? aux : (int?)null;

            var subirAcuerdoComando = new SubirAcuerdoComando()
            {
                AsuntoNeunId = asuntoNeunId,
                AsuntoDocumentoIdOficio = asuntoDocumentoIdOficio,
                SintesisOrden = sintesisOrden,
                NombreArchivo = nombreArchivo,
                Contenido = contenido,
                TipoCuaderno = tipoCuaderno,
                FechaAcuerdo = fechaAuto,
                PromocionesDeterminacion = listaPromocionAcuerdos,
                AutoridadAsunto = listaPromocionAcuerdosAutoridad,
                PersonasNotificacionIndividual = listaPromocionAcuerdosParte,
                AsuntoAlias = asuntoAlias,
                CatTipoAsunto = catTipoAsunto,
                Mesa = mesa,
                ResultadoId = idResultado,
                AgendaId = idAgenda
            };

            subirAcuerdoComando.Archivos = new List<SubirArchivoDto>();

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

                    subirAcuerdoComando.Archivos.Add(archivo);
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
                subirAcuerdoComando.Archivos.Add(archivo);
            }

            return await _processor.ExecuteAsync<SubirAcuerdoComando, List<string>>(subirAcuerdoComando);
        }

        /// <summary>
        /// Sube los acuerdos
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("ActualizaAcuerdoFunction")]
        [OpenApiOperation(operationId: "Run4", tags: new[] { "EditarAcuerdo" }, Summary = " Actualiza los acuerdo")]
        [OpenApiRequestBody(contentType: "multipart/form-data", bodyType: typeof(AcuerdoArchivoDto), Required = true, Description = "Parámetros requeridos para subir el documento de trámite de acuerdo.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ResultadoAcuerdoDto), Description = "Indica TRUE si se pudo subir el acuerdo; en caso contrario, indica FALSE ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResultadoGenericoDto), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> Run4(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "tramite/acuerdo")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var aux = 0;

            var formCollection = await req.ReadFormAsync();

            long asuntoNeunId = long.Parse(formCollection["asuntoNeunId"]);
            short contenido = short.Parse(formCollection["contenido"]);
            short tipoCuaderno = short.Parse(formCollection["tipoCuaderno"]);
            string fechaAuto = formCollection["fechaAcuerdo"];
            int? sintesisOrden = int.TryParse(formCollection["sintesisOrden"], out aux) ? aux : (int?)null;
            int? asuntoDocumentoId = int.TryParse(formCollection["asuntoDocumentoId"], out aux) ? aux : (int?)null;
            string catTipoAsunto = formCollection["catTipoAsunto"];
            string asuntoAlias = formCollection["asuntoAlias"];
            string mesa = formCollection["mesa"];
            var promociones = formCollection["promociones"];
            var promocionesAcuerdoAutoridad = formCollection["promocionesAutoridad"];
            var promocionesAcuerdoParte = formCollection["promocionesParte"];
            var promocionesPromoventes = formCollection["promocionesPromoventes"];


            string nombreArchivo = "pruebaSintesis2023API"/*Convert.ToString(formCollection["sintesis"])*/;
            string IPUsuario = "192.168.0.0"/*Convert.ToString(formCollection["sintesis"])*/;
            List<PromocionAcuerdoDto>? listaPromocionAcuerdos = null;
            List<PromocionAcuerdoAutoridadDto>? listaPromocionAcuerdosAutoridad = null;
            List<PromocionAcuerdoPersonasDto>? listaPromocionAcuerdosParte = null;
            List<PromocionAcuerdoPersonasDto>? listaPromocionAcuerdosPromovente = null;


            if (!string.IsNullOrEmpty(promociones))
                listaPromocionAcuerdos = JsonConvert.DeserializeObject<List<PromocionAcuerdoDto>>(promociones);

            if (!string.IsNullOrEmpty(promocionesAcuerdoAutoridad))
                listaPromocionAcuerdosAutoridad = JsonConvert.DeserializeObject<List<PromocionAcuerdoAutoridadDto>>(promocionesAcuerdoAutoridad);
          
            if (!string.IsNullOrEmpty(promocionesAcuerdoParte))
                listaPromocionAcuerdosParte = JsonConvert.DeserializeObject<List<PromocionAcuerdoPersonasDto>>(promocionesAcuerdoParte);

            if (!string.IsNullOrEmpty(promocionesPromoventes))
                listaPromocionAcuerdosPromovente = JsonConvert.DeserializeObject<List<PromocionAcuerdoPersonasDto>>(promocionesPromoventes);

            if (listaPromocionAcuerdosPromovente != null)
            {
                if(listaPromocionAcuerdosParte == null)
                {
                    listaPromocionAcuerdosParte = listaPromocionAcuerdosPromovente;
                }
                else
                {
                    foreach (var promovente in listaPromocionAcuerdosPromovente)
                    {
                        listaPromocionAcuerdosParte.Add(promovente);
                    }
                }
            }

            long auxLong = 0;
            long? idAgenda = long.TryParse(formCollection["idAgenda"], out auxLong) ? auxLong : (long?)null;
            int? idResultado = int.TryParse(formCollection["idResultado"], out aux) ? aux : (int?)null;

            var subirAcuerdoComando = new SubirAcuerdoComando()
            {
                AsuntoNeunId = asuntoNeunId,
                SintesisOrden = sintesisOrden,
                NombreArchivo = nombreArchivo,
                Contenido = contenido,
                TipoCuaderno = tipoCuaderno,
                FechaAcuerdo = fechaAuto,
                PromocionesDeterminacion = listaPromocionAcuerdos,
                AutoridadAsunto = listaPromocionAcuerdosAutoridad,
                PersonasNotificacionIndividual = listaPromocionAcuerdosParte,
                AsuntoDocumentoId = asuntoDocumentoId,
                AsuntoAlias = asuntoAlias,
                CatTipoAsunto = catTipoAsunto,
                Mesa = mesa,
                ResultadoId = idResultado,
                AgendaId = idAgenda
            };

            subirAcuerdoComando.Archivos = new List<SubirArchivoDto>();

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

                    subirAcuerdoComando.Archivos.Add(archivo);
                    index++;
                }
            }
            else
            {
                var archivo = new SubirArchivoDto
                {
                    NombreArchivo = string.Empty,
                    Clase = 0,
                    Descripcion = 1,
                    Caracter = 0
                };
                subirAcuerdoComando.Archivos.Add(archivo);
            }

            return await _processor.ExecuteAsync<SubirAcuerdoComando, List<string>>(subirAcuerdoComando);
        }

        [FunctionName("ObtenerAcuerdoFunction")]
        [OpenApiOperation(operationId: "ObtenerAcuerdo", tags: new[] { "ObtenerAcuerdo" }, Summary = "Obtiene la información de un acuerdo existente.")]
        [OpenApiParameter(name: "idCatOrganismo", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Identificador del organismo")]
        [OpenApiParameter(name: "idAsuntoNeun", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Identificador del asunto Neun")]
        [OpenApiParameter(name: "ordenSintesis", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Orden sintesís")]
        [OpenApiParameter(name: "asuntoDocumentoId", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Asunto documento Id del acuerdo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "Respuesta exitosa, se obtiene la lista de los acuerdo ")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "La solicitud contiene datos inválidos o incompletos")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Description = "Error interno en el servidor")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Unauthorized, contentType: "text/plain", bodyType: typeof(string), Description = "No autorizado.")]
        [OpenApiSecurity("Bearer", SecuritySchemeType.Http, Name = "authorization", Scheme = OpenApiSecuritySchemeType.Bearer, In = OpenApiSecurityLocationType.Header, BearerFormat = "JWT")]
        public async Task<IActionResult> GetObtenerAcuerdo([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tramite/ObtenerAcuerdo")] HttpRequest req,
          ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation($"{req.Query["idAsuntoNeun"]} {req.Query["ordenSintesis"]} {req.Query["asuntoDocumentoId"]}");

            var consulta = new ObtieneDetalleTramiteConsulta()
            {
                asuntoNeunId = Convert.ToInt32(req.Query["idAsuntoNeun"]),
                sintesisOrden = Convert.ToInt32(req.Query["ordenSintesis"]),
                asuntoDocumentoId = Convert.ToInt32(req.Query["asuntoDocumentoId"])
            };

            return await _processor.ExecuteAsync<ObtieneDetalleTramiteConsulta,
                DetalleTramiteDto<CabeceraTramiteDto, PromocionesDto, PartesDto, OficioDto>>(consulta);
        }       
    }
}
