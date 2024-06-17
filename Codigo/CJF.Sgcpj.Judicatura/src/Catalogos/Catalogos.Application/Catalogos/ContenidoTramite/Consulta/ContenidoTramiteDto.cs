using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.ContenidoTramite.Consulta;
public class ContenidoTramiteDto : IMapFrom<CatalogoContenido>
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
    public int Elementos { get; set; }
}

