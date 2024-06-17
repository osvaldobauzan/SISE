namespace CJF.Sgcpj.Judicatura.Tramite.Domain.Entities;

public class Acuerdo
{
    public long AsuntoNeunId { get; set; }
    public string NombreDocumento { get; set; }
    public string NombreArchivo { get; set; }
    public string ExtensionDocumento { get; set; }
    public short Contenido { get; set; }
    public short TipoCuaderno { get; set; }
    public string FechaAcuerdo { get; set; }
}
