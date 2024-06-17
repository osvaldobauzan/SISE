using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Usuarios.Application.Common.Models;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Usuarios.Application.Usuarios.PromoventeExistente.Consulta;
public class ObtieneCatalogoPromoventeExistenteHandler : IRequestHandler<ObtieneCatalogoPromoventeExistente, List<CatalogoPromoventeExistenteDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;

    public ObtieneCatalogoPromoventeExistenteHandler(IMapper mapper, IApplicationDbContext applicationDbContext)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
    }

    async Task<List<CatalogoPromoventeExistenteDto>> IRequestHandler<ObtieneCatalogoPromoventeExistente, List<CatalogoPromoventeExistenteDto>>.Handle(ObtieneCatalogoPromoventeExistente request, CancellationToken cancellationToken)
    {
        var listaCatalogosPromoventeExistente = new List<CatalogoPromoventeExistenteDto>();
        List<SqlParameter> listaPromoventeExistenteParametros = new List<SqlParameter>();
        var asuntoNeunId = new SqlParameter("@pi_AsuntoNeunId", request.AsuntoNeunId);
        listaPromoventeExistenteParametros.Add(asuntoNeunId);
        var itemsCatalogo = await _applicationDbContext.ExecuteStoredProc<CatalogoPromoventeExistente>("[SISE3].[pcPromoventesXAsunto]", listaPromoventeExistenteParametros.ToArray());
        listaCatalogosPromoventeExistente = _mapper.Map<List<CatalogoPromoventeExistenteDto>>(itemsCatalogo);

        return await Task.FromResult(listaCatalogosPromoventeExistente);
    }
}
