using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.DetalleNotificacionesFiltros;
public class FiltroActuarioDto : IMapFrom<FiltroActuario>
{
    public long EmpleadoId { get; set; }
    public string NombreActuario { get; set; }
}
