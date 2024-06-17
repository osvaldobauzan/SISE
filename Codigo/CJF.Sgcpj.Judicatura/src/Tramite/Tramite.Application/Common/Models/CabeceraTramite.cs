namespace CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;

public class CabeceraTramite
{
    public int? CatOrganismoId { get; set; }
    public int? AsuntoId { get; set; }
    public long? AsuntoNeunId { get; set; }
    public int? SintesisOrden { get; set; }
    public DateTime FechaCaptura { get; set; }
    public int? AsuntoDocumentoId { get; set; }
    public short? CatAutorizacionDocumentosId { get; set; }
    public DateTime FechaAlta { get; set; }
    public string? NombreArchivo { get; set; }
    public string? NombreDocumento { get; set; }
    public short? CatContenidoId { get; set; }
    public string? Sintesis { get; set; }
}





