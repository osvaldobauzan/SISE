namespace CJF.Sgcpj.Judicatura.Proyectos.Domain.Entities;

public class ListadoVersiones
{
    public long AsuntoNeunId { get; set; }

    public string AsuntoAlias { get; set; }

    public int CatOrganismoId { get; set; }

    public string CatOrganismo { get; set; }

    public int TipoAsuntoId { get; set; }

    public string TipoAsunto { get; set; }

    public int CuadernoId { get; set; }

    public string Cuaderno { get; set; }

    public long pkProyectoId { get; set; }

    public long pkProyectoArchivoId { get; set; }

    public string sNombreArchivo { get; set; }

    public string sAnioRuta { get; set; }

    public int iVersion { get; set; }

    public int iEstado { get; set; }

    public string EstadoProyecto { get; set; }

    public long iTitular { get; set; }

    public string NombreTitular { get; set; }

    public long iSecretario { get; set; }

    public string NombreSecretario { get; set; }

    public int iTipoSentenciaId { get; set; }

    public string TipoSentencia { get; set; }

    public int iSentidoId { get; set; }

    public string SentidoSentencia { get; set; }

    public string ComentarioSecretario { get; set; }

    public string? ComentarioTitular { get; set; }

    public long? fkCorreccionArchivoId { get; set; }

    public string? CorreccionArchivo { get; set; }

    public DateTime fFechaAltaArchivoProyecto { get; set; }
}