using CJF.Sgcpj.Judicatura.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerTiempTurnos;
public class DetalleIntervalosDto : IMapFrom<DetalleIntervalos>
{
    public long RegistroEmpleadoId { get; set; }
    public int NumeroRegistro { get; set; }
    public string HoraMinutoAlta { get; set; }
    public string HoraMinutoTurnado { get; set; }

}
