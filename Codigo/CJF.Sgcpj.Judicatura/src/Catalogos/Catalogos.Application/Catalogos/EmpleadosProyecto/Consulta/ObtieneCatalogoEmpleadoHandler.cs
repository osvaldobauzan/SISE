using AutoMapper;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.EmpleadosProyecto.Consulta;

public class ObtieneCatalogoEmpleadoHandler : IRequestHandler<ObtieneCatalogoEmpleado, List<CatalogoEmpleadoDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ISesionService _sesionService;

    public ObtieneCatalogoEmpleadoHandler(IMapper mapper, IApplicationDbContext applicationDbContext, ISesionService sesionService)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
        _sesionService = sesionService;
    }

    async Task<List<CatalogoEmpleadoDto>> IRequestHandler<ObtieneCatalogoEmpleado, List<CatalogoEmpleadoDto>>.Handle(ObtieneCatalogoEmpleado request, CancellationToken cancellationToken)
    {
        request.CatOrganismoId = _sesionService.SesionActual.CatOrganismoId;

        List<SqlParameter> parametrosEmpleado = new()
        {
            new SqlParameter("@pi_CatOrganismoId", request.CatOrganismoId),
            new SqlParameter("@pi_CargoId", request.TipoEmpleado),
        };

        var itemsCatalogo = await _applicationDbContext.ExecuteStoredProc<CatalogoEmpleado>("[SISE3].[pcTableroProyectoCatalogoCargo]", parametrosEmpleado.ToArray());
        var listaCatalogoEmpleado = _mapper.Map<List<CatalogoEmpleadoDto>>(itemsCatalogo);

        return await Task.FromResult(listaCatalogoEmpleado);
    }
}
