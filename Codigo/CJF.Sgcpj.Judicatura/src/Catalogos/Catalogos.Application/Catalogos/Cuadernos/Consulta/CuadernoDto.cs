using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Cuadernos.Consulta;
public class CuadernoDto : IMapFrom<CatalogoCuaderno>
{
    public int CuadernoId { get; set; }
    public string Cuaderno { get; set; }
    public string Color { get; set; }
    public int ClasificacionCuadernoId { get; set; }
    public string ClasificacionCuaderno { get; set; }
    public string CuadernoCorto { get; set; }
}
