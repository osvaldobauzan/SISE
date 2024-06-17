using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.Filtros;
public class FiltroContenidoDto : IMapFrom<FiltroContenido>
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
}
