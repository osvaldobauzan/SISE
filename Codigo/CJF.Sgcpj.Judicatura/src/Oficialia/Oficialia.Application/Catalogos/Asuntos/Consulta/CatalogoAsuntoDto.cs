using CJF.Sgcpj.Judicatura.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Application.Catalogos.Contenido.Consulta;
public class CatalogoAsuntoDto : IMapFrom<CatalogoAsunto>
{
    public int CatTipoAsuntoId { get; set; }
    public string TipoAsunto { get; set; }
    public int CuadernoId { get; set; }


}
