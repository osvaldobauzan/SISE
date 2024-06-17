using AutoMapper;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.DiasInhabiles.Consulta;
public class ObtieneDiasInhabilesHandler : IRequestHandler<CatalogosDiasInhabilesFiltro, List<CatalogoDiasInhabilesDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDBContext;
    private readonly ISesionService _sesionService;

    public ObtieneDiasInhabilesHandler(IMapper mapper, IApplicationDbContext applicationDBContext, ISesionService sesionService)
    {
        _mapper = mapper;
        _applicationDBContext = applicationDBContext;
        _sesionService = sesionService;
    }

    async Task<List<CatalogoDiasInhabilesDto>> IRequestHandler<CatalogosDiasInhabilesFiltro, List<CatalogoDiasInhabilesDto>>.Handle(CatalogosDiasInhabilesFiltro request, CancellationToken cancellationToken)
    {
        
        var lista = new List<CatalogoDiasInhabilesDto>();
        var parametros = new[]
        {
           new SqlParameter("@pi_CatOrganismoId", _sesionService.SesionActual.CatOrganismoId),
           new SqlParameter("@pi_FechaInicio", request.FechaInicio),
           new SqlParameter("@pi_FechaFin", request.FechaFin),
        };


        //List<SqlParameter> listaCatalogoDiasInhabilesSQL = new List<SqlParameter>();
        //var catOrganismoId = new SqlParameter("@pi_CatOrganismoId", _sesionService.SesionActual.CatOrganismoId);
        //listaCatalogoDiasInhabilesSQL.Add(catOrganismoId);


        var itemsCatalogo = await _applicationDBContext.ExecuteStoredProc<CatalogoDiasInhabilesDto>("[SISE3].[pcObtenerDiasInhabliles]", parametros);
        //lista = _mapper.Map<List<CatalogoDiasInhabilesDto>>(itemsCatalogo);
        return await Task.FromResult(itemsCatalogo);
    }
}

