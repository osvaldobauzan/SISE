using AutoMapper;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.TipoComunicacion;
public class ObtieneCatalogoTipoComunicacionHandler : IRequestHandler<ObtieneCatalogoTipoComunicacion, List<CatalogoTipoComunicacionDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;

    public ObtieneCatalogoTipoComunicacionHandler(IMapper mapper, IApplicationDbContext applicationDbContext)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
    }
    async Task<List<CatalogoTipoComunicacionDto>> IRequestHandler<ObtieneCatalogoTipoComunicacion, List<CatalogoTipoComunicacionDto>>.Handle(ObtieneCatalogoTipoComunicacion request, CancellationToken cancellationToken)
    {
        var itemsCatalogo = await _applicationDbContext.ExecuteStoredProc<CatalogoTipoComunicacion>("[SISE3].[pcObtieneCatalogoTipoComunicación]");
        return _mapper.Map<List<CatalogoTipoComunicacionDto>>(itemsCatalogo);
    }
}
