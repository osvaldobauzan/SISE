using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Alertas;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Bre;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Comandos.CorreoMesa;
using MediatR;
using Microsoft.Extensions.Configuration;
using RulesEngine.Exceptions;
using RulesEngine.Extensions;
using RulesEngine.Models;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Comandos.EstadoAcuerdoComandoRE;
public class EstadoAcuerdoREComando : IRequest<bool>
{
    public EstadoAcuerdoREDto Acuerdo { get; set; }
}
public class EstadoAcuerdoComandoREHandler : IRequestHandler<EstadoAcuerdoREComando, bool>
{
    private const int ESTADO_CANCELAR = 3;
    private const int ESTADO_AUTORIZAR = 2;
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;
    private readonly IDocumentoBlob _documentoBlob;
    private readonly IAlertsMessageService _alertsMessageService;
    private readonly BreHelper _breHelper;
    private readonly ITramitesRepository _tramitesRepository;

    public EstadoAcuerdoComandoREHandler(IMapper mapper,
                                         ITramitesRepository tramitesRepository,
                                         ISesionService sesionService,
                                         IMediator mediator,
                                         IConfiguration configuration,
                                         IDocumentoBlob documentoBlob,
                                         IAlertsMessageService alertsMessageService,
                                         BreHelper breHelper)
    {
        _mapper = mapper;
        _tramitesRepository = tramitesRepository;
        _sesionService = sesionService;
        _mediator = mediator;
        _configuration = configuration;
        _documentoBlob = documentoBlob;
        _alertsMessageService = alertsMessageService;
        _breHelper = breHelper;
    }
    public async Task<bool> Handle(EstadoAcuerdoREComando request, CancellationToken cancellationToken)
    {
        var acuerdo = _mapper.Map<EstadoAcuerdo>(request.Acuerdo);
        acuerdo.Valor = _sesionService.SesionActual.EmpleadoId;
        var flujoTrabajo = "wf-cambio-estados-sps";
        var rulesEngine = await _breHelper.ObtenerFlujoDeTrabajo(flujoTrabajo);

        List<RuleResultTree> resultList = await rulesEngine.ExecuteAllRulesAsync(flujoTrabajo, acuerdo);
        var spEjecucion = string.Empty;
        resultList.OnSuccess((resultado) =>
        {
            spEjecucion = resultado;
        });

        resultList.OnFail(() =>
        {
            throw new RuleException("No se reconoce el estado del acuerdo  actualizar");
        });

        if (request.Acuerdo.Estado == ESTADO_AUTORIZAR)
        {
            //Apartir del que se autoriza el documento para a ser un pdf
            acuerdo.NombreDocumento += ".pdf";
        }


        var result = await _tramitesRepository.ActualizaEstadoAcuerdoBre(spEjecucion, acuerdo);

        if (!result)
        {
            return false;
        }

        if (request.Acuerdo.Estado == ESTADO_CANCELAR)
        {
            await CancelarOficiosFirmadorAsync(request.Acuerdo.Id);
        }

        if (!request.Acuerdo.EnviarAlerta)
        {
            return true;
        }

        /******************* Se comentan alertas por que esta hasta que se mande asincrono *****************/
        /*
        var correoMesa = await _mediator.Send(new CorreoMesaComando
        {
            CatOrganismoId = _sesionService.SesionActual.CatOrganismoId,
            EmpleadoIdResponsable = int.Parse(request.Acuerdo.SecretarioId)
        });

        var destinatarios = new List<Destinatario>();

        foreach (var correo in correoMesa)
        {
            destinatarios.Add(new Destinatario()
            {
                UsuarioId = correo.EmpleadoId.ToString(),
                OrganismoId = _sesionService.SesionActual?.CatOrganismoId.ToString() ?? string.Empty,
                DireccionDestino = correo.Correo
            });
        }

        destinatarios.Add(new Destinatario()
        {
            DireccionDestino = string.Empty,
            OrganismoId = _sesionService.SesionActual?.CatOrganismoId.ToString() ?? string.Empty,
            UsuarioId = _sesionService.SesionActual?.EmpleadoId.ToString() ?? string.Empty
        });

        var uri = _configuration["SISE3:BackEnd:SMTPTemplatesUrl"];
        var templateEmail = await _documentoBlob.ObtenerPlantillaCorreo("plantillaPromocionCorreo.html", "emailtemplates", uri);

        var registro = request.Acuerdo.AsuntoDocumentoId;
        var numeroExpediente = request.Acuerdo.NumeroExpediente;
        var tipoAsunto = request.Acuerdo.TipoAsunto;
        var descripcion = request.Acuerdo.TipoProcedimiento;
        var mesa = request.Acuerdo.Mesa;

        var mensaje = $"La promoción {request.Acuerdo.NumeroPromocion} del expediente {numeroExpediente} " +
            $"{tipoAsunto} {descripcion} ha sido cancelada";

        mensaje = mensaje.Replace("  ", " ");

        await _alertsMessageService.TriggerAlertAsync(new AlertDTO<SignalRAlertDTO>()
        {
            TipoDeAlerta = AlertType.SignalR,
            Destinatarios = destinatarios,
            Alerta = new SignalRAlertDTO()
            {
                Id = Guid.NewGuid(),
                Emisor = "Oficialía",
                Estado = "Se ha cancelado la promoción",
                Mensaje = mensaje,
                Receptor = "Receptor de promoción",
                Parte = "Parte promoción",
                OrganismoEmisor = _sesionService.SesionActual.CatOrganismoId.ToString(),
            },
            PersistirAlerta = true
        });
    */
        return true;
    }

    private async Task CancelarOficiosFirmadorAsync(Guid id)
    {
        var documentos = await _tramitesRepository.ObtenerAcuerdosOficios(id);
        foreach (var documento in documentos)
        {
            if (documento.EsAcuerdo == 1)
            {
                if (documento.ExtensionDocumento == "doc")
                {
                    await _tramitesRepository.ActualizaDeterminacion(new DeterminacionAcuerdoRoot()
                    {
                        DeterminacionAcuerdo = new DeterminacionAcuerdo()
                        {
                            Firmado = false,
                            GUID = id,
                            Nombre = documento.NombreArchivo,
                            Extension = ".doc" // Se regresa al archivo original
                        }
                    });
                }
                else if (documento.ExtensionDocumento == "docx")
                {
                    await _tramitesRepository.ActualizaDeterminacion(new DeterminacionAcuerdoRoot()
                    {
                        DeterminacionAcuerdo = new DeterminacionAcuerdo()
                        {
                            Firmado = false,
                            GUID = id,
                            Nombre = documento.NombreArchivo,
                            Extension = ".docx" // Se regresa al archivo original
                        }
                    });
                }

            }
            else
            {
                await _tramitesRepository.ActualizaEstadoOficio(new EstadoOficioRoot()
                {
                    EstadoOficio = new EstadoOficio
                    {
                        Firmado = false,
                        GuidDocumento = documento.uGuid,
                        Nombre = documento.NombreArchivo,
                        Extension = ".docx"
                    }
                });
            }
        }
    }
}
