using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerProyectos;

public record ObtenerProyectosConFiltro : IRequest<ListaPaginada<ProyectoDto, MetaDataEstadoProyectoDto>>
{
    public string FechaInicial { get; init; }

    public string FechaFinal { get; init; }

    public int Estado { get; set; }

    public string OrdenarPor { get; set; }

    public bool Descendente { get; set; }

    public int Pagina { get; set; }

    public int RegistrosPorPagina { get; set; }

    public string Texto { get; set; }
}
