using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerDetalleAcuerdo;
public record ObtieneDetalleAcuerdoConsulta : IRequest<ListaDetalleAcuerdo<DetalleAcuerdoDto, PromocionDto>>
{
    public int CatOrganismoId { get; set; }
    public long AsuntoNeunId { get; set; }
    public int SintesisOrden { get; set; }
    public int AsuntoDocumentoId { get; set; }
}