using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Alertas;
public interface IAlertsMessageService
{
    Task<(bool IsSuccess, string? ErrorMessage)> TriggerAlertAsync<T>(AlertDTO<T> alert) where T : IValidAlertMessages;
}
