using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Seguridad.Application.Seguridad.Comandos.CrearSesion;
public class SesionDto : IMapFrom<Sesion>
{
    public SesionDto()
    {
        Privilegios = new List<int>();
    }
    public int EmpleadoId { get; set; }
    public string? Completo { get; set; }
    public int CargoId { get; set; }
    public string? CargoDescripcion { get; set; }
    public int? idRolCat { get; set; }
    public string? paginaInicio { get; set; }
    public int CatOrganismoId { get; set; }
    public string? NombreOficial { get; set; }
    public int CatTipoOrganismoId { get; set; }
    public string? EMail { get; set; }
    public string ConnectionId { get; set; }
    public List<int> Privilegios { get; set; }
    public string Nonce { get; set; }
    public string RefrehToken { get; set; }
}
