using AutoMapper;
using CJF.Sgcpj.Judicatura.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerPromocionDetalle;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using DocumentFormat.OpenXml.Spreadsheet;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Oficialia.Application.Promociones.Consulta.ObtenerPromocionDetalle;
public class ObtieneLecturaPromocionDetalleHandler : IRequestHandler<ObtieneLecturaPromocionDetalle, ListaDetallePromocionTablero<PromocionDetalleTableroDto, AnexoListaDto>>
{
    private readonly IPromocionesRepository _repository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;

    public ObtieneLecturaPromocionDetalleHandler(IPromocionesRepository repository, IMapper mapper, ISesionService sesionService)
    {
        _repository = repository;
        _mapper = mapper;
        _sesionService = sesionService;
    }

    public async Task<ListaDetallePromocionTablero<PromocionDetalleTableroDto, AnexoListaDto>> Handle(ObtieneLecturaPromocionDetalle request, CancellationToken cancellationToken)
    {

        ListaDetallePromocionTablero<PromocionDetalleTableroDto, AnexoListaDto> listaDetallePromocionTablero = new ListaDetallePromocionTablero<PromocionDetalleTableroDto, AnexoListaDto>();

        if (request.Origen != 0 && request.EsPromocionE && request.Estado == 4)
        {
            var (datosE, metadatosE) = await _repository.ObtenerPromocionDetalleTableroElectronicas(_sesionService.SesionActual.CatOrganismoId,
                        request.AsuntoNeunId, request.UsuariId, request.Origen, request.NumeroOrden, request.YearPromocion, request.kIdElectronica);
            listaDetallePromocionTablero.Datos = _mapper.Map<List<PromocionDetalleTableroDto>>(datosE);
            listaDetallePromocionTablero.Anexos = _mapper.Map<List<AnexoListaDto>>(metadatosE);
            var primerDetalle = listaDetallePromocionTablero.Datos.FirstOrDefault();
            if (primerDetalle != null)
            {
                primerDetalle.OrigenPromocion = request.Tipo + " - " + request.SubTipo;

                if (primerDetalle.HoraPresentacion == null && primerDetalle.OrigenPromocionId != 4)
                {
                    primerDetalle.HoraPresentacion = request.HoraPresentacion;
                }
            }

        }
        else
        {
            var (datos, metadatos) = await _repository.ObtenerPromocionDetalleTablero(_sesionService.SesionActual.CatOrganismoId,
           request.AsuntoNeunId, request.UsuariId, request.Origen, request.NumeroOrden, request.YearPromocion, request.kIdElectronica);
            listaDetallePromocionTablero.Datos = _mapper.Map<List<PromocionDetalleTableroDto>>(datos);
            listaDetallePromocionTablero.Anexos = _mapper.Map<List<AnexoListaDto>>(metadatos);
            var primerDetalle = listaDetallePromocionTablero.Datos.FirstOrDefault();
            if (primerDetalle != null && primerDetalle.HoraPresentacion == null && (primerDetalle.OrigenPromocionId == null || primerDetalle.OrigenPromocionId != 4))
            {
                primerDetalle.OrigenPromocion = request.Tipo + " - " + request.SubTipo;
                if (primerDetalle.OrigenPromocionId == 6 || primerDetalle.OrigenPromocionId == 1)
                {
                    if (primerDetalle.FechaPresentacion.HasValue)
                    {
                        int year = primerDetalle.FechaPresentacion.Value.Year;
                        primerDetalle.FechaAlta = primerDetalle.FechaPresentacion;
                        primerDetalle.OCC = primerDetalle.OCC + "/" + year.ToString();
                    }

                }
                if (primerDetalle.FechaPresentacion.HasValue)
                {
                    string horaPresentacionAux = primerDetalle.FechaPresentacion.Value.ToString("HH:mm");
                    primerDetalle.HoraPresentacion = horaPresentacionAux;
                }
                if (primerDetalle.OrigenPromocion == null)
                {
                    primerDetalle.OrigenPromocion = request.Tipo;
                }

            }

            if (primerDetalle != null && primerDetalle.OrigenPromocionId == 22 || primerDetalle?.OrigenPromocionId == 14 || primerDetalle?.OrigenPromocionId == 6)
            {
                primerDetalle.PromoventeNombre = primerDetalle.NombreOficial;
            }

        }      



        return listaDetallePromocionTablero;
    }
}
