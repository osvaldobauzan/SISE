namespace CJF.Sgcpj.Judicatura.Actuaria.Domain.Entities;
public class ActuariaItemTablero
{
    public string No_Exp { get; set; }
    public string TipoAsuntoDescripcion { get; set; }
    public int CatTipoAsuntoId { get; set; }
    public int NumeroRegistro { get; set; }
    public string NombreArchivo { get; set; }
    public string TipoContenidoDescripcion { get; set; }
    public string FechaAuto_F {  get; set; }
    public DateTime FechaAutoriza { get; set; }
    public int Transcurrido { get; set; }
    public int Notificados { get; set; }
    public string Estado { get; set; }
    public string Sintesis { get; set; }
    public long AsuntoNeunId { get; set; }
    public int TipoCuaderno { get; set; }
    public string TipoCuadernoDesc { get; set; }
    public long SecretarioPId { get; set; }  
    public int TipoContenido {  get; set; }
    public string Contenido {  get; set; }
    public int ContenidoId { get; set; }
    public long UsuarioCaptura { get; set; }
    public int AsuntoDocumentoId { get; set; }
    public int SintesisOrden {  get; set; }
    public int ConAcuse { get; set; }
    public string TipoProcedimiento { get; set; }
    public DateTime FechaAlta { get; set; }
    public Guid? uGuidDocumento { get; set; }
}
