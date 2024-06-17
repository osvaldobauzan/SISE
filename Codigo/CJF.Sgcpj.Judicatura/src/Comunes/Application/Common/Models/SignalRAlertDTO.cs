using CJF.Sgcpj.Judicatura.Common.Application.Common.Alertas;

namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
public class SignalRAlertDTO : IValidAlertMessages
{
    public Guid Id { get; set; }
    public string Emisor { get; set; }
    public string Mensaje { get; set; }
    public string Parte { get; set; }
    public string Receptor { get; set; }
    public string Estado { get; set; }
    public string OrganismoEmisor { get; set; }
}
