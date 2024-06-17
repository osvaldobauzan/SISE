using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.TipoPersonaCaracter.Consulta;

public class CatalogoTipoPersonaCaracterDto : IMapFrom<CatalogoTipoPersonaCaracter>
{
    public int CaracterPersonaId { get; set; }
    public string CaracterPersona { get; set; }
    public int TipoAsuntoId { get; set; }
    public string TipoAsunto { get; set; }
    public int Orden { get; set; }

}