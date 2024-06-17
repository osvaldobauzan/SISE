using AutoMapper;
using CJF.Sgcpj.Judicatura.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerPromocionXExpediente;
public class ObtienePromocionXExpedienteHandler : IRequestHandler<ObtienePromocionXExpediente, List<ObtienePromocionXExpedienteDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;
    public ObtienePromocionXExpedienteHandler(IMapper mapper, IApplicationDbContext applicationDbContext)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
    }

    async Task<List<ObtienePromocionXExpedienteDto>> IRequestHandler<ObtienePromocionXExpediente, List<ObtienePromocionXExpedienteDto>>.Handle(ObtienePromocionXExpediente request, CancellationToken cancellationToken)
    {
        var promocionXExpediente = new List<ObtienePromocionXExpedienteDto>();
        List<SqlParameter> parametros = new List<SqlParameter>();
        var pi_AsuntoNeunId = new Microsoft.Data.SqlClient.SqlParameter("@pi_AsuntoNeunId", request.AsuntoNeunId);
        var pi_NoExpediente = new Microsoft.Data.SqlClient.SqlParameter("@pi_NoExpediente", request.NoExpediente);
        parametros.Add(pi_AsuntoNeunId);
        parametros.Add(pi_NoExpediente);
        var itemsPromocionXExpediente = await _applicationDbContext.ExecuteStoredProc<PromocionXExpediente>("[SISE3].[pcListaPromocionesXExpediente]", parametros.ToArray());
        promocionXExpediente = _mapper.Map<List<ObtienePromocionXExpedienteDto>>(itemsPromocionXExpediente);
        
        return await Task.FromResult(promocionXExpediente );
    }
}
