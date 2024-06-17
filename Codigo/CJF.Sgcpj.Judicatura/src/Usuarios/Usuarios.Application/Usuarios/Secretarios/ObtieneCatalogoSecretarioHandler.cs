using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Usuarios.Application.Common.Models;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Usuarios.Application.Usuarios.Secretarios;

public class ObtieneCatalogoSecretarioHandler : IRequestHandler<ObtieneCatalogoSecretario, List<CatalogoSecretarioDto>>
{

    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;
    private readonly IApplicationDbContext _applicationDbContext;

    public ObtieneCatalogoSecretarioHandler(IMapper mapper, IApplicationDbContext applicationDbContext, ISesionService sesionService)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
        _sesionService = sesionService;
    }

    async Task<List<CatalogoSecretarioDto>> IRequestHandler<ObtieneCatalogoSecretario, List<CatalogoSecretarioDto>>.Handle(ObtieneCatalogoSecretario request, CancellationToken cancellationToken)
    {
        var listaCatalogosSecretario = new List<CatalogoSecretarioDto>();
        List<SqlParameter> listaSecretarioParametros = new List<SqlParameter>();
        request.CatOrganismoId = _sesionService.SesionActual.CatOrganismoId;
        var catOrganismoId = new SqlParameter("@pi_CatOrganismoId", request.CatOrganismoId);
        listaSecretarioParametros.Add(catOrganismoId);
        var itemsCatalogo = await _applicationDbContext.ExecuteStoredProc<UsuarioSecretario>("[SISE3].[pcConsultaSecretariosPorOrganismo]", listaSecretarioParametros.ToArray());
        listaCatalogosSecretario = _mapper.Map<List<CatalogoSecretarioDto>>(itemsCatalogo);
        return await Task.FromResult(listaCatalogosSecretario);
    }
}
