﻿using AutoMapper;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Generico.Consulta;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.TipoSentenciaProyecto.Consulta;

public class ObtieneCatalogoTipoSentenciaHandler : IRequestHandler<ObtieneCatalogoTipoSentencia, List<CatalogoGenericoDTO>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ISesionService _sesionService;

    public ObtieneCatalogoTipoSentenciaHandler(IMapper mapper, IApplicationDbContext applicationDbContext, ISesionService sesionService)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
        _sesionService = sesionService;
    }

    async Task<List<CatalogoGenericoDTO>> IRequestHandler<ObtieneCatalogoTipoSentencia, List<CatalogoGenericoDTO>>.Handle(ObtieneCatalogoTipoSentencia request, CancellationToken cancellationToken)
    {
        List<SqlParameter> parametrosEmpleado = new()
        {
            new SqlParameter("@pi_CatTipoAsuntoId", request.CatTipoAsuntoId),
            new SqlParameter("@ps_TipoCatalogo", request.TipoCatalogo),
        };

        var itemsCatalogo = await _applicationDbContext.ExecuteStoredProc<CatalogoGenericoDTO>("[SISE3].[pcObtieneElementosCatalogosProyecto]", parametrosEmpleado.ToArray());

        return await Task.FromResult(itemsCatalogo);
    }
}
