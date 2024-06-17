using CJF.Sgcpj.Judicatura.Common.Application.Common.Alertas;

namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
public interface IUserConnectionsHandler
{
    Task<(bool IsSuccess, IEnumerable<UserConnection>? Results, string? ErrorMessage)> QueryConnectionAsync(string userId);
    Task<(bool IsSuccess, string? ErrorMessage)> RegisterConnectionAsync(string userId, string connectionId, string organismoId);
    Task<(bool IsSuccess, string? ErrorMessage)> RemoveConnectionAsync(string connectionId, string userId);
    Task<(bool IsSuccess, string? ErrorMessage)> RefreshSessionAsync(string userId, string connectionId, int timeToLive);
}
