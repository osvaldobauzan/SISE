namespace CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;
public class Tramite
{
    public string SRuta { get; set; }
    public string NombreArchivo { get; set; }
    public string RutaCompleta { get; set; }
    public int CatOrganismoId { get; set; }

    public bool Firmado { get; set; }
    public int AsuntoDocumentoId { get; set; }
    public long AsuntoNeunId { get; set; }

}
