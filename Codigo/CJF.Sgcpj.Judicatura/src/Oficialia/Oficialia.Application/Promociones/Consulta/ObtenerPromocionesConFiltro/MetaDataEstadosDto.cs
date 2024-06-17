using CJF.Sgcpj.Judicatura.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerPromocionesConFiltro;
public class MetaDataEstadosDto : IMapFrom<MetaDataEstados>
{
    public int TotalPromociones { get; set; }
    public int TotalSinCaptura { get; set; }
    public int TotalCapturadas { get; set; }
    public int EnviadasAMesa { get; set; }
}
