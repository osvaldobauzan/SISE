using CJF.Sgcpj.Judicatura.Common.Application.Common.Alertas;

namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
public class AlertDTO<T> where T : IValidAlertMessages
{
    public AlertType? TipoDeAlerta { get; set; }
    public IEnumerable<Destinatario> Destinatarios { get; set; }
    public T? Alerta { get; set; }
    public bool PersistirAlerta { get; set; }
}

public class Destinatario
{
    public string UsuarioId { get; set; }
    public string OrganismoId { get; set; }
    public string DireccionDestino { get; set; }
}

public enum AlertType
{
    SignalR,
    Email,
    Test
}

