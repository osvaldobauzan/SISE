using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Anexo.Consulta;

public class CatalogosAnexoDto : IMapFrom<CatalogoAnexo>
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
    public int Elementos { get; set; }
}
