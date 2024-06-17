using Proyectos.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerVersionesProyecto;

public class ListadoVersionesDto
{
    public long AsuntoNeunId { get; set; }

    public string NumeroExpediente { get; set; }

    public int TipoAsuntoId { get; set; }

    public string TipoAsunto { get; set; }

    public int CuadernoId { get; set; }

    public string Cuaderno { get; set; }

    public int CatOrganismoId { get; set; }

    public string CatOrganismo { get; set; }

    public List<VersionDto> Archivos { get; set; }
}
