using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.CatalogoDependiente;

public class CatalogoDependienteHandler : IRequestHandler<CatalogoDependienteFiltro, List<CatalogoDependienteDTO>>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public CatalogoDependienteHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<List<CatalogoDependienteDTO>> Handle(CatalogoDependienteFiltro request, CancellationToken cancellationToken)
    {
        var parametros = new[]
        {
            new SqlParameter("@piCatalogoDependienteIdPadre", request.CatalogoId),
            new SqlParameter("@piCatalogoDependienteId", request.CatalogoPadreId)
        };
        var listOpciones = await _applicationDbContext.ExecuteStoredProc<CatalogoDependienteDTO>("usp_CatalogosDependientes", parametros.ToArray());
        return await Task.FromResult(listOpciones);
    }
}
