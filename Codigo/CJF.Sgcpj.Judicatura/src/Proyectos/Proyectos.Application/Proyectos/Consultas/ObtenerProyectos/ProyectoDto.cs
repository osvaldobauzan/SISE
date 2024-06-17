using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Proyectos.Domain.Entities;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerProyectos;

public class ProyectoDto : IMapFrom<Proyecto>
{
    public ExpedienteDto Expediente { get; set; }

    public bool TieneAudiencia { get; set; }

    public DateTime? FechaAudiencia { get; set; }

    public bool TieneArchivoAudiencia { get; set; }

    public string? ArchivoAudiencia { get; set; }

    public string Secretario { get; set; }

    public string Mesa { get; set; }

    public bool TieneArchivoProyecto { get; set; }

    public string? ArchivoProyecto { get; set; }

    public DateTime? FechaCargaProyecto { get; set; }

    public int? NumeroVersionProyecto { get; set; }

    public int? EstadoProyecto { get; set; }

    public string? DescripcionEstadoProyecto { get; set; }

    public DateTime? FechaEstadoProyecto { get; set; }

    public int? SentidoProyecto { get; set; }

    public string? DescripcionSentidoProyecto { get; set; }

    public int? TipoSentencia { get; set; }

    public string? DescripcionTipoSentencia { get; set; }

    public long ProyectoId { get; set; }

    public int AsuntoDocumentoId { get; set; }
}
