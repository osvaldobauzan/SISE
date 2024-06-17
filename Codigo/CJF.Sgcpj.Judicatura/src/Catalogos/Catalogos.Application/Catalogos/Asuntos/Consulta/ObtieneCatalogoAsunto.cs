using AutoMapper;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Asuntos.Consulta;
public record ObtieneCatalogoAsunto : IRequest<List<CatalogoAsuntoDto>>
{
    public int CatOrganismoId { get; set; }
}

public class ObtieneCatalogoAsuntoHandler : IRequestHandler<ObtieneCatalogoAsunto, List<CatalogoAsuntoDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ISesionService _sesionService;

    public ObtieneCatalogoAsuntoHandler(IMapper mapper, IApplicationDbContext applicationDbContext, ISesionService sesionService)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
        _sesionService = sesionService;
    }



    async Task<List<CatalogoAsuntoDto>> IRequestHandler<ObtieneCatalogoAsunto, List<CatalogoAsuntoDto>>.Handle(ObtieneCatalogoAsunto request, CancellationToken cancellationToken)
    {
        var listaCatalogos = new List<CatalogoAsuntoDto>();
        List<SqlParameter> parametros = new List<SqlParameter>();
        var pi_CatTipoOrganismoId = new SqlParameter("@pi_CatOrganismoId", _sesionService.SesionActual.CatOrganismoId);
        parametros.Add(pi_CatTipoOrganismoId);
        var pi_Catalogo = new SqlParameter("@pi_Catalogo", 1); // Asuntos vigentes
        parametros.Add(pi_Catalogo);
        var itemsCatalogo = await _applicationDbContext.ExecuteStoredProc<CatalogoAsunto>("SISE3.pcListadoCatTiposAsunto", parametros.ToArray());
        listaCatalogos = _mapper.Map<List<CatalogoAsuntoDto>>(itemsCatalogo);
        return await Task.FromResult(listaCatalogos);
    }
}