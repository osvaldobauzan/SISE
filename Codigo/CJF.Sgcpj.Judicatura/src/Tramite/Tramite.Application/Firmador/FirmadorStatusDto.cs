namespace CJF.Sgcpj.Judicatura.Tramite.Application.Firmador;
public class FirmadorStatusDto
{
    public Guid Id { get; set; }
    public string Nombre { get; set; }
    public string ContentType { get; set; }
    public string Estatus { get; set; }
    public string MensajeError { get; set; }
}
