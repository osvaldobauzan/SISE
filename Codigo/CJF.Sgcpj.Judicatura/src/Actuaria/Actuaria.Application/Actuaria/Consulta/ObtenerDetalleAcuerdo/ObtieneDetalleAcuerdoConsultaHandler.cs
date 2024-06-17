using AutoMapper;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerDetalleAcuerdo;
public class ObtieneDetalleAcuerdoConsultaHandler : IRequestHandler<ObtieneDetalleAcuerdoConsulta, ListaDetalleAcuerdo<DetalleAcuerdoDto, PromocionDto>>
{
    private readonly IMapper _mapper;
    private readonly IActuariaRepository _repository;
    private readonly ISesionService _sesionService;

    public ObtieneDetalleAcuerdoConsultaHandler(IMapper mapper, IActuariaRepository repository, ISesionService sesionService)
    {
        _mapper = mapper;
        _repository = repository;
        _sesionService = sesionService;
    }
    public async Task<ListaDetalleAcuerdo<DetalleAcuerdoDto, PromocionDto>> Handle(ObtieneDetalleAcuerdoConsulta request, CancellationToken cancellationToken)
    {
        ListaDetalleAcuerdo<DetalleAcuerdoDto, PromocionDto>  listaDetalleAcuerdo = new ListaDetalleAcuerdo<DetalleAcuerdoDto, PromocionDto>();
        
        var (dataSet1, dataSet2) = await _repository.ObtenerDetalleAcuerdo(_sesionService.SesionActual.CatOrganismoId, 
            request.AsuntoNeunId, request.SintesisOrden, request.AsuntoDocumentoId);

        listaDetalleAcuerdo.Datos = _mapper.Map<List<DetalleAcuerdoDto>>(dataSet1);
        listaDetalleAcuerdo.Promociones = _mapper.Map<List<PromocionDto>>(dataSet2);

        return listaDetalleAcuerdo;
    }
}
