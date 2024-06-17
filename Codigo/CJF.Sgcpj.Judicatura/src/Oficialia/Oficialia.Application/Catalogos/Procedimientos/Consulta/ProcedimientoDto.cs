using CJF.Sgcpj.Judicatura.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Application.Catalogos.Contenido.Consulta;
public class ProcedimientoDto : IMapFrom<CatalogoProcedimiento>
{
    public int ID { get; set; }
    public string DESCRIPCION { get; set; }
}
