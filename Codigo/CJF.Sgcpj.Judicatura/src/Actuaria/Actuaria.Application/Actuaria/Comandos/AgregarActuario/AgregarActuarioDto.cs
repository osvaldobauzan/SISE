using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.AgregarActuario;
public class AgregarActuarioDto : IMapFrom<AgregarActuarioM>
{
    public long AsuntoNeunId { get; set; }
    public int AsuntoId { get; set; }
    public long ActuarioId { get; set; }
    public long Parte { get; set; }
    public long Promovente { get; set; }
    public int TipoNotificacionId { get; set; }
    public bool TieneCOE { get; set; }
    public long NotElecId { get; set; }
    public long EmpleadoId { get; set; }
}