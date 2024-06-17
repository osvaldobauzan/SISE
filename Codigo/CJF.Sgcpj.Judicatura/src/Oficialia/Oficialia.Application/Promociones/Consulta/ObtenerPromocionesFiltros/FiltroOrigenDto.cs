
using CJF.Sgcpj.Judicatura.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Oficialia.Application.Promociones.Consulta.ObtenerPromocionesFiltros;
public class FiltroOrigenDto : IMapFrom<FiltroOrigen>
{
    public string sNombreOrigenPromocion { get; set; }
}
