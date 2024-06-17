using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerFiltrosTramite;
public class ObtieneFiltroOrigenDto : IMapFrom<ObtieneFiltroOrigen>
{
    public string? SNombreOrigenPromocion { get; set; }
    public int? Origen { get; set; }
}
