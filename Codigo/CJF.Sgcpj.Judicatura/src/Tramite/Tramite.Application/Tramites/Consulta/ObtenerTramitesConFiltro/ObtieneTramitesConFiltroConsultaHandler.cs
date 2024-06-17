using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Repositories;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerTramitesConFiltro;
public class ObtieneTramitesConFiltroConsultaHandler : IRequestHandler<ObtieneTramitesConFiltroConsulta, ListaPaginada<TramiteDto, MetaDataEstadosTramiteDto>>
{
    private readonly ITramitesRepository _repository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sessionService;
    public ObtieneTramitesConFiltroConsultaHandler(ITramitesRepository repository, IMapper mapper, ISesionService sesionService)
    {
        _repository = repository;
        _mapper = mapper;
        _sessionService = sesionService;
    }

    public async Task<ListaPaginada<TramiteDto, MetaDataEstadosTramiteDto>> Handle(ObtieneTramitesConFiltroConsulta request, CancellationToken cancellationToken)
    {
        ListaPaginada<TramiteDto, MetaDataEstadosTramiteDto> listaPaginada = new ListaPaginada<TramiteDto, MetaDataEstadosTramiteDto>();
        var consultaPaginada = _mapper.Map<ConsultaPaginadaTramite>(request);
        consultaPaginada.OrganismoID = _sessionService.SesionActual.CatOrganismoId;
        consultaPaginada.UsuariId = _sessionService.SesionActual.EmpleadoId;
        var (datos, metadatos) = await _repository.ObtenerTramitesConFiltro(consultaPaginada);
        foreach (var datosTramite in datos)
        {
            if (datosTramite.Origen == 6 || datosTramite.Origen == 14 || datosTramite.Origen == 22 || datosTramite.Origen == 5 || datosTramite.Origen == 15 || datosTramite.Origen == 29)
                datosTramite.EsPromocionE = true;
            else
            {
                datosTramite.EsPromocionE = false;
                datosTramite.Origen = 0;
            }

                

        }
        listaPaginada.Datos = _mapper.Map<List<TramiteDto>>(datos);
        listaPaginada.MetaDatos = _mapper.Map<MetaDataEstadosTramiteDto>(metadatos);
        listaPaginada.TotalRegistros = request.Estado switch
        {
            0 => metadatos.TotalTramites,
            1 => metadatos.TotalSinAcuerdo,
            5 => metadatos.TotalCancelados,
            2 => metadatos.TotalConAcuerdo,
            3 => metadatos.TotalPreAutorizados,
            4 => metadatos.TotalAutorizados,

        };
        listaPaginada.Pagina = consultaPaginada.Pagina;
        listaPaginada.TotalPaginas = consultaPaginada.RegistrosPorPagina != 0 ? listaPaginada.TotalRegistros / consultaPaginada.RegistrosPorPagina + (listaPaginada.TotalRegistros % consultaPaginada.RegistrosPorPagina > 0 ? 1 : 0) : 0;
        return listaPaginada;
    }
}
