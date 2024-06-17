
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.IndicadoresActuaria;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.DiferenciasNotificaciones;
public class ObtenerDiferenciasRequestDto : IRequest<ObtenerDiferenciasResponseDto>
{
    public int EmpleadoId { get; set; }
    public DateTime FechaInicial { get; set; }
    public DateTime FechaFinal { get; set; }
}