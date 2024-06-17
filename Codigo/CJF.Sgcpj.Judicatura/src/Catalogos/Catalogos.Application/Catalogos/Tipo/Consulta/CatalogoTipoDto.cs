using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Tipo.Consulta;

public class CatalogoTipoDto : IMapFrom<CatalogoTipo>
{
    public int ClasePromocionId { get; set; }
    public string ClasePromocion { get; set; }


}
