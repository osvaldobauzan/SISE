using MediatR;
using Proyectos.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerUltimaVersionProyecto;

public class ObtenerVersion : IRequest<VersionDto>
{
    public int CatOrganismoId { get; set; }
    public long AsuntoNeunId { get; set; }
}
