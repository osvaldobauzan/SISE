using MediatR;

namespace CJF.Sgcpj.Judicatura.Application.Usuarios.PromoventeExistente.Consulta;
public record ObtieneCatalogoPromoventeExistente : IRequest<List<CatalogoPromoventeExistenteDto>>
{
    public long AsuntoNeunId { get; set; }
}
