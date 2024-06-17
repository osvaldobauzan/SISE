using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.Sentencia;
public class EstadoSentenciaDto : IMapFrom<EstadoSentencia>
{
    public string FechaSentencia { get; set; }
    public string Estado { get; set; }
    public string Ejecucion { get; set; }
}
