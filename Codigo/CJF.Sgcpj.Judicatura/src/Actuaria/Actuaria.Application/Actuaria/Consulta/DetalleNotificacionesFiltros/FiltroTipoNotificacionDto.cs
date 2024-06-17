using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.DetalleNotificacionesFiltros;
public class FiltroTipoNotificacionDto : IMapFrom<FiltroTipoNotificacion>
{
    public int kIdCatNotificaciones { get; set; }
    public string sDescripcion { get; set; }
}
