using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Alertas;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Vml;
using MimeKit;

namespace CJF.Sgcpj.Judicatura.Common.Application.Utils
{
    public class GenerarCorreoAlertaUtil
    {
        private readonly IAlertsMessageService _alertsMessageService;

        public GenerarCorreoAlertaUtil(IAlertsMessageService alertsMessageService)
        {
            _alertsMessageService = alertsMessageService;

        }

        public async Task<bool> GeneraCorreoAlertaPromocion(
            string registro,
            string numeroExpediente,
            string tipoAsunto,
            string mesa,
            List<Destinatario> destinatarios,
            string horaPresentacion,
            string fechaPresentacion,
            string tipoProcedimiento,
            string fechaTurno,
            string horaTurno,
            string templateEmail,
            string urlVerPromo
            )
        {
            string tipoProcedimientoClase = string.IsNullOrEmpty(tipoProcedimiento) ? "" : "Tipo de Procedimiento:";
            Dictionary<string, string> valores = new Dictionary<string, string>
            {
                ["@numeroPromocion"] = registro.ToString(),
                ["@numeroExpediente"] = numeroExpediente + " " + tipoAsunto,
                ["@mesa"] = mesa,
                ["@fechaPresentacion"] = fechaPresentacion + " " + horaPresentacion,
                ["@tipoProcedimiento"] = tipoProcedimiento,
                ["@fechaTurno"] = fechaTurno + " " + horaTurno,
                ["@currentYear"] = DateTimeUtil.ObtenerHoraLocal().Year.ToString(),
                ["@urlVerPromo"] = urlVerPromo,
                ["@tituloTipoProcedimiento"] = tipoProcedimientoClase
        };

            string body;
            
            try
            {
                body = ReemplazarCorreoValores(templateEmail, valores);
            }
            catch (Exception)
            {
                body = $"Se generó una nueva promoción - {DateTime.Now}";
            }

            await _alertsMessageService.TriggerAlertAsync(new AlertDTO<EmailAlertDTO>
            {
                TipoDeAlerta = AlertType.Email,
                Destinatarios = destinatarios,
                Alerta = new EmailAlertDTO
                {
                    Asunto = "Nueva promoción",
                    BodyCorreo = body
                }
            });

            return true;
        }

        private string ReemplazarCorreoValores(string templateEmail, Dictionary<string, string> valores)
        {
            foreach (var (key, value) in valores)
            {
                templateEmail = templateEmail.Replace(key, value);
            }

            return templateEmail;
        }
    }
}
