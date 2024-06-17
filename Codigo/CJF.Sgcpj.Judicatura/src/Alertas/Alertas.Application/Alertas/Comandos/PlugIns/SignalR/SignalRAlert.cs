using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Alertas.Application.Alertas.Comandos.PlugIns.SignalR;
public class SignalRAlert
{
    public IEnumerable<Destinatario> Destinatarios { get; set; }
    public dynamic Alerta { get; set; }
}
