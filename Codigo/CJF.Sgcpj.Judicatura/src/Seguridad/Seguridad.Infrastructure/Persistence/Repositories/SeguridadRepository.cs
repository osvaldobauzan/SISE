using CJF.Sgcpj.Judicatura.Common.Application.Common.Exceptions;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Persistence;
using CJF.Sgcpj.Judicatura.Seguridad.Application.Common.Repositories;
using Microsoft.Data.SqlClient;
using PolyCache.Cache;

namespace CJF.Sgcpj.Judicatura.Seguridad.Infrastructure.Persistence.Repositories;
public class SeguridadRepository : ISeguridadRepository
{
    private const int TtlCache = 180;
    private readonly IStaticCacheManager _staticCacheManager;
    private readonly IUserConnectionsHandler _connectionsHandler;
    private readonly ApplicationDbContext _dbContext;

    const string cacheSesionPrefix = "sesion_";

    public SeguridadRepository(IStaticCacheManager staticCacheManager,
                               IUserConnectionsHandler connectionsHandler,
                               ApplicationDbContext dbContext)
    {
        _staticCacheManager = staticCacheManager;
        _connectionsHandler = connectionsHandler;
        _dbContext = dbContext;
    }
    public async Task<Sesion> ObtenerDatosSesion(int empleadoId, int catOrganismoId)
    {
        Sesion resultado;
        resultado = await ObtenerSesionAsync(empleadoId, catOrganismoId);
        return resultado;
    }

    private async Task<Sesion> ObtenerSesionAsync(int empleadoId, int catOrganismoId)
    {
        List<SqlParameter> procedimientoParametros = new List<SqlParameter>();
        SqlParameter? pi_EmpleadoId = new SqlParameter("@pi_EmpleadoId", empleadoId);
        SqlParameter? pi_OrganismoId = new SqlParameter("@pi_CatOrganismoId", catOrganismoId);

        procedimientoParametros.Add(pi_EmpleadoId);
        procedimientoParametros.Add(pi_OrganismoId);

        var itemResulSet = await _dbContext.ExecuteStoredProc<Sesion>("[SISE3].[pcDatosUsuario]", procedimientoParametros.ToArray());
        Sesion? retSession = !itemResulSet.Any() ? null : itemResulSet.SingleOrDefault();

        

        if (retSession == null)
            throw new NotFoundException($"La cuenta de usuario no existe");
        retSession.Privilegios = await ObtenerPrivilegios(empleadoId, catOrganismoId);

        return retSession;
    }
    private static string LlaveCache(string cachePrexi, string sesionId)
    {
        return cachePrexi + sesionId;
    }

    public async Task<bool> CerrarSesion(int idEmpleado, string refreshToken)
    {
        bool resultado = false;
        var llaveCache = new CacheKey(LlaveCache(cacheSesionPrefix, idEmpleado.ToString()));
        var sesiones = await _staticCacheManager.GetAsync<List<Sesion>>(llaveCache);
        var sesionACerrar = sesiones.FirstOrDefault(s => s.RefrehToken == refreshToken);
        if (sesionACerrar == null)
        {
            throw new ForbiddenAccessException();
        }
        else
        {
            await _connectionsHandler.RemoveConnectionAsync(sesionACerrar.ConnectionId, sesionACerrar.EmpleadoId.ToString());

            sesiones.RemoveAll(sesion => sesion.ExpiracionUtc < DateTime.UtcNow);
            sesiones.Remove(sesionACerrar);

            if (sesiones.Any())
            {
                await _staticCacheManager.SetAsync(llaveCache, sesiones);
            }
            else
            {
                await _staticCacheManager.RemoveAsync(llaveCache);
            }
        }

        resultado = true;
        return resultado;

    }

    public async Task<bool> RefrescarSesion(int idEmpleado, string nonce, string refreshToken)
    {
        bool resultado = false;
        var llaveCache = new CacheKey(LlaveCache(cacheSesionPrefix, idEmpleado.ToString()));
        llaveCache.CacheTime = TtlCache;
        var sesiones = await _staticCacheManager.GetAsync<List<Sesion>>(llaveCache);

        if (sesiones == null || !sesiones.Any())
        {
            throw new ForbiddenAccessException();
        }
        var sesionARefrescar = sesiones.FirstOrDefault(s => s.RefrehToken == refreshToken);
        if (sesionARefrescar == null)
        {
            throw new ForbiddenAccessException();
        }
        else
        {
            sesionARefrescar.ExpiracionUtc = DateTime.UtcNow.AddMinutes(TtlCache);
            sesionARefrescar.Nonce = nonce;

            sesiones.RemoveAll(sesion => sesion.ExpiracionUtc < DateTime.UtcNow);
            await _staticCacheManager.SetAsync(llaveCache, sesiones);
        }

        await _connectionsHandler.RefreshSessionAsync(sesionARefrescar.EmpleadoId.ToString(),
                                                      sesionARefrescar.ConnectionId,
                                                      TtlCache);

        resultado = true;

        return resultado;
    }

    public async Task<bool> IniciarSesion(Sesion sesion)
    {
        bool resultado = false;
        var llaveCache = new CacheKey(LlaveCache(cacheSesionPrefix, sesion.EmpleadoId.ToString()));

        var sesiones = await _staticCacheManager.GetAsync<List<Sesion>>(llaveCache);
        sesion.ExpiracionUtc = DateTime.UtcNow.AddMinutes(TtlCache);
        if (sesiones == null)
        {
            sesiones = new List<Sesion>();
        }
        else
        {
            sesiones.RemoveAll(sesion => sesion.ExpiracionUtc < DateTime.UtcNow);
        }
        sesiones.Add(sesion);
        llaveCache.CacheTime = TtlCache;
        await _staticCacheManager.SetAsync(llaveCache, sesiones);
        resultado = true;

        return resultado;
    }

    private async Task<List<int>> ObtenerPrivilegios(int empleadoId, int catOrganismoId)
    {
        List<SqlParameter> procedimientoParametros = new List<SqlParameter>();
        SqlParameter? pi_EmpleadoId = new SqlParameter("@pi_EmpleadoId", empleadoId);
        SqlParameter? pi_OrganismoId = new SqlParameter("@pi_CatOrganismoId", catOrganismoId);

        procedimientoParametros.Add(pi_EmpleadoId);
        procedimientoParametros.Add(pi_OrganismoId);

        var itemResulSet = await _dbContext.ExecuteStoredProc<Privilegio>("[SISE3].[pcPrivilegiosPorUsuario]", procedimientoParametros.ToArray());
        return itemResulSet.Select( x => x.IdPrivilegio).ToList();
    }
}
