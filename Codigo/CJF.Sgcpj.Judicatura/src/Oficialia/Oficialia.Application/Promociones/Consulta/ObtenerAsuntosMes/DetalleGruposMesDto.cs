using CJF.Sgcpj.Judicatura.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerNumeroPromocion;
public class DetalleGruposMesDto : IMapFrom<DetalleGruposMes>
{
    public string Mes { get; set; }
    public string TipoAsunto { get; set; }
    public int Total { get; set; }
}
