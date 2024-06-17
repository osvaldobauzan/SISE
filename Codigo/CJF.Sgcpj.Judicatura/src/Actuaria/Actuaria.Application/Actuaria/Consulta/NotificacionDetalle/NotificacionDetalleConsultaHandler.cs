using AutoMapper;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.DetalleNotificaciones;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.NotificacionDetalle;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerNotificacionesConFiltro;
public class NotificacionDetalleConsultaHandler : IRequestHandler<NotificacionDetalleConsulta, ListaPaginada<NotificacionDetalleDto, NotificacionDetalleMetaDataEstadosDto>>
{
    private readonly IActuariaRepository _repository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;

    public NotificacionDetalleConsultaHandler(IActuariaRepository repository, IMapper mapper, ISesionService sesionService)
    {
        _repository = repository;
        _mapper = mapper;
        _sesionService = sesionService;
    }
    public async Task<ListaPaginada<NotificacionDetalleDto, NotificacionDetalleMetaDataEstadosDto>>
        Handle(NotificacionDetalleConsulta request, CancellationToken cancellationToken)
    {
        ListaPaginada<NotificacionDetalleDto, NotificacionDetalleMetaDataEstadosDto> listaPaginada = new ListaPaginada<NotificacionDetalleDto, NotificacionDetalleMetaDataEstadosDto>();
        var consultaPaginada = _mapper.Map<ConsultaPaginadaDetalle>(request);

        consultaPaginada.CatOrganismoId = _sesionService.SesionActual.CatOrganismoId;
        var (datosAsunto, datos, metadatos) = await _repository.ObtenerNotificacionesDetalleConFiltro(consultaPaginada);
        listaPaginada.Datos = _mapper.Map<List<NotificacionDetalleDto>>(datos);
        listaPaginada.Pagina = metadatos.Pagina;
        listaPaginada.TotalPaginas = metadatos.TotalPaginas;
        listaPaginada.TotalRegistros = metadatos.TotalRegistros;

        listaPaginada.MetaDatos = _mapper.Map<NotificacionDetalleMetaDataEstadosDto>(metadatos);
        listaPaginada.MetaDatos.DatosAsunto = _mapper.Map<DatosAsuntoDto>(datosAsunto);
       
        return listaPaginada;
    }
}
