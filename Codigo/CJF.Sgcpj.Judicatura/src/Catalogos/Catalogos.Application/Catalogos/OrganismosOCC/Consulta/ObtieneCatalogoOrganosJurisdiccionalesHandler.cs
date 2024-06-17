using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.OrganismosOCC.Consulta;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.TipoPersona.Consulta;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Generico.Consulta;
public class OrganosJurisdiccionalesFiltro : IRequest<List<OrganosJurisdiccionalesDTO>>
{
}
public class ObtieneCatalogoOrganosJurisdiccionalesHandler : IRequestHandler<OrganosJurisdiccionalesFiltro, List<OrganosJurisdiccionalesDTO>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;

    public ObtieneCatalogoOrganosJurisdiccionalesHandler(IMapper mapper, IApplicationDbContext applicationDbContext)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
    }
    async Task<List<OrganosJurisdiccionalesDTO>> IRequestHandler<OrganosJurisdiccionalesFiltro, List<OrganosJurisdiccionalesDTO>>.Handle(OrganosJurisdiccionalesFiltro request, CancellationToken cancellationToken)
    {

        var parametros = new List<SqlParameter>();
        var listaCatalogos = await _applicationDbContext.ExecuteStoredProc<OrganosJurisdiccionalesDTO>("SISE3.pcObtenerOrganosJurisdiccionales", parametros.ToArray());
        return await Task.FromResult(listaCatalogos);
    }
}
