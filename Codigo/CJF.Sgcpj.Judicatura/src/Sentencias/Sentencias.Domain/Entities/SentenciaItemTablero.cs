namespace CJF.Sgcpj.Judicatura.Sentencias.Domain.Entities;

public class SentenciaItemTablero
{
    public long AsuntoNeunId { get; set; }

    public string AsuntoAlias { get; set; }

    public int CatOrganismoId { get; set; }

    public string NombreOrganismo { get; set; }

    public string CatTipoAsunto { get; set; }

    public int CatTipoAsuntoId { get; set; }

    public int TipoCuadernoId { get; set; }

    public string TipoCuaderno { get; set; }

    public DateTime fFechaAprobacionProyecto { get; set; }

    public string? TemaAsunto { get; set; }

    public long? ArchivoSentenciaId { get; set; }

    public string? Sentencia { get; set; }

    public long SentenciaId { get; set; }

    public long ArchivoAcuseId { get; set; }

    public string? Acuse { get; set; }

    public long? IdCapturo { get; set; }

    public string? NombreCapturo { get; set; }

    public DateTime? FechaCapturo { get; set; }

    public string? NombrePreautorizo { get; set; }

    public DateTime? FechaPreautorizo { get; set; }

    public string? NombreAutorizo { get; set; }

    public DateTime? FechaAutorizo { get; set; }

    public DateTime? FechaPublicacion { get; set; }

    public DateTime? FechaAuto { get; set; }

    public int? EstadoSentenciaId { get; set; }

    public string? EstadoSentencia { get; set; }
    
    public int CatTipoOrganismoId { get; set; }
    public int AsuntoDocumentoId { get; set; }
    public Guid GuidDocumento { get; set; }
}
