
namespace CJF.Sgcpj.Judicatura.LibretaOficios.Application.Common.Models;
public class LibretaOficio
{
    /// <summary>
    /// Obtiene o establece el contador de los registros.
    /// </summary>
    public long Contador { get; set; }

    /// <summary>
    /// Obtiene o establece identificador del anexo.
    /// </summary>
    public int AnexoId { get; set; }

    /// <summary>
    /// Obtiene o establece el identificador del NEUN.
    /// </summary>
    public long AsuntoNeunId { get; set; }

    /// <summary>
    /// Obtiene o establece el número de folio.
    /// </summary>
    public int Folio { get; set; }

    /// <summary>
    /// Obtiene o establece el número de expediente.
    /// </summary>
    public string NoExpediente { get; set; }
    /// <summary>
    /// Obtiene o establece el identificador del tipo de asunto.
    /// </summary>
    public int TipoAsuntoId { get; set; }

    /// <summary>
    /// Obtiene o establece el nombre del tipo de asunto.
    /// </summary>
    public string TipoAsuntoDescripcion { get; set; }

    /// <summary>
    /// Obtiene o establece el identificador del anexo.
    /// </summary>
    public int AnexoParteId { get; set; }

    /// <summary>
    /// Obtiene o establece el nombre de la parte.
    /// </summary>
    public string AnexoParteDescripcion { get; set; }

    /// <summary>
    /// Obtiene o establece la fecha de alta.
    /// </summary>
    public DateTime FechaAlta { get; set; }

    /// <summary>
    /// Obtiene o establece el estatus del anexo.
    /// </summary>
    public short AnexoStatus { get; set; }

    /// <summary>
    /// Obtiene o establece el identificador del asunto.
    /// </summary>
    public int AsuntoDocumentoId { get; set; }

    /// <summary>
    /// Obtiene o establece el nombre del documento.
    /// </summary>
    public string NombreDocumento { get; set; }

    /// <summary>
    /// Obtiene o establece el nombre del archivo.
    /// </summary>
    public string Archivo { get; set; }

    /// <summary>
    /// Obtiene o establece el número alias.
    /// </summary>
    public long NumeroAlias { get; set; }

    /// <summary>
    /// Obtiene o establece la fecha alta formateada.
    /// </summary>
    public string FechaAlta_F { get; set; }

    /// <summary>
    /// Obtiene o establece el año.
    /// </summary>
    public int Anio { get; set; }

    /// <summary>
    /// Obtiene el nombre del empleado que cancela el oficio
    /// </summary>
    public string EmpleadoElimina { get; set; }

    /// <summary>
    /// Obtiene el UserName del empleado que cancela el oficio
    /// </summary>
    public string UserNameElimina { get; set; }

    /// <summary>
    /// Obtiene la fecha en que se cancela el oficio
    /// </summary>
    public string FechaElimina { get; set; }
    /// <summary>
    /// Obtiene el id del organo jurisdiccional
    /// </summary>
    public int CatOrganismoId { get; set; }
    /// <summary>
    /// Obtiene la cantidad total de registros por el criterio de busqueda
    /// </summary>
    public long TotalRegistros { get; set; }
}