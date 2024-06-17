using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerNotificacionesConFiltro;
public record ObtieneNotificacionesConFiltroConsulta : IRequest<ListaPaginada<NotificacionDto,MetaDataEstadosDto>>
{
    public string FechaInicial { get; init; }
    public string FechaFinal { get; init; }
    public int TipoFiltro { get; set; }
    public string Texto { get; set; }
    public string OrdenarPor { get; set; }
    public bool Descendente { get; set; }
    public int Pagina { get; set; }
    public int RegistrosPorPagina { get; set; }
    public int TotalRegistros { get; set; }
    public string? Estado { get; set; }
    public int? Contenido { get; set; }
}
