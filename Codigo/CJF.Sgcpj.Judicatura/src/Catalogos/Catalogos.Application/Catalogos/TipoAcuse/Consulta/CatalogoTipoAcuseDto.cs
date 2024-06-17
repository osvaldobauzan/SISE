using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.TipoAcuse.Consulta;
public class CatalogoTipoAcuseDto : IMapFrom<CatalogoTipoAcuse>
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
}
