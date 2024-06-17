using MediatR;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerDetalleTramite;
public record ObtieneDetalleTramiteConsulta : IRequest<DetalleTramiteDto<CabeceraTramiteDto, PromocionesDto, PartesDto, OficioDto>>
{
    public int? catOrganismoId { get; set; }
    public int? asuntoNeunId { get; set; }
    public int? sintesisOrden { get; set; }
    public int? asuntoDocumentoId { get; set; }
}
