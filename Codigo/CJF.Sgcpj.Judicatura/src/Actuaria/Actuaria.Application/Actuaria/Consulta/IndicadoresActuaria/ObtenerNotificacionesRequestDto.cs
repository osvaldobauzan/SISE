
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.IndicadoresActuaria;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerPersonaAsunto;
public class ObtenerNotificacionesRequestDto : IRequest<ObtenerNotificacionesResponseDto>
{
    public int CatOrganismoId { get; set; }
    public long FiltroActuarioId { get; set; }
    public DateTime FechaInicial { get; set; }
    public DateTime FechaFinal { get; set; }
}