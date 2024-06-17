using CJF.Sgcpj.Judicatura.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerNumeroPromocion;
public class ObtieneNumeroPromocionDto : IMapFrom<NumeroPromocion>
{
    public int Existe { get; set; }
}
