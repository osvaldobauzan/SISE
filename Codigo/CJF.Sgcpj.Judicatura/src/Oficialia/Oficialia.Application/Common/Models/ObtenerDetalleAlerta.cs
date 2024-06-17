namespace CJF.Sgcpj.Judicatura.Oficialia.Application.Common.Models;

public class ObtenerDetalleAlerta
{
    public int NumeroRegistro { get; set; }
    public string NumeroExpediente { get; set; }
    public string TipoAsunto { get; set; }
    public string TipoProcedimiento { get; set; }
    public string Mesa { get; set; }
    public int SecretarioId { get; set; }
}