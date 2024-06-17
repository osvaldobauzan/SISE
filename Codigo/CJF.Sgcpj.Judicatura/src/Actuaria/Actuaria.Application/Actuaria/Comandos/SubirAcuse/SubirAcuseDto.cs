using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.SubirAcuse;
public class SubirAcuseDto: IMapFrom<SubirAcuseM>
{

    public long AsuntoNeunId { get; set; }
    public long PersonaId { get; set; }
    public int SintesisOrden { get; set; }
    [Description("FechaNotificacion: dd/MM/yyyy")]
    public string FechaNotificacion { get; set; }
    public int? TipoAcuse { get; set; }
    public string SintesisCitatorio { get; set; }
    public string NombreArchivo { get; set; }
    [Description("FechaNotificacionCitatorio: dd/MM/yyyy")]
    public string FechaNotificacionCitatorio { get; set; }
    public int? TipoNotificacion { get; set; }

    [Required(AllowEmptyStrings = true)]
    [Description("Archivo del acuse")]
    public byte[] Archivo { get; set; }
   
  

}