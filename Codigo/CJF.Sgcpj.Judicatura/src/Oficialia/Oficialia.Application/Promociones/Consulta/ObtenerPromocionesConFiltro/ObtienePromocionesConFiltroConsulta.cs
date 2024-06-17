using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerPromocionesConFiltro;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtienePromocionesConFiltro;


public record ObtienePromocionesConFiltroConsulta : IRequest<ListaPaginada<PromocionDto, MetaDataEstadosDto>> {
    public string FechaInicial { get; init; }
    public string FechaFinal { get; init; }

    public int Estado { get; set; }
    public string Texto { get; set; }
    public string OrdenarPor { get; set; }
    public bool Descendente { get; set; }
    public int Pagina { get; set; }
    public int RegistrosPorPagina { get; set; }
    public int TotalRegistros { get; set; }
    public string? Origen { get; set; }
    public long? Asignado { get; set; }
    public string? Capturo { get; set; }





}
