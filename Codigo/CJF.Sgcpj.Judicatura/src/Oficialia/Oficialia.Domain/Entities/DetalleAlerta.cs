namespace CJF.Sgcpj.Judicatura.Oficialia.Domain.Entities;
public class DetalleAlerta
{
    public int NumeroRegistro { get; set; }
    public string NumeroExpediente { get; set; }
    public string TipoAsunto { get; set; }
    public string TipoProcedimiento { get; set; }
    public string Mesa { get; set; }
    public int SecretarioId { get; set; }
}
