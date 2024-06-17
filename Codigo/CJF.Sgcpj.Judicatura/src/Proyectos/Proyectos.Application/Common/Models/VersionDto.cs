namespace Proyectos.Application.Common.Models;

public class VersionDto
{
    public long ProyectoId { get; set; }

    public long ArchivoId { get; set; }

    public string NombreArchivo { get; set; }

    public string Anio { get; set; }

    public int NumeroVersion { get; set; }

    public int EstadoId { get; set; }

    public string EstadoDescripcion { get; set; }

    public int TitularId { get; set; }

    public string NombreTitular { get; set; }

    public int SecretarioId { get; set; }

    public string NombreSecretario { get; set; }

    public int TipoSentenciaId { get; set; }

    public string TipoSentenciaDescripcion { get; set; }

    public int TipoSentidoId { get; set; }

    public string TipoSentidoDescripcion { get; set; }

    public string Sintesis { get; set; }

    public string? ComentarioTitular { get; set; }

    public long? ArchivoComentarioId { get; set; }

    public string? NombreArchivoObservaciones { get; set; }

    public DateTime FechaAlta { get; set; }
}
