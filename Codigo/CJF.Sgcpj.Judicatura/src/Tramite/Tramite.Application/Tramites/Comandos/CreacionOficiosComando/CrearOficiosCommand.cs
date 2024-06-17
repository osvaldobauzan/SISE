using Azure.Data.Tables;
using Azure.Identity;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Alertas;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.TableEntities;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Comandos.CreacionOficiosComando;

[SesionNoRequired]
public class CrearOficiosCommand : IRequest<bool>
{
    public CrearOficiosDTO CrearOficiosDTO { get; set; }
}

public class CrearOficiosCommandHandler : IRequestHandler<CrearOficiosCommand, bool>
{
    private readonly ILogger<CrearOficiosCommandHandler> _logger;
    private readonly IAlertsMessageService _alertsMessageService;
    private readonly IRutasChunkService _rutasChunkService;
    private readonly IConfiguration _configuration;
    private readonly IDocumentoBlob _documentoBlob;
    private readonly IWordContenido _wordContenido;
    private readonly INasArchivo _nas;
    private readonly IWordsUtil _wordsUtil;
    private readonly ITramitesRepository _tramitesRepository;

    private int NumeroOficio = 1;
    private const int TipoOficioLibre = 6;

    public CrearOficiosCommandHandler(ILogger<CrearOficiosCommandHandler> logger,
                                      IAlertsMessageService alertsMessageService,
                                      IRutasChunkService rutasChunkService,
                                      IConfiguration configuration,
                                      IDocumentoBlob documentoBlob,
                                      IWordContenido wordContenido,
                                      INasArchivo nas,
                                      IWordsUtil wordsUtil,
                                      ITramitesRepository tramitesRepository)
    {
        _logger = logger;
        _alertsMessageService = alertsMessageService;
        _rutasChunkService = rutasChunkService;
        _configuration = configuration;
        _documentoBlob = documentoBlob;
        _wordContenido = wordContenido;
        _nas = nas;
        _wordsUtil = wordsUtil;
        _tramitesRepository = tramitesRepository;
    }

    public async Task<bool> Handle(CrearOficiosCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var numeroDeOficios = request.CrearOficiosDTO.Asuntos.Count;
            var oficiosDisponibles = request.CrearOficiosDTO.Asuntos.Count;
            var oficioActual = 1;
            var partitionKey = Guid.NewGuid();
            var logId = Guid.Empty;
            var alertaId = Guid.NewGuid();
            var uri = _configuration["SISE3:BackEnd:SMTPTemplatesUrl"];
            var contenedorOficiosGenerados = _configuration["SISE3:BackEnd:OficiosContenedor"];
            await _alertsMessageService.TriggerAlertAsync(new AlertDTO<OficiosAlertDTO>()
            {
                Destinatarios = new List<Destinatario>()
                    {
                        new Destinatario()
                        {
                            OrganismoId = request.CrearOficiosDTO.OrganismoId,
                            UsuarioId = request.CrearOficiosDTO.UsuarioId
                        }
                    },
                TipoDeAlerta = AlertType.SignalR,
                Alerta = new OficiosAlertDTO()
                {
                    Id = alertaId,
                    TipoProcesoId = AvanceProceso.Inicio,
                    TipoProceso = AvanceProceso.Inicio.ToString(),
                    EsAlertaSesion = true,
                    FechaHora = DateTime.Now,
                    OrigenId = Origenes.Trámite,
                    Origen = Origenes.Trámite.ToString(),
                    Contenido = $"Se generarán {numeroDeOficios} para las diferentes autoridades",
                    Titulo = $"Generando oficios para expediente {request.CrearOficiosDTO.AsuntoAlias}"
                }
            });

            _logger.LogInformation($"Inicia paralelizado de {numeroDeOficios} oficios");
            var textoOficioLibreJson = await _documentoBlob.ObtenerTextoBlob(request.CrearOficiosDTO.TextoLibreId, contenedorOficiosGenerados, uri);
            var listTextoOficioLibre = JsonConvert.DeserializeObject<List<TextoOficioLibreDto>>(textoOficioLibreJson);

            foreach (var asunto in request.CrearOficiosDTO.Asuntos)
            {
                var partitionKeyOficio = Guid.NewGuid();

                oficioActual = oficiosDisponibles;
                oficiosDisponibles = oficiosDisponibles - 1;

                await CrearOficioAsync(request.CrearOficiosDTO, partitionKeyOficio, alertaId, request.CrearOficiosDTO.Id,
                                       listTextoOficioLibre.First(x => x.TextoOficioLibreId == asunto.TextoOficioLibreId).TextoOficioLibre,
                                       listTextoOficioLibre.First(x => x.TextoOficioLibreId == asunto.TextoOficioLibreId).TextoOficioLibreId,
                                       asunto.NombreAutoridad, numeroDeOficios, oficioActual, asunto.NumeroOficio,
                                       asunto.AnexoParteId);
            }

            _logger.LogInformation($"Termina paralelizado de {numeroDeOficios} oficios");

            await _alertsMessageService.TriggerAlertAsync(new AlertDTO<OficiosAlertDTO>()
            {
                Destinatarios = new List<Destinatario>()
                    {
                        new Destinatario()
                        {
                            OrganismoId = request.CrearOficiosDTO.OrganismoId,
                            UsuarioId = request.CrearOficiosDTO.UsuarioId
                        }
                    },
                TipoDeAlerta = AlertType.SignalR,
                Alerta = new OficiosAlertDTO()
                {
                    Id = alertaId,
                    TipoProcesoId = AvanceProceso.Fin,
                    TipoProceso = AvanceProceso.Fin.ToString(),
                    EsAlertaSesion = true,
                    FechaHora = DateTime.Now,
                    OrigenId = Origenes.Trámite,
                    Origen = Origenes.Trámite.ToString(),
                    Contenido = $"Se generaron oficios {numeroDeOficios}/{numeroDeOficios}",
                    Titulo = $"Generando oficios para expediente {request.CrearOficiosDTO.AsuntoAlias}"
                }
            });

            return true;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex.ToString());
            return false;
        }
    }

    private async Task CrearOficioAsync(CrearOficiosDTO request, Guid partitionKey, Guid alertaId,
                                        Guid acuerdoId, string textoOficioLibre,
                                         string textoOficioLibreId, string nombreAutoridad,
                                        int numeroDeOficios, int oficioActual, string numeroOficio, int? parteAnexoId)
    {
        var logId = Guid.NewGuid();

        try
        {
            _logger.LogInformation($"Inicia oficio {oficioActual}/{numeroDeOficios}");

            await SaveStatusAsync(partitionKey, logId, "Inicio", acuerdoId, $"{oficioActual}/{numeroDeOficios}");

            var uri = _configuration["SISE3:BackEnd:SMTPTemplatesUrl"];
            var contenedorPlantillas = _configuration["SISE3:BackEnd:OficiosPlantillasContenedor"];
            var identificadorPlantilla = $"PlantillaOficio.base.{request.OrganismoId}.docx";
            var plantilla = await _documentoBlob.ObtenerBlobDocumento(identificadorPlantilla, contenedorPlantillas, uri);

            IDictionary<string, string> valoresComunesExpediente = new Dictionary<string, string>();

            valoresComunesExpediente.Add("CatTipoAsunto", request.TipoAsunto);
            valoresComunesExpediente.Add("AsuntoAlias", request.AsuntoAlias);
            valoresComunesExpediente.Add("Mesa", $" - {request.Mesa}");

            foreach (var textoAReemplazar in valoresComunesExpediente)
            {
                plantilla = _wordsUtil.ReplaceTextInDocx(plantilla, textoAReemplazar.Key, textoAReemplazar.Value);
            }

            var oficio = plantilla.ToArray();
            oficio = _wordsUtil.ReplaceTextInDocx(oficio, "CatTipoPersona", nombreAutoridad);
            oficio = _wordsUtil.ReplaceTextInDocx(oficio, "NumeroOficio", numeroOficio);

            IDictionary<string, string> qrs = new Dictionary<string, string>();
            qrs.Add("qr1", "{\"E\":{\"N\":" + request.AsuntoNeunId
                + ",\"O\":{\"F\":" + numeroOficio.Split('/').First() + ",\"A\":" + DateTime.Today.Year.ToString() + "}}");
            if (request.AplicaUNC)
            {
                qrs.Add("qr2", $"{request.OrganismoId}/{request.TipoAnexoId}/" +
                    $"{DateTime.Today.Year}/" + $"{numeroOficio.Split('/').First()}/{request.AsuntoNeunId}");
            }
            else
            {
                oficio = _wordsUtil.RemoveImage(oficio, "qr2");
            }


            foreach (var qr in qrs)
            {
                oficio = _wordsUtil.InsertQRCodeInWordDocument(oficio, qr.Key, qr.Value, 10);
            }
            NumeroOficio++;


            oficio = _wordContenido.ReplaceHtmlInWord(oficio, textoOficioLibre);

            var guidOficio = Guid.NewGuid();
            var rutaNasOficio = request.RutaNas + "\\" + request.OrganismoId + "\\" +
                request.NombreArchivo + "-oficio-" + guidOficio + ".docx";
            _nas.AlmacenarArchivo(rutaNasOficio, oficio);
            var rutaNasOficioPdf = request.RutaNas + "\\" + request.OrganismoId + "\\" +
            request.NombreArchivo + "-oficio-" + guidOficio + ".pdf";

            var rutaNasOficioPdfOriginal = request.RutaNas + "\\" + request.OrganismoId + "\\" +
            request.NombreArchivo + "-oficio-" + guidOficio + "_original" + ".pdf";

            var aux = 0;
            long auxLong = 0;

            var oficioRoot = new EstadoOficioRoot();
            var estadoOficio = new EstadoOficio()
            {
                GuidDocumento = guidOficio,
                RutaId = request.RutaNasId,
                Nombre = request.NombreArchivo + "-oficio-" + guidOficio,
                Extension = ".docx",
                Firmado = false,
                AsuntoNeunId = long.TryParse(request.AsuntoNeunId, out auxLong) ? Convert.ToInt64(request.AsuntoNeunId) : null,
                AsuntoDocumentoId = int.TryParse(request.AsuntoDocumentoId, out aux) ? Convert.ToInt32(request.AsuntoDocumentoId) : null,
                AnexoParteId = parteAnexoId,
                CatOrganismoId = int.TryParse(request.OrganismoId, out aux) ? Convert.ToInt32(request.OrganismoId) : null
            };

            oficioRoot.EstadoOficio = estadoOficio;

            await _tramitesRepository.ActualizaEstadoOficio(oficioRoot);

            await SaveStatusAsync(partitionKey, logId, "Finalizado", acuerdoId, $"{oficioActual}/{numeroDeOficios}");

            await _alertsMessageService.TriggerAlertAsync(new AlertDTO<OficiosAlertDTO>()
            {
                Destinatarios = new List<Destinatario>()
                    {
                    new Destinatario()
                    {
                            OrganismoId = request.OrganismoId,
                            UsuarioId = request.UsuarioId
                        }
                    },
                TipoDeAlerta = AlertType.SignalR,
                Alerta = new OficiosAlertDTO()
                {
                    Id = alertaId,
                    TipoProcesoId = AvanceProceso.Avance,
                    TipoProceso = AvanceProceso.Avance.ToString(),
                    EsAlertaSesion = true,
                    FechaHora = DateTime.Now,
                    OrigenId = Origenes.Trámite,
                    Origen = Origenes.Trámite.ToString(),
                    Contenido = $"Se generó oficio {oficioActual}/{numeroDeOficios}",
                    Titulo = $"Generando oficios para expediente {request.AsuntoAlias}"
                }
            });

            _logger.LogInformation($"Termina oficio {oficioActual}/{numeroDeOficios}");
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Error en oficio {oficioActual}/{numeroDeOficios}: {ex}");

            var oficioMessage = JsonConvert.SerializeObject(request);
            await SaveStatusAsync(partitionKey, logId, "Error", acuerdoId, $"{oficioActual}/{numeroDeOficios}",
                                  0, ex.ToString(), oficioMessage);

            await _alertsMessageService.TriggerAlertAsync(new AlertDTO<OficiosAlertDTO>()
            {
                Destinatarios = new List<Destinatario>()
                    {
                        new Destinatario()
                        {
                            OrganismoId = request.OrganismoId,
                            UsuarioId = request.UsuarioId
                        }
                    },
                TipoDeAlerta = AlertType.SignalR,
                Alerta = new OficiosAlertDTO()
                {
                    Id = alertaId,
                    TipoProcesoId = AvanceProceso.Inicio,
                    TipoProceso = AvanceProceso.Error.ToString(),
                    EsAlertaSesion = true,
                    FechaHora = DateTime.Now,
                    OrigenId = Origenes.Trámite,
                    Origen = Origenes.Trámite.ToString(),
                    Contenido = $"Se generaron oficios {numeroDeOficios}/{numeroDeOficios}",
                    Titulo = $"Generando oficios para expediente {request.AsuntoAlias}"
                }
            });
        }
    }

    private async Task SaveStatusAsync(Guid partitionKey, Guid id, string status, Guid acuerdoId,
                                       string oficioActual, int reintento = 0,
                                       string errorMsg = null, string msg = null)
    {
        var setting = _configuration["SISE3:BackEnd:AlertasUrlTabla"];
        var tableClient = new TableClient(new Uri(setting), "Oficios", new DefaultAzureCredential());

        await tableClient.UpsertEntityAsync<OficioEntity>(new OficioEntity()
        {
            PartitionKey = partitionKey.ToString(),
            RowKey = id.ToString(),
            AcuerdoId = acuerdoId.ToString(),
            Status = status,
            Reintento = reintento,
            MensajeError = errorMsg,
            MensajeSerializado = msg,
            CreatedOn = DateTime.UtcNow,
            OficioActual = oficioActual
        });
    }

}
