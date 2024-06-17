using AutoMapper;
using CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Consulta.ObtenerDetalleCaracter;
using CJF.Sgcpj.Judicatura.Agenda.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Consulta.ObtenerDetalleActor;
public class ObtenerDetalleCaracterHandler : IRequestHandler<ObtenerDetalleCaracterRequest, List<ObtenerDetalleCaracterDto>>
{
    private readonly IAgendaRepository _repository;
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ISesionService _sesionService;
    private readonly ILogger _logger;

    public ObtenerDetalleCaracterHandler(IAgendaRepository repository, IMapper mapper, IApplicationDbContext applicationDbContext, ISesionService sesionService, ILogger logger)
    {
        _repository = repository;
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
        _sesionService = sesionService;
        _logger = logger;
    }

    async Task<List<ObtenerDetalleCaracterDto>> IRequestHandler<ObtenerDetalleCaracterRequest, List<ObtenerDetalleCaracterDto>>.Handle(ObtenerDetalleCaracterRequest request, CancellationToken cancellationToken)
    {
        List<ObtenerDetalleCaracterDto> lstResultCaracter = new List<ObtenerDetalleCaracterDto> ();
        try
        {
            lstResultCaracter = await _repository.ObtenerDetalleCaracter(request);           
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message.ToString(), request);
        }
        return lstResultCaracter;
    }

}
