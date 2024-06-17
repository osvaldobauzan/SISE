using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerProyectos;

public class MetaDataEstadoProyectoDto : IMapFrom<MetaDataEstadoProyecto>
{
    public int TotalProyectos { get; set; }

    public int TotalSinProyecto { get; set; }

    public int TotalParaRevision { get; set; }

    public int TotalNoAprobado { get; set; }

    public int TotalConAjustes { get; set; }

    public int TotalAprobado { get; set; }
}
