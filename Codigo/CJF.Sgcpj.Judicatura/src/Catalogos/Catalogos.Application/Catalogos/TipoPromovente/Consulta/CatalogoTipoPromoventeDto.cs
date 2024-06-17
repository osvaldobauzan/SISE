using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.TipoPromovente.Consulta;
public class CatalogoTipoPromoventeDto : IMapFrom<CatalogoTipoPromovente>
{
    public int ID { get; set; }
    public string DESCRIPCION { get; set; }

}
