using AutoMapper;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Anexo.Consulta;

public class ObtieneCatalogosAnexoHandler : IRequestHandler<ObtieneCatalogosAnexo, List<CatalogosAnexoDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ISesionService _sesionService;

    public ObtieneCatalogosAnexoHandler(IMapper mapper, IApplicationDbContext applicationDbContext, ISesionService sesionService)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
        _sesionService = sesionService;
    }
    async Task<List<CatalogosAnexoDto>> IRequestHandler<ObtieneCatalogosAnexo, List<CatalogosAnexoDto>>.Handle(ObtieneCatalogosAnexo request, CancellationToken cancellationToken)
    {

        List<CatalogosAnexoDto> listaCatalogos = new List<CatalogosAnexoDto>();
        List<SqlParameter> anexosParametros = new List<SqlParameter>();
        if (request.CatTipoCatalogoAsuntoId == 27) // Solucion de catalogo caracter anexo
        {
            request.CatOrganismoId = 1;
            request.CatTipoAsuntoId = 1;
        }
        var piCatTipoCatalogoAsuntoId = new SqlParameter("@piCatTipoCatalogoAsuntoId", request.CatTipoCatalogoAsuntoId);
        var piCatOrganismoId = new SqlParameter("@piCatOrganismoId", _sesionService.SesionActual.CatOrganismoId);
        var piCatTipoAsuntoId = new SqlParameter("@piCatTipoAsuntoId", request.CatTipoAsuntoId);
        anexosParametros.Add(piCatTipoCatalogoAsuntoId);
        anexosParametros.Add(piCatOrganismoId);
        anexosParametros.Add(piCatTipoAsuntoId);
        var itemsCatalogo = await _applicationDbContext.ExecuteStoredProc<CatalogoAnexo>("usp_CatalogosSel", anexosParametros.ToArray());
        listaCatalogos = _mapper.Map<List<CatalogosAnexoDto>>(itemsCatalogo);
        return await Task.FromResult(listaCatalogos);
    }

}
