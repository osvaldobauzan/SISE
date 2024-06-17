using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Audiencia.Consulta;
public class CatalogoAudienciaHandler : IRequestHandler<CatalogAudienciaRequest, List<CatalogoAudienciaDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ISesionService _sesionService;

    public CatalogoAudienciaHandler(IMapper mapper, IApplicationDbContext applicationDbContext, ISesionService sesionService)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
        _sesionService = sesionService;
    }

    async Task<List<CatalogoAudienciaDto>> IRequestHandler<CatalogAudienciaRequest, List<CatalogoAudienciaDto>>.Handle(CatalogAudienciaRequest request, CancellationToken cancellationToken)
    {
        List<CatalogoAudienciaDto> listaCatalogos = new List<CatalogoAudienciaDto>();

        List<SqlParameter> listaCatalogoAudienciaSql = new List<SqlParameter>();

        var catOrganismoId = new Microsoft.Data.SqlClient.SqlParameter("@pi_CatTipoOrganismoId", _sesionService.SesionActual.CatOrganismoId);
        var tipoAsuntoId = new Microsoft.Data.SqlClient.SqlParameter("@pi_IdTipoAsunto", request.TipoAsuntoId);
        var tipoAgendaId = new Microsoft.Data.SqlClient.SqlParameter("@pi_IdTipoAgenda", 1);

        listaCatalogoAudienciaSql.Add(catOrganismoId);
        listaCatalogoAudienciaSql.Add(tipoAsuntoId);
        listaCatalogoAudienciaSql.Add(tipoAgendaId);

        var itemsCatalogo = await _applicationDbContext.ExecuteStoredProc<CatalogoAudiencia>("[SISE3].[pcAgendaCatAudiencias]", listaCatalogoAudienciaSql.ToArray());

        listaCatalogos = _mapper.Map<List<CatalogoAudienciaDto>>(itemsCatalogo);
        return await Task.FromResult(listaCatalogos);
    }

}
