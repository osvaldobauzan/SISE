using CJF.Sgcpj.Judicatura.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Application.Catalogos.Contenido.Consulta;
public class ContenidoDto : IMapFrom<CatalogoContenido>
{
    public int ID { get; set; }
    public string DESCRIPCION { get; set; }
}
