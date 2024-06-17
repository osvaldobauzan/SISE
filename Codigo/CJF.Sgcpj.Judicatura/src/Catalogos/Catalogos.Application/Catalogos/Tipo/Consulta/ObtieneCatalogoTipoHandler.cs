using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using MediatR;
using Newtonsoft.Json;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Tipo.Consulta;

public class ObtieneCatalogoTipoHandler : IRequestHandler<ObtieneCatalogoTipo, List<CatalogoTipoDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;

    public ObtieneCatalogoTipoHandler(IMapper mapper, IApplicationDbContext applicationDbContext)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
    }

    async Task<List<CatalogoTipoDto>> IRequestHandler<ObtieneCatalogoTipo, List<CatalogoTipoDto>>.Handle(ObtieneCatalogoTipo request, CancellationToken cancellationToken)
    {
        var listaCatalogos = new List<CatalogoTipoDto>();
        var itemsActivos = MockDataTipos.GetCatalogo().ToList();
        listaCatalogos = _mapper.Map<List<CatalogoTipoDto>>(itemsActivos);
        return await Task.FromResult(listaCatalogos);
    }

}
public class MockDataTipos
{

    public static string dataString = @"[{'ClasePromocionId':1,'ClasePromocion':'Escrito'},{'ClasePromocionId':2,'ClasePromocion':'Oficio'}]";
    public static List<CatalogoTipoDto> GetCatalogo() => JsonConvert.DeserializeObject<List<CatalogoTipoDto>>(dataString);
}