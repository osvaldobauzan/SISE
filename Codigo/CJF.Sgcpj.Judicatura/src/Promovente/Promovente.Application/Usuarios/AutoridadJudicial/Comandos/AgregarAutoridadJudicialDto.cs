using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Promovente.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Promovente.Application.Usuarios.AutoridadJudicial.Comandos;

public class AgregarAutoridadJudicialDto : IMapFrom<AgregarAutoridadJudicial>
{
    public int AsuntoNeunId { get; set; }
    public int catIdOrganismo { get; set; }
    public int EmpleadoId { get; set; }
    public int RegistroEmpleadoId { get; set; }
    public int AutoridadJudicialId { get; set; }
    public int NumeroOrden { get; set; }
}
