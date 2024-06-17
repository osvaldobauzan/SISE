namespace CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;
public class EstadoOficioRoot
{
    public EstadoOficio EstadoOficio { get; set; }
}
public class EstadoOficio
{
    public Guid GuidDocumento { get; set; }
    public int? RutaId { get; set; }
    public string? Nombre { get; set; }
    public string? Extension { get; set; }
    public bool Firmado { get; set; }
    public long? AsuntoNeunId { get; set; }
    public int? AsuntoDocumentoId { get; set; }
    public long? AnexoParteId { get; set; }
    public int? CatOrganismoId { get; set; }
}
