
using CJF.Sgcpj.Judicatura.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Oficialia.Application.Promociones.Consulta.ObtenerPromocionesFiltros;
public class FiltroCapturoDto : IMapFrom<FiltroCapturo>
{
    public string Capturo { get; set; }
    public string UserName { get; set; }
    public long EmpleadoId { get; set; }
    public int CatOrganismoId { get; set; }
}
