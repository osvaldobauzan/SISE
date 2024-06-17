namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;

/// <summary>
/// Representa un oficial en el catálogo de oficiales.
/// </summary>
public class CatalogoOficial
{
    /// <summary>
    /// Identificador del empleado.
    /// </summary>
    public long EmpleadoId { get; set; }

    /// <summary>
    /// Identificador del cargo.
    /// </summary>
    public int CargoId { get; set; }

    /// <summary>
    /// Nombre completo del oficial.
    /// </summary>
    public string nombreOficial { get; set; }

    /// <summary>
    /// Permisos del oficial.
    /// </summary>
    public bool Permisos { get; set; }

    /// <summary>
    /// Nombre del usuario para agrupación de pestañas
    /// </summary>
    public string UserName { get; set; }
}
