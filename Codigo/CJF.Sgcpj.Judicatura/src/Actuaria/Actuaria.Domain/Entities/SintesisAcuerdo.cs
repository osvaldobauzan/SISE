namespace CJF.Sgcpj.Judicatura.Actuaria.Domain.Entities;
public class SintesisAcuerdo
{
    public long AsuntoNeunId { get; set; }
    public short TipoCuaderno { get; set; }
    public string? NombreDocumento { get; set; }
    public string? ExtensionDocumento { get; set; }
    public short? Contenido { get; set;}
    public DateTime FechaAcuerdo { get; set; }
    public long UsuarioCaptura { get; set; }
    public int AsuntoDocumentoId { get; set; }
    public string? Sintesis { get; set; }
    public int? SintesisOrden { get; set; }
    public string? NombreArchivo { get; set; }


}
