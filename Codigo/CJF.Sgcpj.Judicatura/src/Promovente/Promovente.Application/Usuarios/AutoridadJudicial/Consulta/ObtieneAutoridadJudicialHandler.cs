using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Promovente.Application.Common.Models;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Promovente.Application.Usuarios.AutoridadJudicial.Consulta;

public class ObtieneAutoridadJudicialHandler : IRequestHandler<ObtieneAutoridadJudicial,List<AutoridadJudicialDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ISesionService _sessionService;

    public ObtieneAutoridadJudicialHandler(IMapper mapper, IApplicationDbContext applicationDbContext, ISesionService sessionService)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
        _sessionService = sessionService;
    }
    async Task<List<AutoridadJudicialDto>> IRequestHandler<ObtieneAutoridadJudicial, List<AutoridadJudicialDto>>.Handle(ObtieneAutoridadJudicial request, CancellationToken cancellationToken)
    {
        
        var listaCatalogosAutoridadJudicial = new List<AutoridadJudicialDto>();
       
        List<SqlParameter> listaAutoridadParametros = new List<SqlParameter>();
        var nombre = new Microsoft.Data.SqlClient.SqlParameter("@pi_Nombre", request.Nombre);
        var catOrganismo = new Microsoft.Data.SqlClient.SqlParameter("@pi_CatOrganismoId", _sessionService.SesionActual.CatOrganismoId);
        listaAutoridadParametros.Add(nombre);
        listaAutoridadParametros.Add(catOrganismo);
       var itemsCatalogo = await _applicationDbContext.ExecuteStoredProc<BuscaAutoridadJudicial>("[SISE3].[pcAutoridadJudicialPorNombre]", listaAutoridadParametros.ToArray());
        listaCatalogosAutoridadJudicial = _mapper.Map<List<AutoridadJudicialDto>>(itemsCatalogo);
        return await Task.FromResult(listaCatalogosAutoridadJudicial);
    }
}

