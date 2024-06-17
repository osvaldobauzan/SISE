using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CJF.Sgcpj.Judicatura.Oficialia.Application.Promociones.Comandos.SubirArchivoComando;
public class OficialiaArchivoDto
{
    [Required(AllowEmptyStrings = false)]
    [Description("Id. Asunto Neun")]
    public long? AsuntoNeunId { get; set; }

    [Required(AllowEmptyStrings = false)]
    [Description("No. Registro")]
    public int? noRegistro { get; set; }

    [Required(AllowEmptyStrings = false)]
    [Description("Numero de órden")]
    public int? numeroOrden { get; set; }

    [Required(AllowEmptyStrings = false)]
    [Description("Origen")]
    public int? origen { get; set; }

    [Required(AllowEmptyStrings = false)]
    [Description("Anio promocion")]
    public int? yearPromocion { get; set; }

    [Required(AllowEmptyStrings = false)]
    [Description("Fojas")]
    public short? fojas { get; set; }

    [Required(AllowEmptyStrings = true)]
    [Description("Archivo del Anexo en formato .docx o .pdf")]
    public byte[] Archivo { get; set; }
}
