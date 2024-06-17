using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerDetalleTramite;
public class PromocionesDto : IMapFrom<Promociones>
{
    public int? CatOrganismoId { get; set; }
    public int? AsuntoId { get; set; }
    public long? AsuntoNeunId { get; set; }
    public int? NumeroOrden { get; set; }
    public int? NumeroRegistro { get; set; }
}
