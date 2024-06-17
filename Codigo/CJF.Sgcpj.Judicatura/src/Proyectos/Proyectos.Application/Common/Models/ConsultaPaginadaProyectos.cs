namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Models;

public class ConsultaPaginadaProyectos
{
    public DateTime FechaInicial { get; init; }

    public DateTime FechaFinal { get; init; }

    public int Estado { get; set; }

    public string OrdenarPor { get; set; }

    public bool Descendente { get; set; }

    public int Pagina { get; set; }

    public int RegistrosPorPagina { get; set; }

    public string Texto { get; set; }

    public int OrganismoID { get; set; }

    public long UsuarioId { get; set; }
}
