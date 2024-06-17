using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Application.Catalogos.Tipo.Consulta;
using CJF.Sgcpj.Judicatura.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Application.EstadosTipo;
using CJF.Sgcpj.Judicatura.Domain.Entities;
using MediatR;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace CJF.Sgcpj.Judicatura.Application.Usuarios.Secretarios;

public class ObtieneCatalogoSecretarioHandler : IRequestHandler<ObtieneCatalogoSecretario, List<CatalogoSecretarioDto>>
{

    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;
    private readonly IApplicationDbContext _applicationDbContext;

    public ObtieneCatalogoSecretarioHandler(IMapper mapper, IApplicationDbContext applicationDbContext, ISesionService sesionService)
    { 
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
        _sesionService = sesionService;
    }

    async Task<List<CatalogoSecretarioDto>> IRequestHandler<ObtieneCatalogoSecretario, List<CatalogoSecretarioDto>>.Handle(ObtieneCatalogoSecretario request, CancellationToken cancellationToken)
    {
        var listaCatalogosSecretario = new List<CatalogoSecretarioDto>();        
        List<SqlParameter> listaSecretarioParametros = new List<SqlParameter>();
        request.CatOrganismoId = _sesionService.SesionActual.CatOrganismoId;
        var catOrganismoId = new Microsoft.Data.SqlClient.SqlParameter("@pi_CatOrganismoId", request.CatOrganismoId);
        listaSecretarioParametros.Add(catOrganismoId);
        var itemsCatalogo = await _applicationDbContext.ExecuteStoredProc<UsuarioSecretario>("uspx_getSecretariosPorOrganismo", listaSecretarioParametros.ToArray());
        listaCatalogosSecretario = _mapper.Map<List<CatalogoSecretarioDto>>(itemsCatalogo);
        return await Task.FromResult(listaCatalogosSecretario);
    }
}
