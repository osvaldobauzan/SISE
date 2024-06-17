using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.EmpleadosProyecto.Consulta;

public class CatalogoEmpleadoDto : IMapFrom<CatalogoEmpleado>
{
    public long EmpleadoId { get; set; }

    public int CargoId { get; set; }

    public string CargoDescripcion { get; set; }

    public string NombreEmpleado { get; set; }
}
