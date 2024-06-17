using System.Text;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.CorreoMesa;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Alertas;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Common.Application.Utils;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Comandos;
public class EditarPromocionComando : IRequest<long>
{
    public EditarPromocionDto Promocion { get; set; }
}

public class EditarPromocionComandoHandler : IRequestHandler<EditarPromocionComando, long>
{
    private readonly IMapper _mapper;
    private readonly IAlertsMessageService _alertsMessageService;
    private readonly IPromocionesRepository _promocionesRepository;
    private readonly ISesionService _sesionService;
    private readonly IMediator _mediator;
    private readonly IDocumentoBlob _documentoBlob;
    private readonly IConfiguration _configuration;

    public EditarPromocionComandoHandler(IMapper mapper,
                                         IAlertsMessageService alertsMessageService,
                                         IPromocionesRepository promocionesRepository,
                                         ISesionService sesionService,
                                         IMediator mediator,
                                        IDocumentoBlob documentoBlob,
                                        IConfiguration configuration)
    {
        _mapper = mapper;
        _alertsMessageService = alertsMessageService;
        _promocionesRepository = promocionesRepository;
        _sesionService = sesionService;
        _mediator = mediator;
        _documentoBlob = documentoBlob;
        _configuration = configuration;
    }

    public async Task<long> Handle(EditarPromocionComando request, CancellationToken cancellationToken)
    {
        var uri = _configuration["SISE3:BackEnd:SMTPTemplatesUrl"];
        var url = _configuration["SISE3:BackEnd:SMTPTemplatesVerPromoUrl"];
        var contenedorPlantillas = _configuration["SISE3:BackEnd:OficiosPlantillasContenedor"];
        var contenedorOficiosGenerados = _configuration["SISE3:BackEnd:OficiosContenedor"];

        var promocion = _mapper.Map<EditarPromocion>(request.Promocion);
        promocion.IdOrganismo = _sesionService.SesionActual.CatOrganismoId;
        promocion.RegistroEmpleadoId = _sesionService.SesionActual.EmpleadoId;


        if (promocion.EsPromoventeExistente)
        {
            if (promocion.ClasePromovente == 2)
            {
                if (promocion.PromoventeExistente != null)
                {
                    promocion.PersonaId = promocion.PromoventeExistente.PersonaId;

                }
            }
            else if (promocion.ClasePromovente == 1)
            {
                if (promocion.PromoventeAutoridadExistente != null)
                {
                    promocion.PersonaId = Convert.ToInt32(promocion.PromoventeAutoridadExistente.PersonaId);

                }
            }


        }

        if (promocion.ClasePromovente == 3)
        {
            if (promocion.PromoventeAutoridad != null)
            {
                promocion.PersonaId = promocion.PromoventeAutoridad.EmpleadoId;
            }
        }


        var promocionPrevia = await _promocionesRepository.ObtenerPromocionDetalleTablero(
            _sesionService.SesionActual.CatOrganismoId, promocion.AsuntoNeunId, 0,
            0, promocion.NumeroOrden, promocion.YearPromocion, null);

        var cambioDeSecretario = promocionPrevia.Item1.First().SecretarioId != request.Promocion.SecretarioId;

        var resultado = await _promocionesRepository.EditarPromocion(promocion);

        if (request.Promocion.ArchivoAVincular != null || cambioDeSecretario)
        {
            /******************* Se comentan alertas por que esta hasta que se mande asincrono *****************/
            /*
            var correoMesa = await _mediator.Send(new CorreoMesaComando { CatOrganismoId = _sesionService.SesionActual.CatOrganismoId, EmpleadoIdResponsable = promocion.SecretarioId });
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

            var registro = request.Promocion.Registro;
            var numeroExpediente = request.Promocion.NumeroExpediente;
            var tipoAsunto = request.Promocion.TipoAsunto?.TipoAsunto;
            var tipoProcedimiento = request.Promocion.TipoProcedimiento?.DESCRIPCION;
            var mesa = request.Promocion.Secretario?.Area;
            var fechaTurno = Judicatura.Common.Application.Utils.DateTimeUtil.ObtenerHoraLocal().ToString("dd/MM/yyyy");
            var horaTurno = Judicatura.Common.Application.Utils.DateTimeUtil.ObtenerHoraLocal().ToString("HH:mm");
            var fechaPresentacion = request.Promocion.FechaPresentacion;
            var horaPresentacion = request.Promocion.HoraPresentacion;

            var asuntoNeunId = request.Promocion.AsuntoNeunId;
            var origen = request.Promocion.Origen;
            var numeroOrden = request.Promocion.NumeroOrden;
            var yearPromocion = DateTimeUtil.ObtenerHoraLocal().Year.ToString();
            var urlVerPromocion = "&asuntoNeunId=" + asuntoNeunId + "&origen=" + origen + "&numeroOrden=" + numeroOrden + "&yearPromocion=" + yearPromocion;
            var urlVerPromo = url + Convert.ToBase64String(Encoding.UTF8.GetBytes(urlVerPromocion));

            var mensaje = $"La promoción {registro} del expediente {numeroExpediente} " +
                $"{tipoAsunto} {tipoProcedimiento} ha sido asignada a la {mesa}";

            mensaje = mensaje.Replace("  ", " ");

            await _alertsMessageService.TriggerAlertAsync(new AlertDTO<SignalRAlertDTO>()
            {
                TipoDeAlerta = AlertType.SignalR,
                Destinatarios = destinatarios,
                Alerta = new SignalRAlertDTO()
                {
                    Id = Guid.NewGuid(),
                    Emisor = "Oficialía",
                    Estado = "Se asignó una nueva promoción",
                    Mensaje = mensaje,
                    Receptor = "Receptor de promoción",
                    Parte = "Parte promoción",
                    OrganismoEmisor = _sesionService.SesionActual.CatOrganismoId.ToString()
                },
                PersistirAlerta = true
            });

            var templateEmail = await _documentoBlob.ObtenerPlantillaCorreo("plantillaPromocionCorreo.html", "emailtemplates", uri);
            var generarCorreoAlertaUtil = new GenerarCorreoAlertaUtil(_alertsMessageService);

            await generarCorreoAlertaUtil.GeneraCorreoAlertaPromocion(registro.ToString(), numeroExpediente, tipoAsunto, mesa, destinatarios, horaPresentacion, fechaPresentacion, tipoProcedimiento, fechaTurno, horaTurno, templateEmail, urlVerPromo);
            */
        }
        return resultado;
    }
}
