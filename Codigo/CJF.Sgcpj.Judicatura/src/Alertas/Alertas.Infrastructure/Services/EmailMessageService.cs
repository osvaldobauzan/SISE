using System.Net;
using System.Net.Mail;
using CJF.Sgcpj.Judicatura.Alertas.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Infrastructure.Services;

public class EmailMessageService : IEmailMessage
{
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;

    public EmailMessageService(IConfiguration configuration, ILogger logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<(bool IsSuccess, string? ErrorMessage)> SendEmail(string to, string subject, string body)
    {
        try
        {
            using var smtp = new SmtpClient();

            smtp.Port = int.Parse(_configuration["SISE3:BackEnd:SMTPPort"]);
            smtp.Host = _configuration["SISE3:BackEnd:SMTPIp"];

            var mail = new MailMessage();

            mail.From = new MailAddress(_configuration["SISE3:BackEnd:SMTPFrom"]);
            mail.IsBodyHtml = true;
            mail.To.Add(new MailAddress(to));
            mail.Subject = subject;
            mail.Priority = MailPriority.High;
            mail.Body = body;

            await smtp.SendMailAsync(mail);
            return (true, null);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex.ToString());
            return (false, ex.Message);
        }
    }
}
