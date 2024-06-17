using AutoMapper;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models.Actuario;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.Actuario;
public class ActuarioNotificacionesConsultaHandler : IRequestHandler<ActuarioNotificacionesConsulta, ListaPaginada<ActuarioNotificacionesDto, ActuarioNotificacionesMetaDataEstadosDto>>
{
    private readonly IActuarioRepository _repository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;
    private readonly ILogger _logger;

    public ActuarioNotificacionesConsultaHandler(IActuarioRepository repository, IMapper mapper, ISesionService sesionService, ILogger logger)
    {
        _repository = repository;
        _mapper = mapper;
        _sesionService = sesionService;
        _logger = logger;
    }
    public async Task<ListaPaginada<ActuarioNotificacionesDto, ActuarioNotificacionesMetaDataEstadosDto>>
        Handle(ActuarioNotificacionesConsulta request, CancellationToken cancellationToken)
    {
        var listaPaginada = new ListaPaginada<ActuarioNotificacionesDto, ActuarioNotificacionesMetaDataEstadosDto>();

        try
        {
            var consultaPaginada = _mapper.Map<ConsultaPaginadaNotificaciones>(request);

            consultaPaginada.CatOrganismoId = _sesionService.SesionActual.CatOrganismoId;

            var (datos, metadatos) = await _repository.ObtenerActuarioNotificacionesConFiltro(consultaPaginada);

            listaPaginada.Datos = _mapper.Map<List<ActuarioNotificacionesDto>>(datos);
            listaPaginada.Pagina = metadatos.Pagina;
            listaPaginada.TotalPaginas = metadatos.TotalPaginas;
            listaPaginada.TotalRegistros = metadatos.TotalRegistros;

            listaPaginada.MetaDatos = _mapper.Map<ActuarioNotificacionesMetaDataEstadosDto>(metadatos);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex.ToString());
        }

        return listaPaginada;

    }

}
