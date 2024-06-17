using MediatR;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerMotivosPartes;

public class ObtenerMotivosPartes : IRequest<ListadoMotivosPartesDto>
{
    public long IdProyecto { get; set; }
}
