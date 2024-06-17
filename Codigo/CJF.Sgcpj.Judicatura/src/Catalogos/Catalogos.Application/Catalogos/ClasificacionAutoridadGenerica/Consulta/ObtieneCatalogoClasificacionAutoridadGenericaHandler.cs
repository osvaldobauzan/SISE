using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.TipoPersonaJuridica.Consulta;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.ClasificacionAutoridadGenerica.Consulta;
public class ObtieneCatalogoClasificacionAutoridadGenericaHandler: IRequestHandler<CatalogoClasificacionAutoridadGenericaFiltro, List<CatalogoClasificacionAutoridadGenericaDTO>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;

    public ObtieneCatalogoClasificacionAutoridadGenericaHandler(IMapper mapper, IApplicationDbContext applicationDbContext)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
    }
    async Task<List<CatalogoClasificacionAutoridadGenericaDTO>> IRequestHandler<CatalogoClasificacionAutoridadGenericaFiltro, List<CatalogoClasificacionAutoridadGenericaDTO>>.Handle(CatalogoClasificacionAutoridadGenericaFiltro request, CancellationToken cancellationToken)
    {
        List<SqlParameter> parametros = new List<SqlParameter>();
        var listaCatalogos = await _applicationDbContext.ExecuteStoredProc<CatalogoClasificacionAutoridadGenericaDTO>("usp_CatClasificaAutoridadGenericaSel", parametros.ToArray());
        return await Task.FromResult(listaCatalogos);
    }
}
