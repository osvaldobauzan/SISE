using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using Duende.IdentityServer.Models;
using PolyCache.Cache;

namespace CJF.Sgcpj.Judicatura.Promovente.Infrastructure.Services;
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

    Sesion? ISesionService.SesionActual => GetSesionRedis();

    private Sesion? GetSesionRedis()
    {
        var sesiones = _staticCacheManager.GetAsync<List<Sesion>>(_cacheKey).ConfigureAwait(false).GetAwaiter().GetResult();
        if (_cacheKey != null)
        {
            if (sesiones != null)
            {
                return sesiones.FirstOrDefault(s => s.Nonce == _currentUserService.Nonce.ToString());
            }
        }

        return null;
    }
}
