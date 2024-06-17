using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.TipoPersona.Consulta;
public class CatalogoTipoPersonaDto : IMapFrom<CatalogoTipoPersona>
{
    public int CatTipoPersonaId { get; set; }
    public string Descripcion { get; set; }
}
