using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Common.Request;
public class AcuerdoArchivoDto
{
    [Required(AllowEmptyStrings = false)]
    [Description("Id. Asunto Neun")]
    public long? AsuntoNeunId {  get; set; }

    [Required(AllowEmptyStrings = false)]
    [Description("Id. Contenido")]
    public short? Contenido { get; set; }
    
    [Required(AllowEmptyStrings = false)]
    [Description("Id. Tipo Cuaderno")]
    public short? TipoCuaderno { get; set; }
    
    [Required(AllowEmptyStrings = false)]
    [Description("Fecha de acuerdo")]
    public string? FechaAcuerdo { get; set; }

    [Required(AllowEmptyStrings = false)]
    [Description("Listado de promociones")]
    public string? Promociones { get; set; }

    [Required(AllowEmptyStrings = false)]
    [Description("Listado de promociones de autoridad")]
    public string? PromocionesAutoridad { get; set; }

    [Required(AllowEmptyStrings = true)]
    [Description("Listado de promociones de parte")]
    public string? PromocionesParte {  get; set; }
    
    [Required(AllowEmptyStrings = true)]
    [Description("Archivo del acuerdo en formato .docx o .pdf")]
    public byte[] Archivo { get; set; }

    [Required(AllowEmptyStrings = false)]
    [Description("Sintesis Orden")]
    public int? SintesisOrden { get; set; }

    [Required(AllowEmptyStrings = false)]
    [Description("Id de Asunto Docuumento")]
    public int? AsuntoDocumentoId { get; set; }

    [Required(AllowEmptyStrings = false)]
    [Description("Descripcion del Catalogo Tipo Asunto")]
    public string CatTipoAsunto { get; set; }

    [Required(AllowEmptyStrings = false)]
    [Description("Asunto Alias")]
    public string AsuntoAlias { get; set; }

    [Required(AllowEmptyStrings = false)]
    [Description("Mesa")]
    public string Mesa { get; set; }
}
