using CJF.Sgcpj.Judicatura.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.CorreoMesa;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Alertas;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Exceptions;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Common.Application.Utils;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.SubirArchivoComando;
public class SubirArchivoCommando : IRequest<List<string>>
{
    public List<SubirArchivoDto> Archivos;
    public long AsuntoNeunId { get; set; }
    public int YearPromocion { get; set; }
    public int CatIdOrganismo { get; set; }
    public int NumeroOrden { get; set; }
    public int RegistroEmpleadoId { get; set; }
    public int Origen { get; set; }

    public int AsuntoID { get; set; }
    public int Consecutivo { get; set; }
    public int NumeroRegistro { get; set; }
    public short Fojas { get; set; }

    public string TipoAsunto { get; set; }
    public string TipoProcedimiento { get; set; }
    public string NumeroExpediente { get; set; }
    public string Mesa { get; set; }
    public string SecretarioId { get; set; }
    public bool EnviarAlerta { get; set; }
}

public class SubirArchivoComandoHandler : IRequestHandler<SubirArchivoCommando, List<string>>
{
    private readonly INasArchivo _nas;
    private readonly IPromocionesRepository _promocionesRepository;
    private readonly ISesionService _sesionService;
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;
    private readonly IAlertsMessageService _alertsMessageService;
    private readonly IDocumentoBlob _documentoBlob;
    private const int TipoDocumentoPromocion = 82;
    private string modulo = "Oficialia";

    public SubirArchivoComandoHandler(INasArchivo nas, IPromocionesRepository promocionesRepository,
                                      ISesionService sesionService, IMediator mediator,
                                      IConfiguration configuration, IAlertsMessageService alertsMessageService,
                                      IDocumentoBlob documentoBlob)
    {
        _nas = nas;
        _promocionesRepository = promocionesRepository;
        _sesionService = sesionService;
        _mediator = mediator;
        _configuration = configuration;
        _alertsMessageService = alertsMessageService;
        _documentoBlob = documentoBlob;
    }
    public async Task<List<string>> Handle(SubirArchivoCommando request, CancellationToken cancellationToken)
    {
        request.RegistroEmpleadoId = _sesionService.SesionActual.EmpleadoId;
        request.CatIdOrganismo = _sesionService.SesionActual.CatOrganismoId;
        List<string> resultado = new List<string>();
        List<string> archivosNoGuardados = new List<string>();
        var datosDocumento = new DatosDocumento();
        foreach (var archivo in request.Archivos)
        {
            try
            {
                var rutaNas = await _promocionesRepository.RutaArchivo(modulo);
                var agregarDocumento = new AgregarDocumento()
                {
                    AsuntoNeunId = request.AsuntoNeunId,
                    NumeroOrden = request.NumeroOrden,
                    YearPromocion = request.YearPromocion,
                    RegistroEmpleadoId = request.RegistroEmpleadoId,
                    Origen = request.Origen,
                    Caracter = archivo.Caracter,
                    Clase = archivo.Clase,
                    Descripcion = 5031,//Revisar el valor
                    CatOrganismoId = request.CatIdOrganismo,
                    NumeroRegistro = request.NumeroRegistro,
                    NumeroConsecutivo = request.Consecutivo,
                    Fojas = request.Fojas
                };
                datosDocumento = await _promocionesRepository.GuardarDocumento(agregarDocumento);
                agregarDocumento.NumeroConsecutivo = datosDocumento.NumeroConsecutivo;
                await _promocionesRepository.ActualizarArchivo(agregarDocumento);
                if (archivo.Data != null && archivo.Data.Length > 0)
                {
                    _nas.AlmacenarArchivo(rutaNas + "\\" + request.CatIdOrganismo.ToString() + "\\" + datosDocumento.NombreArchivo, archivo.Data);
                }

                resultado.Add(archivo.NombreArchivo);


            }
            catch (RuleException ex)
            {

                await Rollback(request, archivosNoGuardados, datosDocumento, archivo);
                throw;
            }
            catch (Exception ex)
            {
                await Rollback(request, archivosNoGuardados, datosDocumento, archivo);
                throw;
            }

        }

        if (request.EnviarAlerta)
        {
            /******************* Se comentan alertas por que esta hasta que se mande asincrono *****************/
            /*
            var correoMesa = await _mediator.Send(new CorreoMesaComando
            {
                CatOrganismoId = _sesionService.SesionActual.CatOrganismoId,
                EmpleadoIdResponsable = int.Parse(request.SecretarioId)
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

            var registro = request.NumeroRegistro;
            var numeroExpediente = request.NumeroExpediente;
            var tipoAsunto = request.TipoAsunto;
            var descripcion = request.TipoProcedimiento;
            var mesa = request.Mesa;

            var mensaje = $"La promoción {registro} del expediente {numeroExpediente} " +
                $"{tipoAsunto} {descripcion} ha sido asignada a la {mesa}";

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


            var uri = _configuration["SISE3:BackEnd:SMTPTemplatesUrl"];
            var templateEmail = await _documentoBlob.ObtenerPlantillaCorreo("plantillaPromocionCorreo.html", "emailtemplates", uri);

            TimeZoneInfo.ConvertTime(DateTime.Now,
                           TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)"));
            Dictionary<string, string> valores = new Dictionary<string, string>();
            valores.Add("@numeroPromocion", registro.ToString());
            valores.Add("@numeroExpediente", numeroExpediente);
            valores.Add("@mesa", mesa);
            valores.Add("@fecha", DateTimeUtil.ObtenerHoraLocal().ToString("dd/MM/yyyy HH:mm"));
            valores.Add("@currentYear", DateTimeUtil.ObtenerHoraLocal().Year.ToString());
            string body = string.Empty;
            try
            {
                body = ReemplazarCorreoValores(templateEmail, valores);
            }
            catch (Exception)
            {
                body = $"Se generó una nueva promoción - {DateTime.Now}";

            }

            await _alertsMessageService.TriggerAlertAsync(new AlertDTO<EmailAlertDTO>()
            {
                TipoDeAlerta = AlertType.Email,
                Destinatarios = destinatarios,
                Alerta = new EmailAlertDTO()
                {
                    Asunto = "Edición de promoción",
                    BodyCorreo = body
                }
            }); */
        }


        return await Task.FromResult(resultado);
    }

    private async Task Rollback(SubirArchivoCommando request, List<string> archivosNoGuardados, DatosDocumento datosDocumento, SubirArchivoDto archivo)
    {
        archivosNoGuardados.Add(archivo.NombreArchivo);


        var rollBackArchivo = new RollBackArchivo()
        {
            AsuntoNeunId = request.AsuntoNeunId,
            AsuntoID = 1,
            YearPromocion = request.YearPromocion,
            NumeroOrden = request.NumeroOrden,
            CatIdOrganismo = request.CatIdOrganismo,
            RegistroEmpleadoId = request.RegistroEmpleadoId,
            NumeroRegistro = request.NumeroRegistro,
            Consecutivo = datosDocumento.NumeroConsecutivo,
            Origen = request.Origen
        };
        await _promocionesRepository.RollBackArchivo(rollBackArchivo);
    }

    private string ReemplazarCorreoValores(string templaeEmail, Dictionary<string, string> valores)
    {
        foreach (var item in valores)
        {
            templaeEmail = templaeEmail.Replace(item.Key, item.Value);
        }
        return templaeEmail;
    }
}
