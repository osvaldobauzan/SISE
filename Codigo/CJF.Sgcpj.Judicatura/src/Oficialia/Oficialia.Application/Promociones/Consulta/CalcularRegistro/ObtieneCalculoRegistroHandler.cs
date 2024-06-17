using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.CalcularRegistro;
public class ObtieneCalculoRegistroHandler : IRequestHandler<ObtieneCalculoRegistro, RegistroDto>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ISesionService _sesionService;

    public ObtieneCalculoRegistroHandler(IMapper mapper, IApplicationDbContext applicationDbContext, ISesionService sesionService)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
        _sesionService = sesionService;
    }
    async Task<RegistroDto> IRequestHandler<ObtieneCalculoRegistro, RegistroDto>.Handle(ObtieneCalculoRegistro request, CancellationToken cancellationToken)
    {
        var registroDto = new RegistroDto();
        List<SqlParameter> lista2 = new List<SqlParameter>();
        var pi_CatTipoOrganismoId = new Microsoft.Data.SqlClient.SqlParameter("@pi_CatOrganismoId", _sesionService.SesionActual.CatOrganismoId);
        lista2.Add(pi_CatTipoOrganismoId);
        registroDto.Registro = await _applicationDbContext.ExecuteStoredProcObj<int>("[SISE3].[pcGeneraNumeroPromocion]", lista2.ToArray());        
        return await Task.FromResult(registroDto);
    }
}