using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.DetalleNotificacionesFiltros;
public record DetalleNotificacionesFiltrosConsulta : IRequest<FiltroDetalleNotificaciones<FiltroTipoParteDto,FiltroTipoNotificacionDto,FiltroActuarioDto>>
{
    public int CatOrganismoId {  get; set; }
}
