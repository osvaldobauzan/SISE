using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Usuarios.Application.Usuarios.BusquedaParteExistente.Consulta;
public class BusquedaParteHandler : IRequestHandler<BusquedaParteFiltro, List<BusquedaParteDTO>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ISesionService _sesionService;

    public BusquedaParteHandler(IMapper mapper, IApplicationDbContext applicationDbContext, ISesionService sesionService)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
        _sesionService = sesionService;
    }

    async Task<List<BusquedaParteDTO>> IRequestHandler<BusquedaParteFiltro, List<BusquedaParteDTO>>.Handle(BusquedaParteFiltro request, CancellationToken cancellationToken)
    {
        var listaPartes = new List<BusquedaParteDTO>();
        var parametros = new[]
        {
            new SqlParameter("@pi_CatOrganismoId", request.CatOrganismoId),
            new SqlParameter("@pi_CatTipoPersonaId", request.CatTipoPersonaId),
            new SqlParameter("@pi_Nombre", request.Nombre),
            new SqlParameter("@pi_APaterno", request.APaterno),
            new SqlParameter("@pi_AMaterno", request.AMaterno),
            new SqlParameter("@pi_FechaInicial", request.FechaInicial),
            new SqlParameter("@pi_FechaFinal", request.FechaFinal),
            new SqlParameter("@pi_Anio", request.Anio),
            new SqlParameter("@pi_CatTipoAsuntoId", request.CatTipoAsuntoId),
            new SqlParameter("@pi_OrganismoIdConsulta",  _sesionService.SesionActual.CatOrganismoId),
            new SqlParameter("@pi_UsuarioId",  _sesionService.SesionActual.EmpleadoId),
        };

        var itemsCatalogo = await _applicationDbContext.ExecuteStoredProc<BusquedaParteDTO>("SISE3.pcBuscarPersonaXOrganos", parametros);
      
        return await Task.FromResult(itemsCatalogo);
    }
}
