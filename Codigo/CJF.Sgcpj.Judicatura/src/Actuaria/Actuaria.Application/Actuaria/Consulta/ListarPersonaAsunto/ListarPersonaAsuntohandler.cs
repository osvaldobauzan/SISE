
using AutoMapper;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerPersonaAsunto;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ListarPersonaAsunto;
public class ListarPersonaAsuntohandler : IRequestHandler<ListarPersonaAsuntoRequestDto, List<ListarPersonaAsuntoDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;

    public ListarPersonaAsuntohandler(IMapper mapper, IApplicationDbContext applicationDbContext)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
    }

    public async Task<List<ListarPersonaAsuntoDto>> Handle(ListarPersonaAsuntoRequestDto request, CancellationToken cancellationToken)
    {
        List<SqlParameter> personaParameters = new List<SqlParameter>() {
            new SqlParameter("@pi_AsuntoNeunId", request.AsuntoNeunId),
            new SqlParameter("@pi_EmpleadoId", request.EmpleadoId)
        };
        var responseBd = await _applicationDbContext.ExecuteStoredProc<PersonasAsunto>("[SISE3].[ObtenerPersonaAsuntoXidEmpleado]", personaParameters.ToArray());
        var r = _mapper.Map<List<ListarPersonaAsuntoDto>>(responseBd);
        return await Task.FromResult(r);
    }
}