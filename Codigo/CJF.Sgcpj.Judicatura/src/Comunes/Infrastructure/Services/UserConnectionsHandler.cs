using CJF.Sgcpj.Judicatura.Common.Application.Common.Alertas;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Exceptions;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using Microsoft.Extensions.Logging;
using PolyCache.Cache;

namespace CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
public class UserConnectionsHandler : IUserConnectionsHandler
{
    private readonly IStaticCacheManager _staticCacheManager;
    private readonly ISesionService _sesionService;
    private readonly ILogger<UserConnectionsHandler> _logger;
    private const string sesionPrefix = "sesion_";
    private const string cacheConnectionsPrefix = "connections_";

    public UserConnectionsHandler(IStaticCacheManager staticCacheManager,
                                  ISesionService sesionService,   
                                  ILogger<UserConnectionsHandler> logger)
    {
        _staticCacheManager = staticCacheManager;
        _sesionService = sesionService;
        _logger = logger;
    }

    public async Task<(bool IsSuccess, IEnumerable<UserConnection>? Results, string? ErrorMessage)> QueryConnectionAsync(string userId)
    {
        try
        {
            var llaveCache = new CacheKey(LlaveCache(cacheConnectionsPrefix, userId));
            var connections = await _staticCacheManager.GetAsync<List<UserConnection>>(llaveCache);

            if (connections != null && connections.Any())
            {
                connections = connections.GroupBy(c => c.ConnectionId)
                                         .Select(c => c.First())
                                         .ToList();
            }

            return (true, connections, null);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex.ToString());
            return (false, null, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, string? ErrorMessage)> RegisterConnectionAsync(string userId, string connectionId, string organismoId)
    {
        try
        {
            //Conexiones
            var llaveCache = new CacheKey(LlaveCache(cacheConnectionsPrefix, userId));
            var cache = await _staticCacheManager.GetAsync<List<UserConnection>>(llaveCache);

            if (cache is null)
            {
                cache = new List<UserConnection>();
            }

            cache.Add(new UserConnection()
            {
                ConnectionId = connectionId,
                OrganismoId = organismoId
            });

            llaveCache.CacheTime = 180;
            await _staticCacheManager.SetAsync(llaveCache, cache);


            //Sesion
            var llaveCacheSesion = new CacheKey(LlaveCache(sesionPrefix, userId));
            var sesiones = await _staticCacheManager.GetAsync<List<Sesion>>(llaveCacheSesion);

            if (sesiones == null || !sesiones.Any())
            {
                throw new ForbiddenAccessException();
            }
            var sesionARefrescar = sesiones.FirstOrDefault(s => s.RefrehToken == _sesionService.SesionActual.RefrehToken);
            if (sesionARefrescar == null)
            {
                throw new ForbiddenAccessException();
            }
            else
            {
                sesionARefrescar.ConnectionId = connectionId;
                llaveCacheSesion.CacheTime = 180;
                await _staticCacheManager.SetAsync(llaveCacheSesion, sesiones);
            }

            return (true, null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return (false, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, string? ErrorMessage)> RemoveConnectionAsync(string connectionId, string userId)
    {
        try
        {
            var llaveCache = new CacheKey(LlaveCache(cacheConnectionsPrefix, userId));
            var connections = await _staticCacheManager.GetAsync<List<UserConnection>>(llaveCache);

            if (connections is null)
            {
                return (true, "No cache found");
            }

            connections.RemoveAll(connections => connections.ConnectionId == connectionId);

            if (!connections.Any())
            {
                await _staticCacheManager.RemoveAsync(llaveCache);
            }
            else
            {
                await _staticCacheManager.SetAsync(llaveCache, connections);
            }

            return (true, null);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex.ToString());
            return (false, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, string? ErrorMessage)> RefreshSessionAsync(string userId, string connectionId, int timeToLive)
    {
        try
        {
            var llaveCache = new CacheKey(LlaveCache(cacheConnectionsPrefix, userId));
            var connections = await _staticCacheManager.GetAsync<List<UserConnection>>(llaveCache);

            if (connections is null)
            {
                return (false, "No se encontró caché");
            }

            var sesionARefrescar = connections.FirstOrDefault(s => s.ConnectionId == connectionId);

            if(sesionARefrescar is null)
            {
                return (false, "No se encontró sesión");
            }

            sesionARefrescar.ExpirationInUtc = DateTime.UtcNow.AddMinutes(timeToLive);
            connections.RemoveAll(c => c.ExpirationInUtc < DateTime.UtcNow);

            llaveCache.CacheTime = timeToLive;
            await _staticCacheManager.SetAsync(llaveCache, connections);

            return (true, null);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex.ToString());
            return (false, ex.Message);
        }
    }
    private string LlaveCache(string cachePrexi, string sesionId)
    {
        return cachePrexi + sesionId;
    }
}
