using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.Actuario;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models.Actuario;
public class ActuarioNotificacionesMetaDataEstados
{
    public int VerTodo { get; set; }
    public int Pendiente { get; set; }
    public int EnProceso { get; set; }
    public int Notificados { get; set; }
    public int Pagina { get; set; }
    public int TotalPaginas { get; set; }
    public int TotalRegistros { get; set; }
}
