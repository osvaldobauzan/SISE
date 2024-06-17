using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtienePromocionesConFiltro;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Oficialia.Application.Promociones.Consulta.ObtenerNumeroExpediente;
public class ObtenerNumeroExpediente : IRequest<ExpedienteDto>
{
    public int CatOrganismoId { get; set; }
    public int TipoAsuntoId { get; set; }
    public int TipoProcedimientoId{ get; set; }
}
