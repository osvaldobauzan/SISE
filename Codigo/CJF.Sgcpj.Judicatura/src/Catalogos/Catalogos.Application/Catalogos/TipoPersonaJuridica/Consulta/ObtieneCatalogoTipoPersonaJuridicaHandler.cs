using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Generico.Consulta;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.TipoPersonaJuridica.Consulta;
public class ObtieneCatalogoTipoPersonaJuridicaHandler : IRequestHandler<CatalogoTipoPersonaJuridicaFiltro, List<CatalogoTipoPersonaJuridicaDTO>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;

    public ObtieneCatalogoTipoPersonaJuridicaHandler(IMapper mapper, IApplicationDbContext applicationDbContext)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
    }
    async Task<List<CatalogoTipoPersonaJuridicaDTO>> IRequestHandler<CatalogoTipoPersonaJuridicaFiltro, List<CatalogoTipoPersonaJuridicaDTO>>.Handle(CatalogoTipoPersonaJuridicaFiltro request, CancellationToken cancellationToken)
    {

        var parametros = new[]
        {
            new SqlParameter("@piCatTipoAsuntoId", request.CatTipoAsuntoId),
        };
        var listaCatalogos = await _applicationDbContext.ExecuteStoredProc<CatalogoTipoPersonaJuridicaDTO>("usp_CatTipoPersonaJuridicaSel", parametros.ToArray());
        return await Task.FromResult(listaCatalogos);
    }
}
