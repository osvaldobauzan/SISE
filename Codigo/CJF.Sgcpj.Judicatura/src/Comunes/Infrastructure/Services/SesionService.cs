using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using PolyCache.Cache;

namespace CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
public class SesionService : ISesionService
{
    private readonly IStaticCacheManager _staticCacheManager;
    private readonly ICurrentUserService _currentUserService;
    private readonly CacheKey _cacheKey;
    public SesionService(IStaticCacheManager staticCacheManager, ICurrentUserService currentUserService)
    {
        _staticCacheManager = staticCacheManager;
        _currentUserService = currentUserService;
        if (currentUserService != null && currentUserService.EmpleadoId != null)
        {
            _cacheKey = new CacheKey("sesion_" + currentUserService.EmpleadoId.ToString());
        }


    }
    Sesion? _sesion;
    Sesion? ISesionService.SesionActual => GetSesionRedis();

    private Sesion? GetSesionRedis()
    {
        if (_sesion == null)
        {
            var sesiones = _staticCacheManager.GetAsync<List<Sesion>>(_cacheKey).ConfigureAwait(false).GetAwaiter().GetResult();
            if (_cacheKey != null)
            {
                if (sesiones != null)
                {
                    _sesion = sesiones.FirstOrDefault(s => s.Nonce == _currentUserService.Nonce.ToString());
                    return _sesion;
                }
            }
        }
        else
        {
            return _sesion;
        }
        return null;
    }
}
