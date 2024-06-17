using MediatR;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Tipo.Consulta;

public record ObtieneCatalogoTipo : IRequest<List<CatalogoTipoDto>>
{
}
