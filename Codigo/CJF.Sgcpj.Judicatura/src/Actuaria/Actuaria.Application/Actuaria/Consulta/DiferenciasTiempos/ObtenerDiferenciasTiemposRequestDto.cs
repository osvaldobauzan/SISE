
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.IndicadoresActuaria;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.DiferenciasTiempos;
public class ObtenerDiferenciasTiemposRequestDto : IRequest<ObtenerDiferenciasTiemposResponseDto>
{
    public long FiltroActuarioId { get; set; }
    public DateTime FechaInicial { get; set; }
    public DateTime FechaFinal { get; set; }
}