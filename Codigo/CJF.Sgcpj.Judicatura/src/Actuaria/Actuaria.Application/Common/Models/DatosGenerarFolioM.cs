namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;

public class ResponseDatosGenerarFolioM
{
    public List<DatosGenerarFolioM> Datos { get; set; }
    public List<FolioM> Folios { get; set; }
}
public class DatosGenerarFolioM
{
    public long AsuntoNeunId { get; set; }
    public int AsuntoDocumentoId { get; set; }
    public int SintesisOrden { get; set; }
    public int CatOrganismoId { get; set; }
    public int TipoAsuntoId { get; set; }
	public string NombreArchivo { get; set; }
    public string ExtensionDocumento { get; set; }
    public string AsuntoAlias { get; set; }
    public string GuidAsuntoDcoumento { get; set; }
}

public class FolioM
{
    public int Folio { get; set; }
}
