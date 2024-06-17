namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Models;

public class ValidarExpediente
{
    public int CatOrganismoId { get; set; }

    public int CatCuadernoId { get; set; }

    public string? NumeroExpediente { get; set; }

    public int? CatTipoAsuntoId { get; set; }

    public long? AsuntoNeunId { get; set; }
}
