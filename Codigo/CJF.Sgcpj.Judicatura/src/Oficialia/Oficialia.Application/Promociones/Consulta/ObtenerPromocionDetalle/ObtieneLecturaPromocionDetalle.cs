using CJF.Sgcpj.Judicatura.Application.Common.Models;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerPromocionDetalle;
public class ObtieneLecturaPromocionDetalle : IRequest<ListaDetallePromocionTablero<PromocionDetalleTableroDto, AnexoListaDto>>
{
    public long? kIdElectronica { get; set; }
    public int CatOrganismoId { get; set; }
    public long AsuntoNeunId { get; set; }
    public int UsuariId { get; set; }
    public int Origen { get; set; }
    public int NumeroOrden { get; set; }
    public int YearPromocion { get; set; }
    public string? HoraPresentacion { get; set; }
    public bool EsPromocionE { get; set; }
    public int Estado { get; set; }
    public string Tipo { get; set; }
    public string SubTipo { get; set; }
}
