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

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Expediente.Consulta;
public record ObtieneCatalogoExpediente : IRequest<List<CatalogoExpedienteDto>>
{
    public int CatOrganismoId { get; set; }
}

public class ObtieneCatalogoExpedienteHandler : IRequestHandler<ObtieneCatalogoExpediente, List<CatalogoExpedienteDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ISesionService _sesionService;

    public ObtieneCatalogoExpedienteHandler(IMapper mapper, IApplicationDbContext applicationDbContext, ISesionService sesionService)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
        _sesionService = sesionService;
    }

    async Task<List<CatalogoExpedienteDto>> IRequestHandler<ObtieneCatalogoExpediente, List<CatalogoExpedienteDto>>.Handle(ObtieneCatalogoExpediente request, CancellationToken cancellationToken)
    {
        var listaCatalogos = new List<CatalogoExpedienteDto>();
        List<SqlParameter> parametros = new List<SqlParameter>();
        var itemsCatalogo = await _applicationDbContext.ExecuteStoredProc<CatalogoExpediente>("SISE3.pcCatalogoExpedientes");
        listaCatalogos = _mapper.Map<List<CatalogoExpedienteDto>>(itemsCatalogo);
        return await Task.FromResult(listaCatalogos);
    }
}
