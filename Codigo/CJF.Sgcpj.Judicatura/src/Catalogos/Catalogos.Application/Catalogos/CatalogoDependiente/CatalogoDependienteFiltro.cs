using MediatR;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.CatalogoDependiente;

public class CatalogoDependienteFiltro : IRequest<List<CatalogoDependienteDTO>>
{
    public int CatalogoId { get; set; }

    public int CatalogoPadreId { get; set; }
}
