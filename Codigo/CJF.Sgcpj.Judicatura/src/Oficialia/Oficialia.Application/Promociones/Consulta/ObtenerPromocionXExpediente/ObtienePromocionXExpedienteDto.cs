using CJF.Sgcpj.Judicatura.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerPromocionXExpediente;
public class ObtienePromocionXExpedienteDto : IMapFrom<PromocionXExpediente>
{
    public int NumeroRegistro { get; set; }
    public int NumeroOrden { get; set; }
    public int YearPromocion { get; set; }
    public int EstadoPromocion { get; set; }

}
