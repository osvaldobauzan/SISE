using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Seguridad.Application.Common.Repositories;
public interface ISeguridadRepository
{
    Task<bool> CerrarSesion(int idEmpleado, string refreshToken);
    Task<Sesion> ObtenerDatosSesion(int empleadoId, int catOrganismoId);
    Task<bool> IniciarSesion(Sesion sesion);
    Task<bool> RefrescarSesion(int idEmpleado, string nonce, string refreshToken);
}
