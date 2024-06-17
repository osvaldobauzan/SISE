using AutoMapper;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Procedimientos.Consulta;
public record ObtieneCatalogoProcedimientosConsulta : IRequest<List<ProcedimientoDto>>
{
    public int CatTipoAsuntoId { get; set; }
}

public class ObtieneCatalogoProcedimientosHandler : IRequestHandler<ObtieneCatalogoProcedimientosConsulta, List<ProcedimientoDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;

    public ObtieneCatalogoProcedimientosHandler(IMapper mapper, IApplicationDbContext applicationDbContext)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
    }



    async Task<List<ProcedimientoDto>> IRequestHandler<ObtieneCatalogoProcedimientosConsulta, List<ProcedimientoDto>>.Handle(ObtieneCatalogoProcedimientosConsulta request, CancellationToken cancellationToken)
    {
        var listaCatalogosProcedimiento = new List<ProcedimientoDto>();
        List<SqlParameter> procedimientoParametros = new List<SqlParameter>();
        var pi_CatTipoAsuntoId = new SqlParameter("@pi_CatTipoAsuntoId", request.CatTipoAsuntoId);
        procedimientoParametros.Add(pi_CatTipoAsuntoId);
        var itemsCatalogo = await _applicationDbContext.ExecuteStoredProc<CatalogoProcedimiento>("uspx_getCatTiposProcedimiento", procedimientoParametros.ToArray());
        listaCatalogosProcedimiento = _mapper.Map<List<ProcedimientoDto>>(itemsCatalogo);
        return await Task.FromResult(listaCatalogosProcedimiento);
    }
}
