using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.Audiencia;
public class AudienciaDto : IMapFrom<DetalleAudiencia>
{
    public string Fecha { get; set; }
    public string Hora { get; set; }
    public string Resultado { get; set; }
    public string TipoAudiencia { get; set; }
}
