using AutoMapper;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Cuadernos.Consulta;
public class ObtieneCatalogoCuadernoHandler : IRequestHandler<ObtieneCatalogoCuaderno, List<CuadernoDto>>
{

    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;

    public ObtieneCatalogoCuadernoHandler(IMapper mapper, IApplicationDbContext applicationDbContext)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
    }

    async Task<List<CuadernoDto>> IRequestHandler<ObtieneCatalogoCuaderno, List<CuadernoDto>>.Handle(ObtieneCatalogoCuaderno request, CancellationToken cancellationToken)
    {
        var listaCatalogosCuaderno = new List<CuadernoDto>();
        List<SqlParameter> cuadernoParametros = new List<SqlParameter>();
        var tipoAsuntoId = new SqlParameter("@pi_TipoAsuntoId", request.TipoAsuntoId);
        var cuadernoId = new SqlParameter("@pi_CuadernoId", request.CuadernoId);
        var asuntoNeunId = new SqlParameter("@pi_AsuntoNeunId", request.AsuntoNeunId);
        cuadernoParametros.Add(tipoAsuntoId);
        cuadernoParametros.Add(cuadernoId);
        cuadernoParametros.Add(asuntoNeunId);
        var itemsCatalogo = await _applicationDbContext.ExecuteStoredProc<CatalogoCuaderno>("SISE3.pcObtieneCatCuaderno", cuadernoParametros.ToArray());
        listaCatalogosCuaderno = _mapper.Map<List<CuadernoDto>>(itemsCatalogo);
        return await Task.FromResult(listaCatalogosCuaderno);
    }

}
