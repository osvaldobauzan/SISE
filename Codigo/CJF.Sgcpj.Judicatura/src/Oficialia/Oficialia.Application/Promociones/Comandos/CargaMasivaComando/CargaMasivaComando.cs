using System.Text;
using CJF.Sgcpj.Judicatura.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.CargaMasivaComando;
using CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.CorreoMesa;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Alertas;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Exceptions;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Common.Application.Utils;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Promociones.Comandos.CargaMasivaComando;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Promociones.Consulta.ObtenerDetalleCargaMasiva;
using CJF.Sgcpj.Judicatura.Oficialia.Domain.Entities;
using DocumentFormat.OpenXml.Presentation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.SubirArchivoComando;
public class CargaMasivaComando : IRequest<List<ResultadoCargaMasivaArchivoDto>>
{
    public List<CargaMasivaArchivoDto> Archivos;
    public int YearPromocion { get; set; }
    public int NumeroRegistro { get; set; }
    public int CatOrganismoId { get; set; }
    public string NombreArchivoReal { get; set; }
    public int RegistroEmpleadoId { get; set; }
    public int NumeroConsecutivo { get; set; }
    public string NombreArchivo { get; set; }
    public string Mensaje { get; set; }
    public string ExpedienteProcesado { get; set; }
    public List<PromocionesValidasDto> PromocionesValidas;
}

public class CargaMasivaComandoHandler : IRequestHandler<CargaMasivaComando, List<ResultadoCargaMasivaArchivoDto>>
{
    private readonly INasArchivo _nas;
    private readonly IPromocionesRepository _promocionesRepository;
    private readonly ISesionService _sesionService;
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;
    private readonly IAlertsMessageService _alertsMessageService;
    private readonly IDocumentoBlob _documentoBlob;
    private string Modulo = "Oficialia";

    public CargaMasivaComandoHandler(INasArchivo nas,
                                     IPromocionesRepository promocionesRepository,
                                     ISesionService sesionService,
                                     IMediator mediator,
                                     IConfiguration configuration,
                                     IAlertsMessageService alertsMessageService,
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
    public async Task<List<ResultadoCargaMasivaArchivoDto>> Handle(CargaMasivaComando request, CancellationToken cancellationToken)
    {
        List<ResultadoCargaMasivaArchivoDto> resultado = new List<ResultadoCargaMasivaArchivoDto>();
        List<string> archivosNoGuardados = new List<string>();
        var datosDocumento = new DatosDocumento();
        var rutaNas = await _promocionesRepository.RutaArchivo(Modulo);
        foreach (var archivo in request.Archivos)
        {

            ResultadoCargaMasivaArchivoDto resultadoArchivo = new ResultadoCargaMasivaArchivoDto
            {
                NumeroRegistro = archivo.NombreArchivo
            };
            try
            {
                var cargaMasivaModel = new CargaMasiva()
                {
                    YearPromocion = request.YearPromocion,
                    NumeroRegistro = Convert.ToInt32(archivo.NombreArchivo.Split('.').First()),
                    CatOrganismoId = _sesionService.SesionActual.CatOrganismoId,
                    NombreArchivoReal = request.NombreArchivoReal,
                    RegistroEmpleadoId = _sesionService.SesionActual.EmpleadoId
                };
                foreach (var promociones in request.PromocionesValidas)
                {
                    //if(promociones.NumeroRegistro == cargaMasivaModel.NumeroRegistro && promociones.ConArchivo == false && promociones.YearPromocion == cargaMasivaModel.YearPromocion)
                    if (promociones.NumeroRegistro == cargaMasivaModel.NumeroRegistro && promociones.YearPromocion == cargaMasivaModel.YearPromocion)
                    {
                        datosDocumento = await _promocionesRepository.GuardarCargaMasiva(cargaMasivaModel);
                        _nas.AlmacenarArchivo(rutaNas + "\\" + cargaMasivaModel.CatOrganismoId + "\\" + datosDocumento.NombreArchivo, archivo.Data);
                        resultadoArchivo.Correcto = true;
                        resultadoArchivo.ExpedienteProcesado = datosDocumento.ExpedienteProcesado;

                        /******************* Se comentan alertas por que esta hasta que se mande asincrono *****************/
                        /*
                                    var datosAlerta = await _promocionesRepository.ObtenerDetalleAlerta(new ObtenerDetalleCargaMasivaRequest()
                                    {
                                        CatOrganismoId = _sesionService.SesionActual.CatOrganismoId,
                                        NumeroRegistro = Convert.ToInt32(archivo.NombreArchivo.Split('.').First()),
                                        YearPromocion = request.YearPromocion
                                    });

                                    var correoMesa = await _mediator.Send(new CorreoMesaComando
                                    {
                                        CatOrganismoId = _sesionService.SesionActual.CatOrganismoId,
                                        EmpleadoIdResponsable = datosAlerta.SecretarioId
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

                                    var mensaje = $"La promoción {datosAlerta.NumeroRegistro} del expediente {datosAlerta.NumeroExpediente} " +
                                        $"{datosAlerta.TipoAsunto} {datosAlerta.TipoProcedimiento} ha sido asignada a la {datosAlerta.Mesa}";

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
                                    var url = _configuration["SISE3:BackEnd:SMTPTemplatesVerPromoUrl"];
                                    var templateEmail = await _documentoBlob.ObtenerPlantillaCorreo("plantillaPromocionCorreo.html", "emailtemplates", uri);
                                    string tipoProcedimientoClase = string.IsNullOrEmpty(datosAlerta.TipoProcedimiento.ToString()) ? "" : "Tipo de Procedimiento:";
                                    var urlVerPromocion = "&asuntoNeunId=" + datosAlerta.NumeroRegistro + "&origen=" + datosDocumento.NumeroConsecutivo + "&numeroOrden=" + datosDocumento.NumeroOrden + "&yearPromocion=" + promociones.YearPromocion;
                                    var urlVerPromo = url + Convert.ToBase64String(Encoding.UTF8.GetBytes(urlVerPromocion));
                                    TimeZoneInfo.ConvertTime(DateTime.Now,
                                                    TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)"));
                                    Dictionary<string, string> valores = new Dictionary<string, string>();
                                    valores.Add("@numeroPromocion", datosAlerta.NumeroRegistro.ToString());
                                    valores.Add("@numeroExpediente", datosAlerta.NumeroExpediente);
                                    valores.Add("@tipoProcedimiento", datosAlerta.TipoProcedimiento.ToString());
                                    valores.Add("tituloTipoProcedimiento", tipoProcedimientoClase);
                                    valores.Add("@fechaTurno", Judicatura.Common.Application.Utils.DateTimeUtil.ObtenerHoraLocal().ToString("dd/MM/yyyy HH:mm"));
                                    valores.Add("@mesa", datosAlerta.Mesa);
                                    valores.Add("@fechaPresentacion", Judicatura.Common.Application.Utils.DateTimeUtil.ObtenerHoraLocal().ToString("dd/MM/yyyy HH:mm"));
                                    valores.Add("@currentYear", Judicatura.Common.Application.Utils.DateTimeUtil.ObtenerHoraLocal().Year.ToString());
                                    valores.Add("@urlVerPromo", urlVerPromo);
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
                                    });
                                    */
                    }
                    else
                    {
                        resultadoArchivo.Mensaje = "No se asignó la promoción " + cargaMasivaModel.NumeroRegistro;
                    }
                }



            }
            catch (RuleException ex)
            {
                resultadoArchivo.Mensaje = ex.Mensaje.ToString();
            }
            catch (Exception ex)
            {
                if (ex is HttpRequestException || ex is System.Net.Sockets.SocketException)
                {
                    resultadoArchivo.Mensaje = "Error de red. Verifica tu conexión e inténtalo nuevamente.";
                }
                else
                {
                    resultadoArchivo.Mensaje = "Promoción inexistente";
                }
            }

            finally
            {
                resultado.Add(resultadoArchivo);
            }
        }

        return await Task.FromResult(resultado);
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
