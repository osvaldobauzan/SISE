namespace CJF.Sgcpj.Judicatura.Alertas.Application.Common.Interfaces;
public interface IEmailMessage
{
    Task<(bool IsSuccess, string? ErrorMessage)> SendEmail(string to, string subject, string body);
}
