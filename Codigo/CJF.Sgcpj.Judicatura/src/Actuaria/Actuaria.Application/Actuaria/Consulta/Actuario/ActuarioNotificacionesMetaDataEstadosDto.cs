using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models.Actuario;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.Actuario;
public class ActuarioNotificacionesMetaDataEstadosDto: IMapFrom<ActuarioNotificacionesMetaDataEstados>
{
    public int VerTodo { get; set; }
    public int Pendiente { get; set; }
    public int EnProceso { get; set; }
    public int Notificados { get; set; }
}
