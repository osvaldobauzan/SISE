using AutoMapper;
using MediatR;
using Microsoft.Data.SqlClient;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.TipoPromovente.Consulta;
public class ObtieneCatalogoTipoPromoventeHandler : IRequestHandler<ObtieneCatalogoTipoPromovente, List<CatalogoTipoPromoventeDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ISesionService _sesionService;

    public ObtieneCatalogoTipoPromoventeHandler(IMapper mapper, IApplicationDbContext applicationDbContext, ISesionService sesionService)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
        _sesionService = sesionService;
    }

    async Task<List<CatalogoTipoPromoventeDto>> IRequestHandler<ObtieneCatalogoTipoPromovente, List<CatalogoTipoPromoventeDto>>.Handle(ObtieneCatalogoTipoPromovente request, CancellationToken cancellationToken)
    {
        var listaCatalogosTipoPromovente = new List<CatalogoTipoPromoventeDto>();
        List<SqlParameter> tipoPromoventeParametros = new List<SqlParameter>();
        var pi_CatTipoOrganismoId = new SqlParameter("@pi_CatTipoOrganismoId", _sesionService.SesionActual.CatTipoOrganismoId);
        var pi_CatTipoAsuntoId = new SqlParameter("@pi_CatTipoAsuntoId", request.CatTipoAsuntoId);
        var pi_CatOrganismoId = new SqlParameter("@pi_CatOrganismoId", _sesionService.SesionActual.CatOrganismoId);
        tipoPromoventeParametros.Add(pi_CatTipoOrganismoId);
        tipoPromoventeParametros.Add(pi_CatTipoAsuntoId);
        tipoPromoventeParametros.Add(pi_CatOrganismoId);
        var itemsCatalogo = await _applicationDbContext.ExecuteStoredProc<CatalogoTipoPromovente>("uspx_op_getTipoPromovente", tipoPromoventeParametros.ToArray());
        listaCatalogosTipoPromovente = _mapper.Map<List<CatalogoTipoPromoventeDto>>(itemsCatalogo);
        return await Task.FromResult(listaCatalogosTipoPromovente);
    }
}
