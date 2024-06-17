using CJF.Sgcpj.Judicatura.Common.Application.Common.Alertas;

namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
public class EmailAlertDTO : IValidAlertMessages
{
    public string Asunto { get; set; }
    public string BodyCorreo { get; set; }
}
