using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Seguridad.Application.Common.Models;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Seguridad.Application.Seguridad.Consultas.ObtieneOrganismosConsulta;
[SesionNoRequiredAttribute]
public class ObtieneOrganismosConsulta : IRequest<List<OrganismoDto>>
{
}

public class ObtieneOrganismosConsultaHandler : IRequestHandler<ObtieneOrganismosConsulta, List<OrganismoDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ICurrentUserService _currentUserService;

    public ObtieneOrganismosConsultaHandler(IMapper mapper, IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
        _currentUserService = currentUserService;
    }

    async Task<List<OrganismoDto>> IRequestHandler<ObtieneOrganismosConsulta, List<OrganismoDto>>.Handle(ObtieneOrganismosConsulta request, CancellationToken cancellationToken)
    {
        var empleadoId = _currentUserService.EmpleadoId;
        var listaOrganismosPorUsuario = new List<OrganismoDto>();
        List<SqlParameter> procedimientoParametros = new List<SqlParameter>();
        var pi_EmpleadoId = new SqlParameter("@pi_EmpleadoId", empleadoId);

        procedimientoParametros.Add(pi_EmpleadoId);

        var itemsCatalogo = await _applicationDbContext.ExecuteStoredProc<OrganismoPorUsuario>("[SISE3].[pcOrganismosPorUsuario]", procedimientoParametros.ToArray());
        listaOrganismosPorUsuario = _mapper.Map<List<OrganismoDto>>(itemsCatalogo);
        return await Task.FromResult(listaOrganismosPorUsuario);
    }
}