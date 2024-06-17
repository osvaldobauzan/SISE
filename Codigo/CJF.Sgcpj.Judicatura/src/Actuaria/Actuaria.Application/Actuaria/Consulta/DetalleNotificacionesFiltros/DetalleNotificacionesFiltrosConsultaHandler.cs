using AutoMapper;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.DetalleNotificacionesFiltros;
public class DetalleNotificacionesFiltrosConsultaHandler : IRequestHandler<DetalleNotificacionesFiltrosConsulta,
    FiltroDetalleNotificaciones<FiltroTipoParteDto, FiltroTipoNotificacionDto, FiltroActuarioDto>>
{
    private readonly IMapper _mapper;
    private readonly IActuariaRepository _repository;
    private readonly ISesionService _sesionService;

    public DetalleNotificacionesFiltrosConsultaHandler(IMapper mapper, IActuariaRepository repository, ISesionService sesionService)
    {
        _mapper = mapper;
        _repository = repository;
        _sesionService = sesionService;
    }
    public async Task<FiltroDetalleNotificaciones<FiltroTipoParteDto, FiltroTipoNotificacionDto, FiltroActuarioDto>> Handle(DetalleNotificacionesFiltrosConsulta request,
        CancellationToken cancellationToken)
    {
        var filtroDetalleNotificaciones = new FiltroDetalleNotificaciones<FiltroTipoParteDto, FiltroTipoNotificacionDto, FiltroActuarioDto>();
        request.CatOrganismoId = _sesionService.SesionActual.CatOrganismoId;
        var (data1, data2, data3) = await _repository.ObternerFiltroDetalleNotificaciones(request);

        filtroDetalleNotificaciones.TipoParte = _mapper.Map<List<FiltroTipoParteDto>>(data1);
        filtroDetalleNotificaciones.TipoNotificacion = _mapper.Map<List<FiltroTipoNotificacionDto>>(data2);
        filtroDetalleNotificaciones.Actuario = _mapper.Map<List<FiltroActuarioDto>>(data3);

        return filtroDetalleNotificaciones;
    }
}
