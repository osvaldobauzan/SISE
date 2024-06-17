using MediatR;

namespace CJF.Sgcpj.Judicatura.Oficialia.Application.Promociones.Consulta.ObtenerDetalleCargaMasiva;
public class ObtenerDetalleCargaMasivaRequest : IRequest<ObtenerDetalleAlertaDto>
{
    public int YearPromocion { get; set; }
    public int NumeroRegistro { get; set; }
    public int CatOrganismoId { get; set; }
}
