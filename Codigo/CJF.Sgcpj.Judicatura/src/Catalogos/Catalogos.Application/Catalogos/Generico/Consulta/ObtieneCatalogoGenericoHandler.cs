using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.TipoPersona.Consulta;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Generico.Consulta;
public class ObtieneCatalogoGenericoHandler : IRequestHandler<CatalogoGenericoFiltro, List<CatalogoGenericoDTO>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;

    public ObtieneCatalogoGenericoHandler(IMapper mapper, IApplicationDbContext applicationDbContext)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
    }
    async Task<List<CatalogoGenericoDTO>> IRequestHandler<CatalogoGenericoFiltro, List<CatalogoGenericoDTO>>.Handle(CatalogoGenericoFiltro request, CancellationToken cancellationToken)
    {

        var parametros = new[]
        {
            new SqlParameter("@piCatTipoCatalogoAsuntoId", request.CatalogoId),
            new SqlParameter("@piCatOrganismoId", 1),
            new SqlParameter("@piCatTipoAsuntoId", 1),
        };
        var listaCatalogos = await _applicationDbContext.ExecuteStoredProc<CatalogoGenericoDTO>("usp_CatalogosSel", parametros.ToArray());
        return await Task.FromResult(listaCatalogos);
    }
}
