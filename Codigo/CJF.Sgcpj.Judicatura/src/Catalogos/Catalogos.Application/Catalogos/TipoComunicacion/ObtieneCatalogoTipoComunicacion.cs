using MediatR;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.TipoComunicacion;
public record ObtieneCatalogoTipoComunicacion : IRequest<List<CatalogoTipoComunicacionDto>>
{
}
