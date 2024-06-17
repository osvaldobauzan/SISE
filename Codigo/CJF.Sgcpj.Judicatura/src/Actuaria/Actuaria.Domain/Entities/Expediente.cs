namespace CJF.Sgcpj.Judicatura.Actuaria.Domain.Entities;
public class Expediente
{
    public long AsuntoNeunId { get; set; }
    public string AsuntoAlias { get; set; }
    public string CatTipoAsunto { get; set; }
    public string TipoProcedimiento { get; set; }
    public int CatTipoAsuntoId { get; set; }
}
