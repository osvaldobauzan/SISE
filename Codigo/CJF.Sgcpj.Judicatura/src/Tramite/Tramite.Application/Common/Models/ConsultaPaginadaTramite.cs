namespace CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;
public class ConsultaPaginadaTramite
{
    public DateTime FechaInicial { get; init; }
    public DateTime FechaFinal { get; init; }

    public int Estado { get; set; }
    public string Texto { get; set; }
    public string OrdenarPor { get; set; }
    public bool Descendente { get; set; }
    public int Pagina { get; set; }
    public int RegistrosPorPagina { get; set; }
    public int OrganismoID { get; set; }
    public int TotalRegistros { get; set; }
    public long AsuntoNeunId { get; set; }
    public long? SecretarioId { get; set; }
    public string? Origen { get; set; }
    public int? CatTipoAsuntoId { get; set; }
    public long? CapturoId { get; set; }
    public long? PreautorizoId { get; set; }
    public long? AutorizoId { get; set; }
    public long? CanceloId { get; set; }
    public int? UsuariId { get; set; }
}
