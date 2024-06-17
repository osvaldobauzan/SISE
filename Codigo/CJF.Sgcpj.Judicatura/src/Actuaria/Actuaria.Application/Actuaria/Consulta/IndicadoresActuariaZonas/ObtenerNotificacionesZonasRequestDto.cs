
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.IndicadoresActuaria;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerPersonaAsunto;
public class ObtenerNotificacionesZonasRequestDto : IRequest<ObtenerNotificacionesZonasResponseDto>
{
    public int CatOrganismoId { get; set; }
    public DateTime FechaInicial { get; set; }
    public DateTime FechaFinal { get; set; }
}