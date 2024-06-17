using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerProyectos;

public class ObtenerProyectosConFiltroHandler : IRequestHandler<ObtenerProyectosConFiltro, ListaPaginada<ProyectoDto, MetaDataEstadoProyectoDto>>
{
    private readonly IProyectosRepository _repository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sessionService;
    private readonly ILogger<ObtenerProyectosConFiltroHandler> _logger;

    public ObtenerProyectosConFiltroHandler(IProyectosRepository repository, IMapper mapper, ISesionService sesionService, ILogger<ObtenerProyectosConFiltroHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _sessionService = sesionService;
        _logger = logger;
    }

    public async Task<ListaPaginada<ProyectoDto, MetaDataEstadoProyectoDto>> Handle(ObtenerProyectosConFiltro request, CancellationToken cancellationToken)
    {
        try
        {
            ListaPaginada<ProyectoDto, MetaDataEstadoProyectoDto> listaPaginada = new();
            var consultaPaginada = _mapper.Map<ConsultaPaginadaProyectos>(request);
            consultaPaginada.OrganismoID = _sessionService.SesionActual.CatOrganismoId;
            consultaPaginada.UsuarioId = _sessionService.SesionActual.EmpleadoId;
            var (datos, metadatos) = await _repository.ObtenerProyectosConFiltro(consultaPaginada);

            listaPaginada.Datos = _mapper.Map<List<ProyectoDto>>(datos);
            listaPaginada.MetaDatos = _mapper.Map<MetaDataEstadoProyectoDto>(metadatos);
            listaPaginada.TotalRegistros = request.Estado switch
            {
                0 => metadatos.TotalProyectos,
                1 => metadatos.TotalSinProyecto,
                2 => metadatos.TotalParaRevision,
                3 => metadatos.TotalConAjustes,
                4 => metadatos.TotalNoAprobado,
                5 => metadatos.TotalAprobado,
            };

            listaPaginada.Pagina = consultaPaginada.Pagina;
            listaPaginada.TotalPaginas = consultaPaginada.RegistrosPorPagina != 0 ? listaPaginada.TotalRegistros / consultaPaginada.RegistrosPorPagina + (listaPaginada.TotalRegistros % consultaPaginada.RegistrosPorPagina > 0 ? 1 : 0) : 0;
            return listaPaginada;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
