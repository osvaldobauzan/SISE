using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Oficialia.Application.Oficialia.Consultas.ObtenerPromociones;
public class OficialiaPartesDTO
{
    /// <summary>
    /// Obtiene o establece el número de expediente.
    /// </summary>
    public String Expediente { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public String Occ { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public String TipoAsuntoDescripcion { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public int CatTipoAsuntoId { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public Int64 AsuntoNeunId { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public int NumeroOrden { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public int CatOrganismoId { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public int YearPromocion { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public int NumeroRegistro { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public int SintesisOrden { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public int TipoPromocion { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public String TipoPromocionDescripcion { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public DateTime FechaPresentacion { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public String HoraPresentacion { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public int TipoCuaderno { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public int ClasePromocion { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public String TipoCuadernoDescripcion { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public Int32 ParteId { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public int ClasePromovente { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public String ParteDescripcion { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public int TipoContenidoId { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public String TipoContenidoDescripcion { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public String Contenido { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public int Copias { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public int Anexos { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public int EstadoPromocion { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public String NombreArchivo { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public int EstatusArchivo { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public int AsuntoDocumentoId { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public String Mesa { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public Int64 SecretarioId { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public String SecretarioDescripcion { get; set; }
    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public String IPUsuario { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public int CatTipoOrganismoId { get; set; }

    /// <summary>
    /// Obtiene o establece
    /// </summary>
    public int OrigenPromocion { get; set; }

    /// <summary>
    /// Obtiene o establece código Hexadecimal del color.
    /// </summary>
    public String Color { get; set; }

    /// <summary>
    /// Obtiene o establece la descripción corta.
    /// </summary>
    public String NombreCorto { get; set; }

    /// <summary>
    /// Obtiene o establece el identificador del empleado.
    /// </summary>
    public Int64 RegistroEmpleadoId { get; set; }

    /// <summary>
    /// Obtiene o establece.
    /// </summary>
    public Int64 NumeroAlias { get; set; }

    /// <summary>
    /// Obtiene o establece.
    /// </summary>
    public String UserNameSecretario { get; set; }

    /// <summary>
    /// Obtiene o establece.
    /// </summary>
    public String FechaPresentacion_F { get; set; }

    /// <summary>
    /// Obtiene o establece las observaciones a una promocion
    /// </summary>
    public string Observaciones { get; set; }

    /// <summary>
    /// Obtiene o establece la ruta del archivo de la promoción
    /// </summary>
    public Int32 RutaArchivoNAS { get; set; }
}

