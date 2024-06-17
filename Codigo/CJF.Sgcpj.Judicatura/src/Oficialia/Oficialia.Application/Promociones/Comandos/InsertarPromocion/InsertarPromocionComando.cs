using System;
using System.Text;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.CorreoMesa;
using CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.SubirArchivoComando;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Alertas;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Common.Application.Utils;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Utilities;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.InsertarPromocion;
public class InsertarPromocionComando : IRequest<int>
{
    public InsertarPromocionDto Promocion { get; set; }
}
public class InsertarPromocionComandoHandler : IRequestHandler<InsertarPromocionComando, int>
{
    private readonly IMapper _mapper;
    private readonly IPromocionesRepository _promocionesRepository;
    private readonly ISesionService _sesionService;
    private readonly IMediator _mediator;
    private readonly IAlertsMessageService _alertsMessageService;
    private readonly IDocumentoBlob _documentoBlob;
    private readonly IConfiguration _configuration;
    private readonly IRutasChunkService _rutasChunkService;
    private readonly IWordsUtil _wordsUtil;
    private readonly INasArchivo _nas;
    private readonly ILogger<InsertarPromocionComandoHandler> _logger;
    public InsertarPromocionComandoHandler(IMapper mapper, IPromocionesRepository promocionesRepository,
                                            ISesionService sesionService, IMediator mediator,
                                            IAlertsMessageService alertsMessageService, IDocumentoBlob documentoBlob,
                                            IConfiguration configuration, IRutasChunkService rutasChunkService,
                                            IWordsUtil wordsUtil, INasArchivo nas,
                                            ILogger<InsertarPromocionComandoHandler> logger)
    {
        _mapper = mapper;
        _promocionesRepository = promocionesRepository;
        _sesionService = sesionService;
        _mediator = mediator;
        _alertsMessageService = alertsMessageService;
        _documentoBlob = documentoBlob;
        _configuration = configuration;
        _rutasChunkService = rutasChunkService;
        _wordsUtil = wordsUtil;
        _nas = nas;
        _logger = logger;
    }

    public async Task<int> Handle(InsertarPromocionComando request, CancellationToken cancellationToken)
    {
        request.Promocion.RegistroEmpleadoId = _sesionService.SesionActual.EmpleadoId;
        var uri = _configuration["SISE3:BackEnd:SMTPTemplatesUrl"];
        var url = _configuration["SISE3:BackEnd:SMTPTemplatesVerPromoUrl"];
        var promocion = _mapper.Map<Common.Models.InsertarPromocion>(request.Promocion);
        var promocionResult = await _promocionesRepository.InsertarPromocion(promocion);
        if (promocion.OrigenPromocion != 4) // 4 son promociones de oficialia
        {
            if (promocion.OrigenPromocion == 22 && request.Promocion.ConExpedienteElectronico)
            {
                await _promocionesRepository.RelacionPromocionElectronica(request.Promocion.AsuntoNeunId, promocionResult, _sesionService.SesionActual.CatOrganismoId, request.Promocion.Folio, request.Promocion.Origen, _sesionService.SesionActual.EmpleadoId, true);
            }
            else
            {
                await _promocionesRepository.RelacionPromocionElectronica(request.Promocion.AsuntoNeunId, promocionResult, _sesionService.SesionActual.CatOrganismoId, request.Promocion.Folio, request.Promocion.Origen, _sesionService.SesionActual.EmpleadoId, null);
            }

            var seGenera = await GenerarArchivoPromocion(request.Promocion.AsuntoNeunId, promocionResult, _sesionService.SesionActual.CatOrganismoId,
                 request.Promocion.Origen.Value, request.Promocion.Fojas);

            request.Promocion.ArchivoAVincular = true;
        }
        if (request.Promocion.ArchivoAVincular)
        {
            /******************* Se comentan alertas por que esta hasta que se mande asincrono *****************/
            /*
            var correoMesa = await _mediator.Send(new CorreoMesaComando { CatOrganismoId = _sesionService.SesionActual.CatOrganismoId, EmpleadoIdResponsable = promocion.Secretario });
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

            var registro = promocion.NumeroRegistro;
            var numeroExpediente = request.Promocion.NumeroExpediente;
            var tipoAsunto = request.Promocion.TipoAsunto;
            var tipoProcedimiento = request.Promocion.TipoProcedimiento;
            var mesa = request.Promocion.Mesa;
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

            TimeZoneInfo.ConvertTime(DateTime.Now,
                           TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)"));

            var generarCorreoAlertaUtil = new GenerarCorreoAlertaUtil(_alertsMessageService);


            await generarCorreoAlertaUtil.GeneraCorreoAlertaPromocion(registro.ToString(), numeroExpediente, tipoAsunto, mesa, destinatarios, horaPresentacion, fechaPresentacion, tipoProcedimiento, fechaTurno, horaTurno, templateEmail, urlVerPromo);

            */
        }
        return promocionResult;
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
        var agregarDocumento = new Common.Models.AgregarDocumento()
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