using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Procedimientos.Consulta;
public class ProcedimientoDto : IMapFrom<CatalogoProcedimiento>
{
    public int ID { get; set; }
    public string DESCRIPCION { get; set; }
}
