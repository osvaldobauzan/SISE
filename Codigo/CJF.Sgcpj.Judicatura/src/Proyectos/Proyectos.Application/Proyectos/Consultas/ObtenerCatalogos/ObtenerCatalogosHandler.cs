using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerCatalogos;

public class ObtenerCatalogosHandler : IRequestHandler<ObtenerCatalogos, ObtenerCatalogosDto>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ILogger<ObtenerCatalogosHandler> _logger;

    public ObtenerCatalogosHandler(IApplicationDbContext applicationDbContext, ILogger<ObtenerCatalogosHandler> logger)
    {
        _applicationDbContext = applicationDbContext;
        _logger = logger;
    }

    public async Task<ObtenerCatalogosDto> Handle(ObtenerCatalogos request, CancellationToken cancellationToken)
    {
        try
        {
            List<SqlParameter> parametrosEmpleado = new()
        {
            new SqlParameter("@ps_TipoAsuntoId", request.CatTipoAsuntoId)
        };

            var (tipoCatalogo, catalogo) = await _applicationDbContext.ExecuteStoredProc<ObtenerCatalogosDto, CatalogoDTO>("[SISE3].[pcTableroProyectoSentidoPartesCatalogos]", parametrosEmpleado.ToArray());

            return await Task.FromResult(new ObtenerCatalogosDto
            {
                Datos = catalogo,
                TipoCatalogo = tipoCatalogo.Select(x => x.TipoCatalogo).FirstOrDefault()
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
