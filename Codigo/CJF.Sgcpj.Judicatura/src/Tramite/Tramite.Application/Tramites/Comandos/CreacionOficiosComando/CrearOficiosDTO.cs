namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Comandos.CreacionOficiosComando;
public class CrearOficiosDTO
{
    public Guid Id { get; set; }
    public string UsuarioId { get; set; }
    public string OrganismoId { get; set; }
    public string TipoAsunto { get; set; }
    public string AsuntoAlias { get; set; }
    public string Mesa { get; set; }
    public string AsuntoNeunId { get; set; }
    public string NombreArchivo { get; set; }
    public string RutaNas { get; set; }
    public string RutaNasAcuerdo { get; set; }
    public string TipoAnexoId { get; set; }
    public bool AplicaUNC { get; set; }
    public List<AutoridadAsunto> Asuntos { get; set; }
    public string TextoLibreId { get; set; }
    public string AsuntoDocumentoId { get; set; }
    public string AsuntoDocumentoIdOficio { get; set; }
    public int RutaNasId {get; set;}
}

public class AutoridadAsunto
{

    public string NombreAutoridad { get; set; }
    public string AnexoParteDescripcion { get; set; }
    public string TextoOficioLibreId { get; set; }
    public int? AnexoParteId { get; set; }
    public string? NumeroOficio { get; set; }
}

public class TextoOficioLibreDto
{
    public string TextoOficioLibreId { get; set; }
    public string TextoOficioLibre { get; set; }
}

