using AutoMapper;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using MediatR;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.TipoPersona.Consulta;
public class ObtieneCatalogoTipoPersonaHandler : IRequestHandler<ObtieneCatalogoTipoPersona, List<CatalogoTipoPersonaDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;

    public ObtieneCatalogoTipoPersonaHandler(IMapper mapper, IApplicationDbContext applicationDbContext)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
    }
    async Task<List<CatalogoTipoPersonaDto>> IRequestHandler<ObtieneCatalogoTipoPersona, List<CatalogoTipoPersonaDto>>.Handle(ObtieneCatalogoTipoPersona request, CancellationToken cancellationToken)
    {
        var listaCatalogos = new List<CatalogoTipoPersonaDto>();
        List<SqlParameter> tipoPersonaParametros = new List<SqlParameter>();
        var piCatTipoAsuntoId = new SqlParameter("@piCatTipoCatalogoAsuntoId", 133);
        tipoPersonaParametros.Add(piCatTipoAsuntoId);
        var itemsCatalogo = await _applicationDbContext.ExecuteStoredProc<CatalogoTipoPersona>("usp_CatCatalogosAsuntoSel", tipoPersonaParametros.ToArray());
        listaCatalogos = _mapper.Map<List<CatalogoTipoPersonaDto>>(itemsCatalogo);
        return await Task.FromResult(listaCatalogos);
    }

}
