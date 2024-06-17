using CJF.Sgcpj.Judicatura.Common.Application.Common.Alertas;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.ServiceMocks;
internal class UserConnectionsHandlerMock : IUserConnectionsHandler
{
    public async Task<(bool IsSuccess, IEnumerable<UserConnection>? Results, string? ErrorMessage)> QueryConnectionAsync(string userId)
    {
        return (true, new List<UserConnection>()
        {
            new UserConnection()
            {
                ConnectionId = "ConnectionIdDummy", 
                ExpirationInUtc = DateTime.UtcNow.AddDays(1),
                OrganismoId = "180"
            }
        }, null);
    }

    public async Task<(bool IsSuccess, string? ErrorMessage)> RefreshSessionAsync(string userId, string connectionId, int timeToLive)
    {
        return (true, null);
    }

    public async Task<(bool IsSuccess, string? ErrorMessage)> RegisterConnectionAsync(string userId, string connectionId, string organismoId)
    {
        return (true, null);
    }

    public async Task<(bool IsSuccess, string? ErrorMessage)> RemoveConnectionAsync(string connectionId, string userId)
    {
        return (true, null);
    }
}
