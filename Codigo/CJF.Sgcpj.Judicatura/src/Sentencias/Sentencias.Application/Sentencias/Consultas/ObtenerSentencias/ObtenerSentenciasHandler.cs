using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Sentencias.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Sentencias.Application.Common.Repositories;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Sentencias.Application.Sentencias.Consultas.ObtenerSentencias;

public class ObtenerSentenciasHandler : IRequestHandler<ObtenerSentenciasFiltro, TableroSentenciasDto>
{
    private readonly ISentenciasRepository _repository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sessionService;

    public ObtenerSentenciasHandler(ISentenciasRepository repository, IMapper mapper, ISesionService sessionService)
    {
        _repository = repository;
        _mapper = mapper;
        _sessionService = sessionService;
    }

    public async Task<TableroSentenciasDto> Handle(ObtenerSentenciasFiltro request, CancellationToken cancellationToken)
    {
        var filtros = _mapper.Map<ConsultaSentencias>(request);
        filtros.CatOrganismoId = _sessionService.SesionActual.CatOrganismoId;
        var sentencias = await _repository.ObtenerSentencias(filtros);

        return new TableroSentenciasDto
        {
            Datos = sentencias
        };
    }
}
