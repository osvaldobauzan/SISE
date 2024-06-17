using AutoMapper;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.TipoAcuse.Consulta;
public class ObtieneCatalogoTipoAcuseHandler : IRequestHandler<ObtieneCatalogoTipoAcuse, List<CatalogoTipoAcuseDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;

    public ObtieneCatalogoTipoAcuseHandler(IMapper mapper, IApplicationDbContext applicationDbContext)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
    }

    public async Task<List<CatalogoTipoAcuseDto>> Handle(ObtieneCatalogoTipoAcuse request, CancellationToken cancellationToken)
    {
        var itemsCatalogo = await _applicationDbContext.ExecuteStoredProc<CatalogoTipoAcuse>("[SISE3].[pcObtieneCatalogoTipoAcuse]");

        return _mapper.Map<List<CatalogoTipoAcuseDto>>(itemsCatalogo);
    }
}
