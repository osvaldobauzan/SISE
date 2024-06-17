namespace CJF.Sgcpj.Judicatura.Proyectos.Domain.Entities;

public class ProyectosItemTablero
{
    public long AsuntoNeunId { get; set; }

    public string AsuntoAlias { get; set; }

    public int CatTipoOrganismoId { get; set; }

    public int CatOrganismoId { get; set; }

    public string CatTipoAsunto { get; set; }

    public int CatTipoAsuntoId { get; set; }

    public int TipoProcedimiento { get; set; }

    public string NombreCorto { get; set; }

    public int TipoCuaderno { get; set; }

    public string sTipoCuaderno { get; set; }

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

    public string? sEstadoProyecto { get; set; }

    public DateTime? FechaEstadoProyecto { get; set; }

    public int? SentidoProyecto { get; set; }

    public string? sSentido { get; set; }

    public int? TipoSentencia { get; set; }

    public string? sTipoSentencia { get; set; }

    public long pkProyectoId { get; set; }

    public int AsuntoDocumentoId { get; set; }
}
