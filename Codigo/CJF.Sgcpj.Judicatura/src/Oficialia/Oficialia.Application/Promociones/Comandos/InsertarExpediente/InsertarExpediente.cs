namespace CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.InsertarExpediente;
public class InsertarExpediente
{
    public int CatOrganismoId { get; set; }
    public int CatTipoAsuntoId { get; set; }
    public string NumeroOCC { get; set; }
    public string NoExpediente { get; set; }
    public long EmpleadoId { get; set; }
    public int? TipoProcedimiento { get; set; }
    public long? AsuntoNeunId { get; set; }
    public long? PiAsuntoNeunId { get; set; }
    public int AsuntoId { get; set; }
    public int EsActualizacion { get; set; }
}
