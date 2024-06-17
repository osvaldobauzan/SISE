using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.Actuario.GenerarAcuseOficio.Actuario;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.Actuario.GenerarAcuseOficio.Fecha;
public class GenerarAcuseOficioPorFechaRequestDto : IRequest<GenerarAcuseOficioPorFechaResponseDto>
{
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public long ActuarioId { get; set; }
    public int CatOrganismoId { get; set; }
}
