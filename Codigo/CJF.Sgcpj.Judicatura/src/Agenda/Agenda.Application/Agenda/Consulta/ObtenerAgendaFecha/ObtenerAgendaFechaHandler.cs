using System.Collections.Generic;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Agenda.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Consulta.ObtenerAgendaFecha;
public class ObtenerAgendaFechaHandler : IRequestHandler<ObtenerAgendaFechaRequest, List<ObtenerAgendaFechaDto>>
{
    private readonly IAgendaRepository _repository;
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ISesionService _sesionService;
    private readonly ILogger _logger;

    public ObtenerAgendaFechaHandler(IAgendaRepository reposiroty, IMapper mapper, IApplicationDbContext applicationDbContext, ISesionService sesionService, ILogger logger)
    {
        _repository = reposiroty;
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
        _sesionService = sesionService;
        _logger = logger;
    }

    async Task<List<ObtenerAgendaFechaDto>> IRequestHandler<ObtenerAgendaFechaRequest, List<ObtenerAgendaFechaDto>>.Handle(ObtenerAgendaFechaRequest request, CancellationToken cancellationToken)
    {
        List<ObtenerAgendaFechaDto> lstResultObtenerFecha = new List<ObtenerAgendaFechaDto>();
        try
        {
            if (request.FechaIni != null && request.FechaFin != null || request.Expediente != null || request.Persona != null)
            {
                request.CatIdOrganismo = _sesionService.SesionActual.CatOrganismoId;
                lstResultObtenerFecha = await _repository.ObtenerAgendaFecha(request);
            }
            else
            {
                return lstResultObtenerFecha;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message.ToString(), request);
        }
        return lstResultObtenerFecha;
    }


}
