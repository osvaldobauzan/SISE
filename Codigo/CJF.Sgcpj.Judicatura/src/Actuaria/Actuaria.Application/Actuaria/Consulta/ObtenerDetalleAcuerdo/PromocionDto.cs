using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerDetalleAcuerdo;
public class PromocionDto : IMapFrom<Promociones>
{
    public string Promocion { get; set; }
}
