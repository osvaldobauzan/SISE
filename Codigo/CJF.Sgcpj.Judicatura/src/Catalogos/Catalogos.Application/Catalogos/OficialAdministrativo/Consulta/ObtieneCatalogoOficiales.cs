using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.OficialAdministrativo.Consulta;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.OficialAdministrativo.Consulta;

/// <summary>
/// Solicitud para obtener el catálogo de oficiales.
/// </summary>
public record ObtieneCatalogoOficiales : IRequest<List<CatalogoOficialDto>>
{
    /// <summary>
    /// Identificador del organismo.
    /// </summary>
    public int CatOrganismoId { get; set; }

    /// <summary>
    /// Identificador del cargo.
    /// </summary>
    public int CargoId { get; set; } = 17; // Valor por defecto
}
