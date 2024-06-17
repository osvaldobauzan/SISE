using System.Text;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Procesos;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Actuaria.Domain.Entities;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
using CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Comandos.CreacionOficiosComando;
using CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerArchivos;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Utilities;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.GenerarOficio;
public class GenerarOficioCommand : IRequestHandler<GenerarOficioRequestDto, ResponseDatosGenerarFolioM>
{

    private readonly ISesionService _sesionService;
    private readonly IRutasChunkRepository _rutasChunkRepository;
    private readonly ISanitizerService _sanitizerService;
    private readonly IConfiguration _configuration;
    private readonly IDocumentoBlob _documentoBlob;
    private readonly IActuariaRepository _actuariaRepository;
    private readonly IProcessQueueService _processQueueService;
    private readonly IMapper _mapper;
    private string Modulo = "Tramite";

    public GenerarOficioCommand(ISesionService sesionService, IRutasChunkRepository rutasChunkRepository, ISanitizerService sanitizerService, IWordContenido _wordContenido,
        IConfiguration configuration, IDocumentoBlob _documentoBlob, IActuariaRepository actuariaRepository, IProcessQueueService processQueueService, IMapper mapper)
    {
 
        _sesionService = sesionService;
        _rutasChunkRepository = rutasChunkRepository;
        _sanitizerService = sanitizerService;
        _configuration = configuration;
        this._documentoBlob = _documentoBlob;
        _actuariaRepository = actuariaRepository;
        _processQueueService = processQueueService;
        _mapper = mapper;
    }
    public async Task<ResponseDatosGenerarFolioM> Handle(GenerarOficioRequestDto request, CancellationToken cancellationToken)
    {
        var idEmpleado = _sesionService.SesionActual.EmpleadoId;
        var catOrganismoId = _sesionService.SesionActual.CatOrganismoId;


        var rutaNas = string.Empty;
        var rutaNasAcuerdo = string.Empty;
        var resultado = new ResponseDatosGenerarFolioM();

        var ruta = await _rutasChunkRepository.RutaArchivo(Modulo);
        rutaNas = ruta.First().Ruta;

        var PersonasNotificacion = new List<PersonasDto>();
  
        if (request.PartesLista != null)
        {

            foreach (var t in request.PartesLista)
            {
                var itm = new PersonasDto();
                itm.PersonaId = t.PersonaId;
                itm.TipoNotificacionId = t.TipoNotificacionId;

                itm.DescripcionPromovente = t.AnexoParteDescripcion;
                itm.NombreParte = t.NombreParte;
                itm.TextoOficioLibre = t.TextoOficioLibre;

                if (itm.TipoNotificacionId == 11)
                {
                    itm.TipoAnexoId = 6;
                }
                else if (itm.TipoNotificacionId == 5)
                {
                    itm.TipoAnexoId = 1;
                }

                t.TextoOficioLibre = _sanitizerService.SanitizeHtml(t.TextoOficioLibre);

                PersonasNotificacion.Add(itm);
            }

            var info = new AgregarFoliosPartes()
            {
                AsuntoNeunId = request.AsuntoNeunId,
                AsuntoDocumentoId = request.AsuntoDocumentoId,
                PartesNotificaciones = _mapper.Map<List<ParteNotificacionFolios>>(PersonasNotificacion)
            };

            resultado = await _actuariaRepository.GenerarFoliosPartes(info, idEmpleado);

            if (PersonasNotificacion != null && resultado.Datos != null)
            {
                var uri = _configuration["SISE3:BackEnd:SMTPTemplatesUrl"];
                var contenedorOficiosGenerados = _configuration["SISE3:BackEnd:OficiosContenedor"];

                var asuntos = new List<AutoridadAsunto>();
                var listaTextoOficioLibre = new List<TextoOficioLibreDto>();
                var textoLibreId = $"Oficio-Libre-{Guid.NewGuid()}";
                string asuntoDocumentoIdOficio;
                if (request.AsuntoDocumentoIdOficio == null)
                {
                    asuntoDocumentoIdOficio = request.AsuntoDocumentoId.ToString();
                }
                else
                {
                    asuntoDocumentoIdOficio = request.AsuntoDocumentoIdOficio.ToString();
                    if (request.AsuntoDocumentoIdOficio == 0 && asuntoDocumentoIdOficio == "0")
                        asuntoDocumentoIdOficio = request.AsuntoDocumentoId.ToString();
                }
                int.TryParse(asuntoDocumentoIdOficio, out int _resultadoAsuntoDocumento);
                if (_resultadoAsuntoDocumento == 0)
                    _resultadoAsuntoDocumento = 1;

                var existeOficio = PersonasNotificacion.Where(x => x.TipoNotificacionId == 11 || x.TipoNotificacionId == 5).ToList();

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
                        personaId = autoPromoParte.PersonaId;
                    }
                    else if (autoPromoParte.PromoventeId != null)
                    {
                        personaId = autoPromoParte.PromoventeId;
                    }

                    asuntos.Add(new AutoridadAsunto
                    {
                        NombreAutoridad = nombreAutoridad,
                        AnexoParteDescripcion = autoPromoParte.DescripcionPromovente,
                        TextoOficioLibreId = textoOficioLibreId,
                        AnexoParteId = personaId,
                        NumeroOficio = "0"
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
                            TextoOficioLibre = "<p>Oficio Actuaria</p>"
                        });
                    }
                }

                var jsonOficioLibre = JsonConvert.SerializeObject(listaTextoOficioLibre.ToArray());
                await _documentoBlob.GuardarBlobDocumento(Encoding.UTF8.GetBytes(jsonOficioLibre), textoLibreId, contenedorOficiosGenerados, uri);


                var esOrganismoUnc = await _actuariaRepository.GetStatusUNC(catOrganismoId);

                if (existeOficio.Count() >= 1)
                {
                    await _processQueueService.TriggerProcessAsync(new CrearOficiosDTO()
                    {
                        Id = new Guid(resultado.Datos.FirstOrDefault().GuidAsuntoDcoumento),
                        AsuntoAlias = resultado.Datos.FirstOrDefault().AsuntoAlias.ToString(),
                        AsuntoNeunId = resultado.Datos.FirstOrDefault().AsuntoNeunId.ToString(),
                        AsuntoDocumentoIdOficio = asuntoDocumentoIdOficio,
                        AsuntoDocumentoId = request.AsuntoDocumentoId.ToString(),
                        NombreArchivo = resultado.Datos.FirstOrDefault().NombreArchivo,
                        RutaNas = rutaNas,
                        RutaNasAcuerdo = rutaNasAcuerdo,
                        TipoAnexoId = existeOficio.FirstOrDefault().TipoAnexoId.ToString(),
                        TipoAsunto = resultado.Datos.FirstOrDefault().TipoAsuntoId.ToString(),
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

        }

        return await Task.FromResult(resultado);
     
    }
}
