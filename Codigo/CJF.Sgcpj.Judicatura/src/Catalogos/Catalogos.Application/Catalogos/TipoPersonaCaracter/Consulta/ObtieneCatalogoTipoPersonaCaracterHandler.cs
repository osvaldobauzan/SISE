using AutoMapper;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using MediatR;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.TipoPersonaCaracter.Consulta;
public class ObtieneCatalogoTipoPersonaCaracterHandler : IRequestHandler<ObtieneCatalogoTipoPersonaCaracter, List<CatalogoTipoPersonaCaracterDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;

    public ObtieneCatalogoTipoPersonaCaracterHandler(IMapper mapper, IApplicationDbContext applicationDbContext)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
    }
    async Task<List<CatalogoTipoPersonaCaracterDto>> IRequestHandler<ObtieneCatalogoTipoPersonaCaracter, List<CatalogoTipoPersonaCaracterDto>>.Handle(ObtieneCatalogoTipoPersonaCaracter request, CancellationToken cancellationToken)
    {
        var listaCatalogosPersonaCaracter = new List<CatalogoTipoPersonaCaracterDto>();
        List<SqlParameter> personaCaracterParametros = new List<SqlParameter>();
        var tipoAsuntoId = new SqlParameter("@pi_TipoAsuntoId", request.TipoAsuntoId);
        personaCaracterParametros.Add(tipoAsuntoId);
        var itemsCatalogo = await _applicationDbContext.ExecuteStoredProc<CatalogoTipoPersonaCaracter>("uspx_getTipoCaracter", personaCaracterParametros.ToArray());
        listaCatalogosPersonaCaracter = _mapper.Map<List<CatalogoTipoPersonaCaracterDto>>(itemsCatalogo);
        return await Task.FromResult(listaCatalogosPersonaCaracter);
    }
}