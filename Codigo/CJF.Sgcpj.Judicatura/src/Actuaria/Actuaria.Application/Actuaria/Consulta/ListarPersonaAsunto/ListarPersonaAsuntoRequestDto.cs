using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerPersonaAsunto;

public class ListarPersonaAsuntoRequestDto : IRequest<List<ListarPersonaAsuntoDto>>
{
    public long EmpleadoId { get; set; }
    public long AsuntoNeunId { get; set; }
}