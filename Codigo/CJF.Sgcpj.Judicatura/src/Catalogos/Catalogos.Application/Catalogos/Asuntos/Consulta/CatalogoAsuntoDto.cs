using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Asuntos.Consulta;
public class CatalogoAsuntoDto : IMapFrom<CatalogoAsunto>
{
    public int CatTipoAsuntoId { get; set; }
    public string TipoAsunto { get; set; }
    public int CuadernoId { get; set; }


}
