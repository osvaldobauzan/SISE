namespace CJF.Sgcpj.Judicatura.Alertas.Application.Common.Interfaces;

public interface IEmailTemplatesService
{
    Task<(bool IsSuccess, string? Template, string? ErrorMessage)> GetTemplateAsync(string templateId);
}
