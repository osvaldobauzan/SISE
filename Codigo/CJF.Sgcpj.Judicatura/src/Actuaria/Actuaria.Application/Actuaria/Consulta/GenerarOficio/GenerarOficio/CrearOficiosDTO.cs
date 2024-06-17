namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.GenerarOficio;
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
    public string? TipoAnexoId { get; set; }
    public bool AplicaUNC { get; set; }
    public List<AutoridadAsunto> Asuntos { get; set; }
    public string TextoLibreId { get; set; }
    public string AsuntoDocumentoId { get; set; }
    public string AsuntoDocumentoIdOficio { get; set; }
    public int RutaNasId { get; set; }
}
