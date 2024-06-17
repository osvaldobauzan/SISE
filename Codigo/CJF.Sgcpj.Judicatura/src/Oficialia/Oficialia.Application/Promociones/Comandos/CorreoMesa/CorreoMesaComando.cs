using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.CorreoMesa;
public class CorreoMesaComando : IRequest<List<CorreoMesaDto>>
{
    public int EmpleadoIdResponsable { get; set; }
    public int CatOrganismoId { get; set; }
}

public class CorreoMesaComandoHandler : IRequestHandler<CorreoMesaComando, List<CorreoMesaDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ISesionService _sesionService;

    public CorreoMesaComandoHandler(IMapper mapper, IApplicationDbContext applicationDbContext, ISesionService sesionService)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
        _sesionService = sesionService;
    }

    async Task<List<CorreoMesaDto>> IRequestHandler<CorreoMesaComando, List<CorreoMesaDto>>.Handle(CorreoMesaComando request, CancellationToken cancellationToken)
    {
        List<CorreoMesaDto> listaCorreoMesa = new List<CorreoMesaDto>();
        List<SqlParameter> anexosParametros = new List<SqlParameter>();
        var piEmpleadoIdResponsable = new Microsoft.Data.SqlClient.SqlParameter("@pi_EmpleadoIdResponsable", request.EmpleadoIdResponsable);
        var piCatOrganismoId = new Microsoft.Data.SqlClient.SqlParameter("@pi_CatOrganismoId", request.CatOrganismoId);
        anexosParametros.Add(piEmpleadoIdResponsable);
        anexosParametros.Add(piCatOrganismoId);
        var itemsCatalogo = await _applicationDbContext.ExecuteStoredProc<Judicatura.Common.Application.Common.Models.CorreoMesa> ("[SISE3].[pcConsultaCorreoMesa]", anexosParametros.ToArray());
        listaCorreoMesa = _mapper.Map<List<CorreoMesaDto>>(itemsCatalogo);
        return await Task.FromResult(listaCorreoMesa);
    }
}
