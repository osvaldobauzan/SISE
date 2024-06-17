using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Generico.Consulta;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Sexo.Consulta;
public class ObtieneCatalogoSexoHandler : IRequestHandler<CatalogoSexoFiltro, List<CatalogoSexoDTO>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;
    public ObtieneCatalogoSexoHandler(IMapper mapper, IApplicationDbContext applicationDbContext)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
    }
    async Task<List<CatalogoSexoDTO>> IRequestHandler<CatalogoSexoFiltro, List<CatalogoSexoDTO>>.Handle(CatalogoSexoFiltro request, CancellationToken cancellationToken)
    {
        List<SqlParameter> parametros = new List<SqlParameter>();
        var listaCatalogos = await _applicationDbContext.ExecuteStoredProc<CatalogoSexoDTO>("SJPA_pcCTSexo", parametros.ToArray());
        listaCatalogos.Add(new CatalogoSexoDTO { KIdSexo = 0, sDescripcion = "Se desconoce" });
        listaCatalogos = listaCatalogos.OrderBy(o=>o.KIdSexo).ToList();

        return await Task.FromResult(listaCatalogos);
    }
}
