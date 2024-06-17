using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Alertas.Application.Alertas.Comandos.RecuperarAlertas;
public class AlertQueryDTO
{
    public Guid Id { get; set; }
    public AlertType TipoDeAlerta { get; set; }

    public string Emisor { get; set; }
    public DateTime HoraDeLaAlerta { get; set; }
    public string Mensaje { get; set; }
    public string Parte { get; set; }
    public string Receptor { get; set; }
    public string Estado { get; set; }
}
