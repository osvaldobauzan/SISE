using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Repositories;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerFiltrosTramite;
public class ObtieneFiltroTramiteConsultaHandler : IRequestHandler<ObtieneFiltroTramiteConsulta,
    FiltroTramite<ObtieneFiltroSecretarioDto, ObtieneFiltroOrigenDto, ObtieneFiltroTipoAsuntoDto,
    ObtieneFiltroCapturoDto, ObtieneFiltroPreautorizoDto, ObtieneFiltroAutorizoDto, ObtieneFiltroCanceloDto>>
{
    private readonly IMapper _mapper;
    private readonly ITramitesRepository _repository;
    private readonly ISesionService _sesionService;

    public ObtieneFiltroTramiteConsultaHandler(IMapper mapper, ITramitesRepository repository, ISesionService sesionService)
    {
        _mapper = mapper;
        _repository = repository;
        _sesionService = sesionService;
    }
    public async Task<FiltroTramite<ObtieneFiltroSecretarioDto, ObtieneFiltroOrigenDto, ObtieneFiltroTipoAsuntoDto, ObtieneFiltroCapturoDto,
        ObtieneFiltroPreautorizoDto, ObtieneFiltroAutorizoDto, ObtieneFiltroCanceloDto>> Handle(ObtieneFiltroTramiteConsulta request, CancellationToken cancellationToken)
    {
        var filtroTramite = new FiltroTramite<ObtieneFiltroSecretarioDto, ObtieneFiltroOrigenDto, ObtieneFiltroTipoAsuntoDto,
            ObtieneFiltroCapturoDto, ObtieneFiltroPreautorizoDto, ObtieneFiltroAutorizoDto, ObtieneFiltroCanceloDto>();
        request.CatOrganismoId = _sesionService.SesionActual.CatOrganismoId;

        var (data1, data2, data3, data4, data5, data6, data7) = await _repository.ObtenerFiltroTramite(request);

        filtroTramite.Secretario = _mapper.Map<List<ObtieneFiltroSecretarioDto>>(data1);
        filtroTramite.Origen = _mapper.Map<List<ObtieneFiltroOrigenDto>>(data2);
        filtroTramite.TipoAsunto = _mapper.Map<List<ObtieneFiltroTipoAsuntoDto>>(data3);
        filtroTramite.Capturo = _mapper.Map<List<ObtieneFiltroCapturoDto>>(data4);
        filtroTramite.Preautorizo = _mapper.Map<List<ObtieneFiltroPreautorizoDto>>(data5);
        filtroTramite.Autorizo = _mapper.Map<List<ObtieneFiltroAutorizoDto>>(data6);
        filtroTramite.Cancelo = _mapper.Map<List<ObtieneFiltroCanceloDto>>(data7);

        return filtroTramite;
    }
}
