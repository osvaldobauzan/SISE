using AutoMapper;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Zonas.Consulta;
public class ObtieneCatalogoZonaHandler : IRequestHandler<ObtieneCatalogoZona, List<CatalogoZonaDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ISesionService _sesionService;

    public ObtieneCatalogoZonaHandler(IMapper mapper, IApplicationDbContext applicationDbContext, ISesionService sesionService)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
        _sesionService = sesionService;
    }
    async Task<List<CatalogoZonaDto>> IRequestHandler<ObtieneCatalogoZona, List<CatalogoZonaDto>>.Handle(ObtieneCatalogoZona request, CancellationToken cancellationToken)
    {
        var listaCatalogoZona = new List<CatalogoZonaDto>();
        List<SqlParameter> parametrosZona = new List<SqlParameter>();

        request.CatOrganismoId = _sesionService.SesionActual.CatOrganismoId;

        var pi_CatOrganismoId = new SqlParameter("@pi_CatOrganismoId", request.CatOrganismoId);
        parametrosZona.Add(pi_CatOrganismoId);

        var itemsCatalogo = await _applicationDbContext.ExecuteStoredProc<CatalogoZona>("[SISE3].[pcCatalogoZonas]", parametrosZona.ToArray());
        listaCatalogoZona = _mapper.Map<List<CatalogoZonaDto>>(itemsCatalogo);
       
        return await Task.FromResult(listaCatalogoZona);
    }
}
