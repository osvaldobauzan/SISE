using AutoMapper;
using CJF.Sgcpj.Judicatura.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerPromocionesConFiltro;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtienePromocionesConFiltro;

public class ObtienePromocionesConFiltroConsultaHandler : IRequestHandler<ObtienePromocionesConFiltroConsulta, ListaPaginada<PromocionDto, MetaDataEstadosDto>>
{
    private readonly IPromocionesRepository _repository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sessionService;

    public ObtienePromocionesConFiltroConsultaHandler(IPromocionesRepository repository, IMapper mapper, ISesionService sessionService)
    {
        _repository = repository;
        _mapper = mapper;
        _sessionService = sessionService;
    }

    public async Task<ListaPaginada<PromocionDto, MetaDataEstadosDto>> Handle(ObtienePromocionesConFiltroConsulta request, CancellationToken cancellationToken)
    {
        ListaPaginada<PromocionDto, MetaDataEstadosDto> listaPaginada = new ListaPaginada<PromocionDto, MetaDataEstadosDto>();
        var consultaPaginada = _mapper.Map<ConsultaPaginada>(request);
        consultaPaginada.OrganismoID = _sessionService.SesionActual.CatOrganismoId;

        var (datos,metadatos) =  await _repository.ObtenerPromocionesConFiltro(consultaPaginada);
        foreach (var dato in datos)
        {
            if ((new[] { "OCC", "INTERCONEXIÓN OJ", "INTERCONEXIÓN" }.Contains(dato.OrigenPromocionDescripcion)) && dato.Estado == 1)
            {
                dato.ParteDescripcion = dato.NombreOficial;
            }

            if (new[] { "INTERCONEXIÓN OJ", "INTERCONEXIÓN" }.Contains(dato.OrigenPromocionDescripcion))
            {
                dato.NombreOrigen = "Promoción Electrónica";
            }


        }

        listaPaginada.Datos = _mapper.Map<List<PromocionDto>>( datos);
        listaPaginada.MetaDatos = _mapper.Map<MetaDataEstadosDto>(metadatos);
        listaPaginada.TotalRegistros = request.Estado switch { 
            0 => metadatos.TotalPromociones,
            1 => metadatos.TotalSinCaptura,
            2 => metadatos.TotalCapturadas,
            4 => metadatos.EnviadasAMesa
        };
        listaPaginada.Pagina = consultaPaginada.Pagina;
        listaPaginada.TotalPaginas = consultaPaginada.RegistrosPorPagina != 0 ? (listaPaginada.TotalRegistros / consultaPaginada.RegistrosPorPagina) + ((listaPaginada.TotalRegistros % consultaPaginada.RegistrosPorPagina) > 0 ? 1 : 0) : 0;

        return  listaPaginada;
    }


}
