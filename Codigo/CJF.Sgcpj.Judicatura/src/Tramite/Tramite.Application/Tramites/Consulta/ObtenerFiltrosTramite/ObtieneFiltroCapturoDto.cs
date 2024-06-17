using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerFiltrosTramite;
public class ObtieneFiltroCapturoDto : IMapFrom<ObtieneFiltroCapturo>
{
    public string? Capturo { get; set; }
    public string? UserName { get; set; }
    public long? EmpleadoId { get; set; }
    public int? CatOrganismoId { get; set; }
}
