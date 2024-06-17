using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Contenido.Consulta;
public class ContenidoDto : IMapFrom<CatalogoContenido>
{
    public int ID { get; set; }
    public string DESCRIPCION { get; set; }
}
