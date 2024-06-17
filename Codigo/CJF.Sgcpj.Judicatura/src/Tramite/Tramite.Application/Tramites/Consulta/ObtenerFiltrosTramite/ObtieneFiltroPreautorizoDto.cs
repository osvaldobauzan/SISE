using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerFiltrosTramite;
public class ObtieneFiltroPreautorizoDto : IMapFrom<ObtieneFiltroPreautorizo>
{
    public string? Preautorizo { get; set; }
    public string? UserName { get; set; }
    public long? EmpleadoId { get; set; }
    public int? CatOrganismoId { get; set; }
}
