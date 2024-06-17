using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.TipoComunicacion;
public class CatalogoTipoComunicacionDto : IMapFrom<CatalogoTipoComunicacion>
{
    public int ID { get; set; }
    public string Descripcion { get; set; }
}
