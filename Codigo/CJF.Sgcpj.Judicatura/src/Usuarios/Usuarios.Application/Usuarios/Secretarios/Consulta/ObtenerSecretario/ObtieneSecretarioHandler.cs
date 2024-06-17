using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Usuarios.Application.Common.Models;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Usuarios.Application.Usuarios.Secretarios.Consulta.ObtenerSecretario;
public class ObtieneSecretarioHandler : IRequestHandler<ObtieneSecretario, ObtieneSecretarioDto>
{
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;
    private readonly IApplicationDbContext _applicationDbContext;

    public ObtieneSecretarioHandler(IMapper mapper, IApplicationDbContext applicationDbContext, ISesionService sesionService)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
        _sesionService = sesionService;
    }

    async Task<ObtieneSecretarioDto?> IRequestHandler<ObtieneSecretario, ObtieneSecretarioDto>.Handle(ObtieneSecretario request, CancellationToken cancellationToken)
    {
        
        long secretarioSugerido;

        List<SqlParameter> secretarioParametros = new List<SqlParameter>();
        long? secretarioId = null;
        var asuntoNeunId = new SqlParameter("@pi_AsuntoNeunId", request.AsuntoNeunId);
        secretarioParametros.Add(asuntoNeunId);
        request.CatOrganismoId = _sesionService.SesionActual.CatOrganismoId;
        var catOrganismoId = new SqlParameter("@pi_CatOrganismoId", request.CatOrganismoId);
        secretarioParametros.Add(catOrganismoId);
        secretarioId = (long?)await _applicationDbContext.ExecuteStoredProcObj<object?>("SISE3.pcObtieneUltimoSecretarioExpediente", secretarioParametros.ToArray());
        var resultadoSecretario = new ObtieneSecretarioDto();

        resultadoSecretario.Secretario = secretarioId;
        return resultadoSecretario;
    }
}
