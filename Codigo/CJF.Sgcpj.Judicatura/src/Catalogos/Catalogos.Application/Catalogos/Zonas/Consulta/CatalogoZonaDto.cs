using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Zonas.Consulta;
public class CatalogoZonaDto : IMapFrom<CatalogoZona>
{
    public int AreaId { get; set; }
    public string Nombre { get; set; }
    public long EmpleadoId { get; set; }
    public string NombreEmpleado { get; set; }
}
