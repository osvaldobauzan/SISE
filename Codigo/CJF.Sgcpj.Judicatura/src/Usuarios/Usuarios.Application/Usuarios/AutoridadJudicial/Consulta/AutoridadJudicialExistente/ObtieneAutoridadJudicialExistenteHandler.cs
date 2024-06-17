using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Usuarios.Application.Common.Models;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Usuarios.Application.Usuarios.AutoridadJudicial.Consulta.AutoridadJudicialExistente;
public class ObtieneAutoridadJudicialExistenteConsultaHandler : IRequestHandler<ObtieneAutoridadJudicialExistenteConsulta, List<ObtieneAutoridadJudicialExistenteDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;
    public ObtieneAutoridadJudicialExistenteConsultaHandler(IMapper mapper, IApplicationDbContext applicationDbContext)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
    }
    async Task<List<ObtieneAutoridadJudicialExistenteDto>> IRequestHandler<ObtieneAutoridadJudicialExistenteConsulta, List<ObtieneAutoridadJudicialExistenteDto>>.Handle(ObtieneAutoridadJudicialExistenteConsulta request, CancellationToken cancellationToken)
    {
        var listaAutoridad = new List<ObtieneAutoridadJudicialExistenteDto>();
        List<SqlParameter> listaParametros = new List<SqlParameter>();
        var asuntoNeunId = new SqlParameter("@pi_AsuntoNeunId", request.AsuntoNeunId);
        var pi_NoExp = new SqlParameter("@pi_NoExp", request.NoExp);

        listaParametros.Add(asuntoNeunId);
        listaParametros.Add(pi_NoExp);

        var itemsCatalogo = await _applicationDbContext.ExecuteStoredProc<Autoridad>("[SISE3].[pcAutoridadJudicialXAsunto]", listaParametros.ToArray());
        listaAutoridad = _mapper.Map<List<ObtieneAutoridadJudicialExistenteDto>>(itemsCatalogo);

        return await Task.FromResult(listaAutoridad);
    }
        
}
