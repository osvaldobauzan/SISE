using AutoMapper;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Audiencia.Consulta;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.ResultadoAudiencia.Consulta;

public class CatalogoResultadoHandler : IRequestHandler<CatalogoResultadoRequest, List<CatalogoResultadoDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ISesionService _sesionService;

    public CatalogoResultadoHandler(IMapper mapper, IApplicationDbContext applicationDbContext, ISesionService sesionService)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
        _sesionService = sesionService;
    }
    async Task<List<CatalogoResultadoDto>> IRequestHandler<CatalogoResultadoRequest, List<CatalogoResultadoDto>>.Handle(CatalogoResultadoRequest request, CancellationToken cancellationToken)
    {
        List<CatalogoResultadoDto> lstResultado = new List<CatalogoResultadoDto>();
        List<SqlParameter> lstParamsResultado = new List<SqlParameter>();

        var idTipoAudiencia = new Microsoft.Data.SqlClient.SqlParameter("@pi_IdTipoAudiencia", request.IdTipoAudiencia);
        lstParamsResultado.Add(idTipoAudiencia);

        var itemsCatalogo = await _applicationDbContext.ExecuteStoredProc<CatalogoResultado>("[SISE3].[pcCatResultadoAudiencia]", lstParamsResultado.ToArray());

        lstResultado = _mapper.Map<List<CatalogoResultadoDto>>(itemsCatalogo);
        return await Task.FromResult(lstResultado);
    }

}
