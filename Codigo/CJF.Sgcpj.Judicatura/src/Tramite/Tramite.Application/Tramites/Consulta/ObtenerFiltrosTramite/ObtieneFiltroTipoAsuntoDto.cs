using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerFiltrosTramite;
public class ObtieneFiltroTipoAsuntoDto : IMapFrom<ObtieneFiltroTipoAsunto>
{
    public int? CatTipoAsuntoId { get; set; }
    public string? TipoAsunto { get; set; }
    public string? TipoAsuntoCorto { get; set; }
    public string? Color { get; set; }
    public int? CuadernoId { get; set; }
    public string? Cuaderno { get; set; }
}
