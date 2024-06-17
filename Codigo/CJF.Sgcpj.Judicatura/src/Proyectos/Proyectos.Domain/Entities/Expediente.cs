namespace CJF.Sgcpj.Judicatura.Proyectos.Domain.Entities;

public class Expediente
{
    public long AsuntoNeunId { get; set; }

    public string AsuntoAlias { get; set; }

    public int CatTipoOrganismoId { get; set; }

    public int CatOrganismoId { get; set; }

    public string CatTipoAsunto { get; set; }

    public int CatTipoAsuntoId { get; set; }

    public int TipoProcedimiento { get; set; }

    public string NombreCorto { get; set; }

    public int TipoCuadernoId { get; set; }

    public string Cuaderno { get; set; }
}
