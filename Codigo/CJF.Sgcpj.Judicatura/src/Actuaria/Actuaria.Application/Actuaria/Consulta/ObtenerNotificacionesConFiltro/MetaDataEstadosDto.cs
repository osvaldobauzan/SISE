using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerNotificacionesConFiltro;
public class MetaDataEstadosDto : IMapFrom<MetaDataEstados>
{
    public int TotalNotificaciones { get; set; }
    public int TotalMasTresDias { get; set; }
    public int TotalDosDias { get; set; }
    public int TotalUnDia { get; set; }
    public int TotalNotificados { get; set; }
}
