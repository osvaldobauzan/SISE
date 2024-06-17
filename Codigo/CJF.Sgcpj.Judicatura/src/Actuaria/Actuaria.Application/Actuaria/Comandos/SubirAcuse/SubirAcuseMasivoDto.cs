using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using Newtonsoft.Json;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.SubirAcuse;
public class SubirAcuseMasivoDto:IMapFrom<AgregarAcuseMasivoM>
{
    public long AsuntoNeunId { get; set; }
    public int SintesisOrden { get; set; }
    public int TipoAcuse { get; set; }
    public string SintesisCitatorio { get; set; }
    [Description("FechaNotificacionCitatorio: dd/MM/yyyy")]
    public string FechaNotificacionCitatorio { get; set; }
    public int? TipoNotificacion { get; set; }
    public List<ParteNotificacionAcuse> PartesNotificacionesAcuse { get; set; }
    public List<ArchivosAcuseDto> ArchivosAcuse { get; set; }
}


public class ParteNotificacionAcuse
{
    public long ParteId { get; set; }
    public string FechaNotificacion { get; set; }
}

public class ArchivosAcuseDto
{
    public string NombreArchivo { get; set; }
    [Required(AllowEmptyStrings = true)]
    [Description("Archivo del acuse")]
    public byte[] Archivo { get; set; }
}