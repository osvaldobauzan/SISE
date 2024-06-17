using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using ExpedienteElectronico.Application.Common.Models;

namespace ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.DatosGenerales;
public class DatosGeneralesDto : IMapFrom<DatosGeneralesM>
{
    public string FechaOCC { get; set; }
    public string FechaOrg { get; set; }
    public string Secretario { get; set; }
    public string Mesa { get; set; }
}
