using AutoMapper;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Contenido.Consulta;

public class ObtieneCatalogoContenIDoHandler : IRequestHandler<ObtieneCatalogoContenido, List<ContenidoDto>>
{

    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ISesionService _sesionService;

    public ObtieneCatalogoContenIDoHandler(IMapper mapper, IApplicationDbContext applicationDbContext, ISesionService sesionService)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
        _sesionService = sesionService;
    }

    async Task<List<ContenidoDto>> IRequestHandler<ObtieneCatalogoContenido, List<ContenidoDto>>.Handle(ObtieneCatalogoContenido request, CancellationToken cancellationToken)
    {
        var listaCatalogos = new List<ContenidoDto>();
        List<SqlParameter> contenidoParametros = new List<SqlParameter>();
        var pi_CatTipoOrganismoId = new SqlParameter("@pi_CatTipoOrganismoId", _sesionService.SesionActual.CatTipoOrganismoId);
        var pi_CatTipoAsuntoId = new SqlParameter("@pi_CatTipoAsuntoId", request.CatTipoAsuntoId);
        contenidoParametros.Add(pi_CatTipoOrganismoId);
        contenidoParametros.Add(pi_CatTipoAsuntoId);
        var itemsCatalogo = await _applicationDbContext.ExecuteStoredProc<CatalogoContenido>("uspx_op_getTipoPromocion", contenidoParametros.ToArray());
        listaCatalogos = _mapper.Map<List<ContenidoDto>>(itemsCatalogo);
        return await Task.FromResult(listaCatalogos);
    }

}