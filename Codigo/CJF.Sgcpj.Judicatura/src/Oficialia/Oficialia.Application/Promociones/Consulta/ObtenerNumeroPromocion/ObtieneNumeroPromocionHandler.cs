using AutoMapper;
using CJF.Sgcpj.Judicatura.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerNumeroPromocion;
public class ObtieneNumeroPromocionHandler : IRequestHandler<ObtieneNumeroPromocion, ObtieneNumeroPromocionDto>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ISesionService _sesionService;

    public ObtieneNumeroPromocionHandler(IMapper mapper, IApplicationDbContext applicationDbContext, ISesionService sesionService)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
        _sesionService = sesionService;
    }
    async Task<ObtieneNumeroPromocionDto> IRequestHandler<ObtieneNumeroPromocion, ObtieneNumeroPromocionDto>.Handle(ObtieneNumeroPromocion request, CancellationToken cancellationToken)
    {
        var numeroPromocionDto = new ObtieneNumeroPromocionDto();
        List<SqlParameter> parametros = new List<SqlParameter>();
        var pi_CatTipoOrganismoId = new Microsoft.Data.SqlClient.SqlParameter("@pi_CatOrganismoId", _sesionService.SesionActual.CatOrganismoId);
        var pi_NumeroRegistro = new Microsoft.Data.SqlClient.SqlParameter("@pi_NumeroRegistro", request.NumeroRegistro);
        var pi_YearPromocion = new Microsoft.Data.SqlClient.SqlParameter("@pi_YearPromocion", request.YearPromocion);
        parametros.Add(pi_CatTipoOrganismoId);
        parametros.Add(pi_NumeroRegistro);
        parametros.Add(pi_YearPromocion);
        numeroPromocionDto.Existe = await _applicationDbContext.ExecuteStoredProcObj<int>("[SISE3].[pcObtieneNumeroPromocion]", parametros.ToArray());
        
        return await Task.FromResult(numeroPromocionDto);
    }
}
