using System.Text;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Tramite.Application.Procesos;
using CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Comandos.CreacionOficiosComando;
using CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerArchivos;
using DocumentFormat.OpenXml.Office.CustomUI;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Comandos.SubirAcuerdoComando;
public class SubirAcuerdoComando : IRequest<List<string>>
{
    public List<SubirArchivoDto> Archivos;
    public long AsuntoNeunId { get; set; }
    public string NombreDocumento { get; set; }
    public string NombreArchivo { get; set; }
    public string ExtensionDocumento { get; set; }
    public short Contenido { get; set; }
    public short TipoCuaderno { get; set; }
    public string FechaAcuerdo { get; set; }
    public int EmpleadoId { get; set; }
    public int CatIdOrganismo { get; set; }
    public int? SintesisOrden { get; set; }
    public int? AsuntoDocumentoId { get; set; }
    public int? AsuntoDocumentoIdOficio { get; set; }
    public string CatTipoAsunto { get; set; }
    public string AsuntoAlias { get; set; }
    public string Mesa { get; set; }
    public List<PromocionAcuerdoDto> PromocionesDeterminacion { get; set; }
    public List<PromocionAcuerdoPersonasDto>? PersonasNotificacionIndividual { get; set; }
    public List<PromocionAcuerdoAutoridadDto>? AutoridadAsunto { get; set; }
    public long? AgendaId { get; set; }
    public int? ResultadoId { get; set; }
}
public class SubirAcuerdoComandoHandler : IRequestHandler<SubirAcuerdoComando, List<string>>
{
    private readonly INasArchivo _nas;
    private readonly ITramitesRepository _tramitesRepository;
    private readonly ISesionService _sesionService;
    private readonly IAcuerdosDocxsHelpers _acuerdoDocumentoFunciones;
    private readonly ILogger<SubirAcuerdoComandoHandler> _logger;
    private readonly IConfiguration _configuration;
    private readonly IProcessQueueService _processQueueService;
    private readonly IDocumentoBlob _documentoBlob;
    private readonly IWordContenido _wordContenido;
    private readonly IMapper _mapper;

    private string Modulo = "Tramite";
    private readonly ISanitizerService _sanitizerService;

    public SubirAcuerdoComandoHandler(INasArchivo nas, ISesionService sesionService, ITramitesRepository tramitesRepository,
        IAcuerdosDocxsHelpers acuerdoDocumentoFunciones, IMapper mapper, ILogger<SubirAcuerdoComandoHandler> logger,
        ISanitizerService sanitizerService, IConfiguration configuration, IProcessQueueService processQueueService,
        IDocumentoBlob documentoBlob, IWordContenido wordContenido)
    {
        _nas = nas;
        _tramitesRepository = tramitesRepository;
        _mapper = mapper;
        _logger = logger;
        _configuration = configuration;
        _processQueueService = processQueueService;
        _documentoBlob = documentoBlob;
        _wordContenido = wordContenido;
        _sesionService = sesionService;
        _sanitizerService = sanitizerService;
        _acuerdoDocumentoFunciones = acuerdoDocumentoFunciones;
    }
    public async Task<List<string>> Handle(SubirAcuerdoComando request, CancellationToken cancellationToken)
    {
        List<string> resultado = new List<string>();
        List<string> archivosNoGuardados = new List<string>();
        var rutaNas = string.Empty;
        var rutaNasAcuerdo = string.Empty;
        var fechaAutoTramite = _mapper.Map<AgregarDocumento>(request);
        var datosDocumento = new DatosDocumento();

        request.CatIdOrganismo = _sesionService.SesionActual.CatOrganismoId;
        ResultadoAcuerdoDto resultadoArchivo = null;
        var acuerdoDocx = new byte[0];
        var agregarDocumento = new AgregarDocumento();
        var htmlAcuerdo = "";
        var ruta = await _tramitesRepository.RutaArchivo(Modulo);
        rutaNas = ruta.First().RutaNas;
        foreach (var archivo in request.Archivos)
        {
            resultadoArchivo = new ResultadoAcuerdoDto
            {
                NombreArchivo = archivo.NombreArchivo.Split('.').First(),
                ExtensionDocumento = "." + archivo.NombreArchivo.Split('.').Last()
            };
            if (string.IsNullOrEmpty(resultadoArchivo.NombreArchivo))
            {
                resultadoArchivo.ExtensionDocumento = null;
            }
            try
            {
                agregarDocumento = new AgregarDocumento()
                {
                    AsuntoNeunId = request.AsuntoNeunId,
                    NombreDocumento = resultadoArchivo.NombreArchivo,
                    NombreArchivo = request.NombreArchivo,
                    ExtensionDocumento = resultadoArchivo.ExtensionDocumento,
                    Caracter = archivo.Caracter,
                    Clase = archivo.Clase,
                    Descripcion = archivo.Descripcion,
                    FechaAcuerdo = fechaAutoTramite.FechaAcuerdo,
                    Contenido = request.Contenido,
                    TipoCuaderno = request.TipoCuaderno,
                    SintesisOrden = request.SintesisOrden,
                    PromocionesDeterminacion = request.PromocionesDeterminacion,
                    PersonasNotificacionIndividual = request.PersonasNotificacionIndividual,
                    AutoridadAsunto = request.AutoridadAsunto,
                    AsuntoDocumentoId = request.AsuntoDocumentoId,
                    UsuarioCaptura = _sesionService.SesionActual.EmpleadoId,
                    AgendaId = request.AgendaId,
                    ResultadoId = request.ResultadoId,
                };
                if (!agregarDocumento.PromocionesDeterminacion.IsNullOrEmpty())
                {
                    foreach (var promocionDeterminacion in agregarDocumento.PromocionesDeterminacion)
                    {
                        if (promocionDeterminacion.EstadoPromocion == null && promocionDeterminacion.YearPromocion == null)
                        {
                            promocionDeterminacion.EstadoPromocion = 4;
                            promocionDeterminacion.YearPromocion = agregarDocumento.FechaAcuerdo.Year;
                        }
                    }
                }

                if (agregarDocumento.AutoridadAsunto != null)
                {

                    foreach (var t in agregarDocumento.AutoridadAsunto)
                    {
                        PromocionAcuerdoPersonasDto itm = new PromocionAcuerdoPersonasDto();
                        itm.PersonaId = t.AnexoParteId;
                        itm.TipoNotificacionId = t.TipoAnexoId;

                        itm.DescripcionPromovente = t.AnexoParteDescripcion;
                        itm.NombreParte = t.NombreAutoridad;
                        itm.TextoOficioLibre = t.TextoOficioLibre;

                        t.TextoOficioLibre = _sanitizerService.SanitizeHtml(t.TextoOficioLibre);

                        agregarDocumento.PersonasNotificacionIndividual.Add(itm);
                    }
                }

                if (agregarDocumento.PersonasNotificacionIndividual != null)
                {
                    foreach (var it in agregarDocumento.PersonasNotificacionIndividual)
                    {
                        if (it.TipoNotificacionId == 11)
                        {
                            it.TipoAnexoId = 6;
                        }
                        else if (it.TipoNotificacionId == 5)
                        {
                            it.TipoAnexoId = 1;
                        }
                    }
                }


                datosDocumento = await _tramitesRepository.GuardarDocumentoAcuerdo(agregarDocumento);

                if (archivo.Data != null)
                {
                    acuerdoDocx = archivo.Data;
                    htmlAcuerdo = _wordContenido.ReadDocumentWord(archivo.Data);

                    string strQRCode = QrAcuerdo(datosDocumento, agregarDocumento);

                    archivo.Data = _acuerdoDocumentoFunciones.InsertarQrLateral(archivo.Data, strQRCode, 100);

                    rutaNasAcuerdo = rutaNas + "\\" + request.CatIdOrganismo.ToString() + "\\" +
                        datosDocumento.NombreArchivo + resultadoArchivo.ExtensionDocumento;

                    _nas.AlmacenarArchivo(rutaNasAcuerdo, archivo.Data);
                    resultado.Add(archivo.NombreArchivo);
                    request.NombreArchivo = datosDocumento.NombreArchivo;
                }
            }
            catch (Exception ex)
            {
                archivosNoGuardados.Add(archivo.NombreArchivo);
                _logger.LogError("No se pudo almacenar archivo(s) " + ex.Message);

                throw;
            }
        }

        if (agregarDocumento.PersonasNotificacionIndividual != null && request.Archivos != null && request.Archivos.Any())
        {
            var uri = _configuration["SISE3:BackEnd:SMTPTemplatesUrl"];
            var contenedorOficiosGenerados = _configuration["SISE3:BackEnd:OficiosContenedor"];

            var asuntos = new List<AutoridadAsunto>();
            var listaTextoOficioLibre = new List<TextoOficioLibreDto>();
            var textoLibreId = $"Oficio-Libre-{Guid.NewGuid()}";
            string asuntoDocumentoIdOficio;
            if (request.AsuntoDocumentoIdOficio == null)
            {
                asuntoDocumentoIdOficio = datosDocumento.AsuntoDocumentoId.ToString();
            }
            else
            {
                asuntoDocumentoIdOficio = request.AsuntoDocumentoIdOficio.ToString();
                if (request.AsuntoDocumentoIdOficio == 0 && asuntoDocumentoIdOficio == "0")
                    asuntoDocumentoIdOficio = datosDocumento.AsuntoDocumentoId.ToString();
            }
            int.TryParse(asuntoDocumentoIdOficio, out int _resultadoAsuntoDocumento);
            if (_resultadoAsuntoDocumento == 0)
                _resultadoAsuntoDocumento = 1;

            var existeOficio = agregarDocumento.PersonasNotificacionIndividual.Where(x => x.TipoNotificacionId == 11 || x.TipoNotificacionId == 5).ToList();

            foreach (var autoPromoParte in existeOficio)
            {
                var textoOficioLibreId = "";
                if (autoPromoParte.TipoNotificacionId == 11)
                {
                    textoOficioLibreId = $"Texto-Oficio-Libre-{Guid.NewGuid()}";
                }
                else if (autoPromoParte.TipoNotificacionId == 5)
                {
                    textoOficioLibreId = $"Oficio-{autoPromoParte.TipoNotificacionId}";
                }

                string nombreAutoridad = autoPromoParte.NombreParte.Split('-')[0].Trim();
                int personaId = 0;
                if (autoPromoParte.PersonaId != null)
                {
                    personaId = (int)autoPromoParte.PersonaId;
                }
                else if (autoPromoParte.PromoventeId != null)
                {
                    personaId = (int)autoPromoParte.PromoventeId;
                }

                asuntos.Add(new AutoridadAsunto
                {
                    NombreAutoridad = nombreAutoridad,
                    AnexoParteDescripcion = autoPromoParte.DescripcionPromovente,
                    TextoOficioLibreId = textoOficioLibreId,
                    AnexoParteId = personaId,
                    NumeroOficio = await _tramitesRepository.ObtenerNumeroOficio(request.CatIdOrganismo, request.AsuntoNeunId, _resultadoAsuntoDocumento, personaId)
                });
                if (autoPromoParte.TipoNotificacionId == 11)
                {
                    listaTextoOficioLibre.Add(new TextoOficioLibreDto
                    {
                        TextoOficioLibreId = textoOficioLibreId,
                        TextoOficioLibre = autoPromoParte.TextoOficioLibre,
                    });
                }
                if (autoPromoParte.TipoNotificacionId == 5)
                {
                    listaTextoOficioLibre.Add(new TextoOficioLibreDto
                    {
                        TextoOficioLibreId = $"Oficio-{autoPromoParte.TipoNotificacionId}",
                        TextoOficioLibre = htmlAcuerdo
                    });
                }
            }

            var jsonOficioLibre = JsonConvert.SerializeObject(listaTextoOficioLibre.ToArray());
            await _documentoBlob.GuardarBlobDocumento(Encoding.UTF8.GetBytes(jsonOficioLibre), textoLibreId, contenedorOficiosGenerados, uri);


            var esOrganismoUnc = await _tramitesRepository.GetStatusUNC(request.CatIdOrganismo);

            if (existeOficio.Count() >= 1)
            {
                await _processQueueService.TriggerProcessAsync(new CrearOficiosDTO()
                {
                    Id = datosDocumento.GuidDocumento,
                    AsuntoAlias = request.AsuntoAlias.ToString(),
                    AsuntoNeunId = request.AsuntoNeunId.ToString(),
                    AsuntoDocumentoIdOficio = asuntoDocumentoIdOficio,
                    AsuntoDocumentoId = datosDocumento.AsuntoDocumentoId.ToString(),
                    NombreArchivo = datosDocumento.NombreArchivo,
                    RutaNas = rutaNas,
                    RutaNasAcuerdo = rutaNasAcuerdo,
                    TipoAnexoId = existeOficio.FirstOrDefault().TipoAnexoId.ToString(),
                    TipoAsunto = request.CatTipoAsunto.ToString(),
                    Mesa = request.Mesa.ToString(),
                    OrganismoId = _sesionService.SesionActual.CatOrganismoId.ToString(),
                    UsuarioId = _sesionService.SesionActual.EmpleadoId.ToString(),
                    Asuntos = asuntos,
                    AplicaUNC = esOrganismoUnc,
                    TextoLibreId = textoLibreId,
                    RutaNasId = ruta.First().RutaId
                });
            }
        }
        return await Task.FromResult(resultado);
    }

    private static string QrAcuerdo(DatosDocumento datosDocumento, AgregarDocumento agregarDocumento)
    {
        string strQRCode = "{\"E\": \"*\", \"A\": \"**\" }".Replace("*", agregarDocumento.AsuntoNeunId.ToString());
        strQRCode = strQRCode.Replace("**", datosDocumento.AsuntoDocumentoId.ToString());
        return strQRCode;
    }
}