using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerTramitesConFiltro;
public class MetaDataEstadosTramiteDto : IMapFrom<MetaDataEstadosTramite>
{
    public int TotalTramites { get; set; }
    public int TotalSinAcuerdo { get; set; }
    public int TotalCancelados { get; set; }
    public int TotalConAcuerdo { get; set; }
    public int TotalPreAutorizados { get; set; }
    public int TotalAutorizados { get; set; }
}
