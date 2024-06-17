using AutoMapper;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerNotificacionesConFiltro;
public class ObtieneNotificacionesConFiltroConsultaHandler : IRequestHandler<ObtieneNotificacionesConFiltroConsulta, ListaPaginada<NotificacionDto, MetaDataEstadosDto>>
{
    private readonly IActuariaRepository _repository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;

    public ObtieneNotificacionesConFiltroConsultaHandler(IActuariaRepository repository, IMapper mapper, ISesionService sesionService)
    {
        _repository = repository;
        _mapper = mapper;
        _sesionService = sesionService;
    }
    public async Task<ListaPaginada<NotificacionDto, MetaDataEstadosDto>> Handle(ObtieneNotificacionesConFiltroConsulta request, CancellationToken cancellationToken)
    {
        ListaPaginada<NotificacionDto, MetaDataEstadosDto> listaPaginada = new ListaPaginada<NotificacionDto, MetaDataEstadosDto>();
        var consultaPaginada = _mapper.Map<ConsultaPaginada>(request);

        consultaPaginada.CatOrganismoId = _sesionService.SesionActual.CatOrganismoId;
        var (datos, metadatos) = await _repository.ObtenerNotificacionesConFiltro(consultaPaginada);
        listaPaginada.Datos = _mapper.Map<List<NotificacionDto>>(datos);
        listaPaginada.MetaDatos = _mapper.Map<MetaDataEstadosDto>(metadatos);
        listaPaginada.TotalRegistros = request.TipoFiltro switch
        {
            0 => metadatos.TotalNotificaciones,
            3 => metadatos.TotalUnDia,
            1 => metadatos.TotalDosDias,
            2 => metadatos.TotalMasTresDias,
            4 => metadatos.TotalNotificados,           

        };
        listaPaginada.Pagina = consultaPaginada.Pagina;
        listaPaginada.TotalPaginas = consultaPaginada.RegistrosPorPagina != 0 ? listaPaginada.TotalRegistros / consultaPaginada.RegistrosPorPagina + (listaPaginada.TotalRegistros % consultaPaginada.RegistrosPorPagina > 0 ? 1 : 0) : 0;
        return listaPaginada;
    }
}
