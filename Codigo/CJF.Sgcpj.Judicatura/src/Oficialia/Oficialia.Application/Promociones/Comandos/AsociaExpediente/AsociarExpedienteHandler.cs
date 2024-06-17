using AutoMapper;
using CJF.Sgcpj.Judicatura.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.AsociaExpediente;

public class AsociarExpedienteHandler : IRequestHandler<AsociarExpediente, List<AsociarExpedienteDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ISesionService _sesionService;

    public AsociarExpedienteHandler(IMapper mapper, IApplicationDbContext applicationDbContext, ISesionService sesionService)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
        _sesionService = sesionService;
    }

    async Task<List<AsociarExpedienteDto>> IRequestHandler<AsociarExpediente, List<AsociarExpedienteDto>>.Handle(AsociarExpediente request, CancellationToken cancellationToken)
    {
        var listaCatalogos = new List<AsociarExpedienteDto>();
        List<SqlParameter> listaCatalogoExpedienteSql = new List<SqlParameter>();
        var asuntoAlias = new Microsoft.Data.SqlClient.SqlParameter("@pi_AsuntoAlias", request.AsuntoAlias);
        var catOrganismoId = new Microsoft.Data.SqlClient.SqlParameter("@pi_CatOrganismoId", _sesionService.SesionActual.CatOrganismoId);
        var catTipoAsuntoId = new Microsoft.Data.SqlClient.SqlParameter("@pio_CatTipoAsuntoId", request.CatTipoAsuntoId);
        var pi_Modulo = new Microsoft.Data.SqlClient.SqlParameter("@pi_Modulo", request.Modulo);
        var pi_CatTipoProcedimiento = new Microsoft.Data.SqlClient.SqlParameter("@pi_CatTipoProcedimiento", request.CatTipoProcedimiento);

        listaCatalogoExpedienteSql.Add(asuntoAlias);
        listaCatalogoExpedienteSql.Add(catOrganismoId);
        listaCatalogoExpedienteSql.Add(catTipoAsuntoId);
        listaCatalogoExpedienteSql.Add(pi_Modulo);
        listaCatalogoExpedienteSql.Add(pi_CatTipoProcedimiento);
        var itemsCatalogo = await _applicationDbContext.ExecuteStoredProc<ExpedienteAsociar>("SISE3.pcExpedientePorAsuntoAlias", listaCatalogoExpedienteSql.ToArray());

        listaCatalogos = _mapper.Map<List<AsociarExpedienteDto>>(itemsCatalogo);
        return await Task.FromResult(listaCatalogos);
    }
}
