using AutoMapper;
using CJF.Sgcpj.Judicatura.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Alertas;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace CJF.Sgcpj.Judicatura.Oficialia.Application.Promociones.Comandos.AgregarPromociones;
public class AgregarPromocionComando : IRequest<DatosPromocion>
{
    public AgregarPromocionDto Promocion { get; set; }
}

public class AgregarPromocionComandoHandler : IRequestHandler<AgregarPromocionComando, DatosPromocion>
{
    private readonly IMapper _mapper;
    private readonly IPromocionesRepository _promocionesRepository;
    private readonly IAlertsMessageService _alertsMessageService;
    private readonly IMediator _mediator;
    private readonly ISesionService _sesionService;
    private readonly IDocumentoBlob _documentoBlob;
    private readonly IConfiguration _configuration;
    private readonly IRutasChunkService _rutasChunkService;
    private readonly IWordsUtil _wordsUtil;
    private readonly INasArchivo _nas;

    public AgregarPromocionComandoHandler(IMapper mapper, IPromocionesRepository promocionesRepository,
                                          IAlertsMessageService alertsMessageService, IMediator mediator, ISesionService sesionService,
                                           IDocumentoBlob documentoBlob, IConfiguration configuration,
                                           IRutasChunkService rutasChunkService, IWordsUtil wordsUtil, INasArchivo nas)
    {
        _mapper = mapper;
        _promocionesRepository = promocionesRepository;
        _alertsMessageService = alertsMessageService;
        _mediator = mediator;
        _sesionService = sesionService;
        _documentoBlob = documentoBlob;
        _configuration = configuration;
        _rutasChunkService = rutasChunkService;
        _wordsUtil = wordsUtil;
        _nas = nas;
    }
    public async Task<DatosPromocion> Handle(AgregarPromocionComando request, CancellationToken cancellationToken)
    {
        /*Agrega nuevo expediente y promocion*/
        var promocion = _mapper.Map<AgregarPromocion>(request.Promocion);
        promocion.CatOrganismoId = _sesionService.SesionActual.CatOrganismoId;
        promocion.EmpleadoId = _sesionService.SesionActual.EmpleadoId;

        var resultado = await _promocionesRepository.AgregarPromocion(promocion);

        if (promocion.Origen != 4) // 4 son promociones de oficialia
        {
            if (promocion.Origen == 22)
            {
                await _promocionesRepository.RelacionPromocionElectronica(resultado.AsuntoNeunId, resultado.NumeroOrden, _sesionService.SesionActual.CatOrganismoId, request.Promocion.Folio, request.Promocion.Origen, _sesionService.SesionActual.EmpleadoId, false);
            }
            else
            {
                await _promocionesRepository.RelacionPromocionElectronica(resultado.AsuntoNeunId, resultado.NumeroOrden, _sesionService.SesionActual.CatOrganismoId, request.Promocion.Folio, request.Promocion.Origen, _sesionService.SesionActual.EmpleadoId, null);
            }

            if (request.Promocion.ArchivoAVincular == null)
            {
                var seGenera = await GenerarArchivoPromocion(resultado.AsuntoNeunId, resultado.NumeroOrden, _sesionService.SesionActual.CatOrganismoId,
                   promocion.Origen.Value, request.Promocion.Fojas);
            }
            request.Promocion.ArchivoAVincular = true;

        }
        if (request.Promocion.ArchivoAVincular != null)
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
            var uri = _configuration["SISE3:BackEnd:SMTPTemplatesUrl"];
            var url = _configuration["SISE3:BackEnd:SMTPTemplatesVerPromoUrl"];
            var templateEmail = await _documentoBlob.ObtenerPlantillaCorreo("plantillaPromocionCorreo.html", "emailtemplates", uri);
            var registro = request.Promocion.Registro;
            var numeroExpediente = request.Promocion.NumeroExpediente;
            var tipoAsunto = request.Promocion.TipoAsunto?.TipoAsunto;
            var tipoProcedimiento = request.Promocion.TipoProcedimiento?.DESCRIPCION;
            var mesa = request.Promocion.Secretario?.Area;
            var fechaTurno = Judicatura.Common.Application.Utils.DateTimeUtil.ObtenerHoraLocal().ToString("dd/MM/yyyy");
            var horaTurno = Judicatura.Common.Application.Utils.DateTimeUtil.ObtenerHoraLocal().ToString("HH:mm");
            var fechaPresentacion = request.Promocion.FechaPresentacion;
            var horaPresentacion = request.Promocion.HoraPresentacion;

            var asuntoNeunId = resultado.AsuntoNeunId;
            var origen = request.Promocion.Origen;
            var numeroOrden = resultado.NumeroOrden;
            var yearPromocion = DateTimeUtil.ObtenerHoraLocal().Year.ToString();
            if (origen == 4) { origen = 0; }
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
                    OrganismoEmisor = _sesionService.SesionActual.CatOrganismoId.ToString(),
                },
                PersistirAlerta = true
            });

            var generarCorreoAlertaUtil = new GenerarCorreoAlertaUtil(_alertsMessageService);
            await generarCorreoAlertaUtil.GeneraCorreoAlertaPromocion(registro.ToString(), numeroExpediente, tipoAsunto, mesa, destinatarios, horaPresentacion, fechaPresentacion, tipoProcedimiento, fechaTurno, horaTurno, templateEmail, urlVerPromo);
            */
        }

        return resultado;
    }
    private async Task<bool> GenerarArchivoPromocion(long asuntoNeunId, int numeroOrden, int catOrganismoId, int origenPromocion, int? fojas)
    {
        var archivos = await _promocionesRepository.ObtenerArchivosyAnexos(asuntoNeunId, numeroOrden,
            DateTime.Now.Year, catOrganismoId, origenPromocion, 1, 0);

        byte[] archivoPromocion = null;
        List<byte[]> archivosElectronico = new List<byte[]>();
        foreach (var archivo in archivos)
        {
            var nas = _rutasChunkService.RutasChunkPorModuloAsync(archivo.RutaCompleta, RutasChunkModulos.Oficialia, asuntoNeunId, DateTime.Now.Year, numeroOrden);
            if (nas.IsCompleted && !string.IsNullOrEmpty(nas.Result.Base64))
                archivosElectronico.Add(Convert.FromBase64String(nas.Result.Base64));
        }
        archivoPromocion = _wordsUtil.MergePdf(archivosElectronico);
        var agregarDocumento = new AgregarDocumento()
        {
            AsuntoNeunId = asuntoNeunId,
            NumeroOrden = numeroOrden,
            YearPromocion = DateTime.Now.Year,
            RegistroEmpleadoId = _sesionService.SesionActual.EmpleadoId,
            Origen = origenPromocion,
            CatOrganismoId = catOrganismoId,
        };
        var datosDocumento = await _promocionesRepository.GuardarDocumento(agregarDocumento);
        agregarDocumento.NumeroConsecutivo = datosDocumento.NumeroConsecutivo;
        await _promocionesRepository.ActualizarArchivo(agregarDocumento);
        var rutaNas = await _promocionesRepository.RutaArchivo("Oficialia");
        _nas.AlmacenarArchivo(rutaNas + "\\" + catOrganismoId + "\\" + datosDocumento.NombreArchivo, archivoPromocion);
        return true;
    }
}
