using MediatR;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerNumeroPromocion;
public record ObtieneNumeroPromocion : IRequest<ObtieneNumeroPromocionDto>
{
    public int CatOrganismoId { get; set; }
    public int NumeroRegistro {  get; set; }
    public int YearPromocion { get; set; }
}
