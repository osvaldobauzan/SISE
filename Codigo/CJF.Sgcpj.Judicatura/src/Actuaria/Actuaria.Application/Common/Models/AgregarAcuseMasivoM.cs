using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text.Json.Nodes;
using Microsoft.Data.SqlClient.Server;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
public class AgregarAcuseMasivoM
{
    public long AsuntoNeunId { get; set; }
    public int SintesisOrden { get; set; }
    public int TipoAcuse { get; set; }
    public string SintesisCitatorio { get; set; }
    [Description("FechaNotificacionCitatorio: dd/MM/yyyy")]
    public string FechaNotificacionCitatorio { get; set; }
    public int? TipoNotificacion { get; set; }
    public string PartesNotificacionesAcuse { get; set; }

    public List<ArchivosAcuseM> ArchivosAcuse { get; set; }
}

public class ParteNotificacionAcuseM
{
    public long ParteId { get; set; }
    public string FechaNotificacion { get; set; }

}

public class ArchivosAcuseM
{
    public string NombreArchivo { get; set; }
    [Required(AllowEmptyStrings = true)]
    [Description("Archivo del acuse")]
    public byte[] Archivo { get; set; }
}
