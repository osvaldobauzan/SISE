using AutoMapper;
using CJF.Sgcpj.Judicatura.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Common.Models;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Oficialia.Application.Promociones.Consulta.ObtenerPromocionesFiltros;
public class ObtienePromocionesFiltrosConsultaHandler : IRequestHandler<ObtienePromocionesFiltrosConsulta, FiltroPromociones<FiltroSecretarioDto, FiltroOrigenDto, FiltroCapturoDto>>
{
    private readonly IMapper _mapper;
    private readonly IPromocionesRepository _repository;
    private readonly ISesionService _sesionService;
    public ObtienePromocionesFiltrosConsultaHandler(IMapper mapper, IPromocionesRepository repository, ISesionService sesionService)
    {
        _mapper = mapper;
        _repository = repository;
        _sesionService = sesionService;
    }

    public async Task<FiltroPromociones<FiltroSecretarioDto, FiltroOrigenDto, FiltroCapturoDto>> Handle(ObtienePromocionesFiltrosConsulta request, CancellationToken cancellationToken)
    {
        var filtrosPromociones = new FiltroPromociones<FiltroSecretarioDto, FiltroOrigenDto, FiltroCapturoDto>();
        request.CatOrganismoId = _sesionService.SesionActual.CatOrganismoId;
        var (data1, data2, data3) = await _repository.ObtenerFiltrosPromociones(request);

        filtrosPromociones.Secretario = _mapper.Map<List<FiltroSecretarioDto>>(data1);
        filtrosPromociones.Origen = _mapper.Map<List<FiltroOrigenDto>>(data2);
        filtrosPromociones.Capturo = _mapper.Map<List<FiltroCapturoDto>>(data3);

        return filtrosPromociones;

    }
}
