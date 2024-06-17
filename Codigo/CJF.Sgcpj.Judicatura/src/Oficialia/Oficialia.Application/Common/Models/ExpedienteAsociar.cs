namespace CJF.Sgcpj.Judicatura.Application.Common.Models;

public class ExpedienteAsociar
{
    public long AsuntoNeunId { get; set; }
    public int CatOrganismoId { get; set; }
    public string AsuntoAlias { get; set; }
    public int CatTipoAsuntoId { get; set; }
    public int CatMateriaId { get; set; }
    public string NumeroOCC { get; set; }
    public string TipoAsunto { get; set; }
    public int SecretarioId { get; set; }
    public string? Secretario { get; set; }
    public int? CatTipoProcedimiento { get; set; }
    public string? TipoProcedimiento { get; set; }
    public string Mesa { get; set; }
}
