using MediatR;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerVersionesProyecto;

public class ObtenerListadoVersiones : IRequest<ListadoVersionesDto>
{
    public int CatOrganismoId { get; set; }

    public long AsuntoNeunId { get; set; }
}
