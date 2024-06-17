using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.Filtros;
public class FiltroConsultaHandler : IRequestHandler<FiltrosConsulta, FiltroRootDto>
{
    private readonly IMapper _mapper;
    private readonly IActuariaRepository _repository;
    private readonly ISesionService _sesionService;

    public FiltroConsultaHandler(IMapper mapper, IActuariaRepository repository, ISesionService sesionService)
    {
        _mapper = mapper;
        _repository = repository;
        _sesionService = sesionService;
    }
    public async Task<FiltroRootDto> Handle(FiltrosConsulta request, CancellationToken cancellationToken)
    {

        FiltroRootDto resultado = new FiltroRootDto();
            ;
        var (estadosModels,contenidosModels) = await _repository.ObtenenerFiltrosTablero(_sesionService.SesionActual.CatTipoOrganismoId);

        resultado.FiltroEstado = _mapper.Map<List<FiltroEstadoDto>>(estadosModels);
        resultado.FiltroContenido = _mapper.Map<List<FiltroContenidoDto>>(contenidosModels);
        return resultado;
    }
}
