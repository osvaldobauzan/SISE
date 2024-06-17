using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CJF.Sgcpj.Judicatura.Oficialia.Application.Promociones.Comandos.SubirArchivoComando;
public class OficialiaCargaMasivaDto
{
    [Required(AllowEmptyStrings = false)]
    [Description("Anio promocion")]
    public int? yearPromocion { get; set; }

    [Required(AllowEmptyStrings = true)]
    [Description("Archivo del Anexo en formato .docx o .pdf")]
    public List<byte[]> Archivo { get; set; }
}
