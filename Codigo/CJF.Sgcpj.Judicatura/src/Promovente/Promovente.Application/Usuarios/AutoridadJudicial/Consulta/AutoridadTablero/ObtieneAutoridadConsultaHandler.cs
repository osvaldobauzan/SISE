using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Promovente.Application.Common.Models;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Promovente.Application.Usuarios.AutoridadJudicial.Consulta.AutoridadTablero;
public class ObtieneAutoridadConsultaHandler : IRequestHandler<ObtieneAutoridadConsulta, List<ObtieneAutoridadDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;
    public ObtieneAutoridadConsultaHandler(IMapper mapper, IApplicationDbContext applicationDbContext)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
    }
    async Task<List<ObtieneAutoridadDto>> IRequestHandler<ObtieneAutoridadConsulta, List<ObtieneAutoridadDto>>.Handle(ObtieneAutoridadConsulta request, CancellationToken cancellationToken)
    {
        var listaAutoridad = new List<ObtieneAutoridadDto>();
        List<SqlParameter> listaParametros = new List<SqlParameter>();
        var asuntoNeunId = new Microsoft.Data.SqlClient.SqlParameter("@pi_AsuntoNeunId", request.AsuntoNeunId);
        var pi_NoExp = new Microsoft.Data.SqlClient.SqlParameter("@pi_NoExp", request.NoExp);
        var pi_Texto = new Microsoft.Data.SqlClient.SqlParameter("@pi_Texto", request.Texto);
        var pi_Modulo = new Microsoft.Data.SqlClient.SqlParameter("@pi_Modulo", request.Modulo);
        var pi_TipoParte = new Microsoft.Data.SqlClient.SqlParameter("@pi_TipoParte", request.TipoParte);
        listaParametros.Add(asuntoNeunId);
        listaParametros.Add(pi_NoExp);
        listaParametros.Add(pi_Texto);
        listaParametros.Add(pi_Modulo);
        listaParametros.Add(pi_TipoParte);
        var itemsCatalogo = await _applicationDbContext.ExecuteStoredProc<Autoridad>("[SISE3].[pcPersonasXAsunto]", listaParametros.ToArray());
        listaAutoridad = _mapper.Map<List<ObtieneAutoridadDto>>(itemsCatalogo);

        return await Task.FromResult(listaAutoridad);
    }
}
