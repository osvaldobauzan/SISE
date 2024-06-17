using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Alertas.Application.Alertas.Comandos.PlugIns;
public class AlertPlugInsListDTO : INotification
{
    public string Alerts { get; set; }
    public IEnumerable<object>? PlugInsParameters { get; set; }
    public AlertType TipoDeAlerta { get; set; }
}
