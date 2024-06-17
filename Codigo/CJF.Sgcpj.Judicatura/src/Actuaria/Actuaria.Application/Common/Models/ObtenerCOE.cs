namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
public class ObtenerCOE
{
    public long AsuntoNeunId { get; set; }

    public string AsuntoAlias { get; set; }

    public int CatTipoAsuntoId { get; set; }

    public DateTime FechaEnvio { get; set; }

    public int TipoComunicacion { get; set; }

    public int Secretario { get; set; }

    public int OficinaCorrespondenciaComun { get; set; }

    public int TipoAsunto { get; set; }

    public string Mesa { get; set; }

    public string NumeroOrigen { get; set; }

    public string NumeroExpedienteOrigen { get; set; }

    public string Destinatario { get; set; }

    public string Objetivo { get; set; }
}
