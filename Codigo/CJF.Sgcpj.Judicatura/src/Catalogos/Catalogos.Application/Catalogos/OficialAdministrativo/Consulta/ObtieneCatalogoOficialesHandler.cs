using AutoMapper;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.OficialAdministrativo.Consulta;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.OficialAdministrativo.Consulta;

/// <summary>
/// Handler para obtener el catálogo de oficiales.
/// </summary>
public class ObtieneCatalogoOficialesHandler : IRequestHandler<ObtieneCatalogoOficiales, List<CatalogoOficialDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ISesionService _sesionService;

    /// <summary>
    /// Constructor de la clase.
    /// </summary>
    /// <param name="mapper">Instancia de IMapper.</param>
    /// <param name="applicationDbContext">Contexto de la base de datos.</param>
    /// <param name="sesionService">Servicio de sesión.</param>
    public ObtieneCatalogoOficialesHandler(IMapper mapper, IApplicationDbContext applicationDbContext, ISesionService sesionService)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
        _sesionService = sesionService;
    }

    /// <summary>
    /// Maneja la solicitud para obtener el catálogo de oficiales.
    /// </summary>
    /// <param name="request">Solicitud para obtener el catálogo de oficiales.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Lista de oficiales.</returns>
    public async Task<List<CatalogoOficialDto>> Handle(ObtieneCatalogoOficiales request, CancellationToken cancellationToken)
    {
        request.CatOrganismoId = _sesionService.SesionActual.CatOrganismoId;

        List<SqlParameter> parametrosOficiales = new()
        {
            new SqlParameter("@pi_CatOrganismoId", request.CatOrganismoId),
            new SqlParameter("@pi_CargoId", request.CargoId),
        };

        var itemsCatalogo = await _applicationDbContext.ExecuteStoredProc<CatalogoOficial>("[SISE3].[pcObtieneCatalogoOficiales]", parametrosOficiales.ToArray());
        var listaCatalogoOficiales = _mapper.Map<List<CatalogoOficialDto>>(itemsCatalogo);

        return await Task.FromResult(listaCatalogoOficiales);
    }
}
