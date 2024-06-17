namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
public class ConsultaPaginada
{
    public DateTime FechaInicial { get; init; }
    public DateTime FechaFinal { get; init; }

    public int TipoFiltro { get; set; }
    public string Texto { get; set; }
    public string OrdenarPor { get; set; }
    public bool Descendente { get; set; }
    public int Pagina { get; set; }
    public int RegistrosPorPagina { get; set; }
    public int CatOrganismoId { get; set; }
    public int TotalRegistros { get; set; }
    public long AsuntoNeunId { get; set; }
    public string? Estado { get; set; }
    public int? Contenido { get; set; }
    
}
