namespace CJF.Sgcpj.Judicatura.Tramite.Application.FirmadorDocumentos;
public class FirmadorInicioPeticion
{
    public string CveSiseg { get; set; }
    public bool Rubrica { get; set; }
    public string UrlGuardarDocumento { get; set; }
    public string UrlObtenerDocumento { get; set; }
    public string UrlRetorno { get; set; }
    public IEnumerable<FirmadorInicioDocumentoPeticionDto> DocumentosInfo { get; set; }
}

public class FirmadorInicioPeticionDto
{
    public bool FirmarOficios { get; set; }
    /// <summary>
    /// 1 Preautorizar, 2 Autorizar, 3 Cancelar
    /// </summary>
    public int Accion { get; set; }
    public List<FirmadorInicioDocumentoPeticionDto> Documentos { get; set; }
}

public class FirmadorInicioDocumentoPeticionDto
{
    public string Id { get; set; }
    public string Nombre { get; set; }
    public int Modulo { get; set; }
    public string TipoArchivo { get; set; }
}
