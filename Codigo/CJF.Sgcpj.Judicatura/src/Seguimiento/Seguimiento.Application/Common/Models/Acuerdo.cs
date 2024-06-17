using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;
/// <summary>
///                        DECLARACION DE ENTIDAD  DE ACUERDO UNICAMENTE PARA SEGUIMIENTO
/// </summary>
public class Acuerdo : Documentos
{

    /// <summary>
    /// Obtiene o establece el identificador del organismo.
    /// </summary>

    public int OrganismoId { get; set; }

    /// <summary>
    /// Obtiene o establece la fecha de autorización.
    /// </summary>

    public DateTime? FechaAuto { get; set; }


    /// <summary>
    /// Obtiene o establece el identificador del organismo.
    /// </summary>

    public int SintesisOrden { get; set; }
    /// <summary>
    /// Obtiene o establece la fecha de publicación.
    /// </summary>

    public DateTime FechaPublicacion { get; set; }

    /// <summary>
    /// Obtiene o establece El AsuntoNeun.
    /// </summary>
    public Int64 Neun { get; set; }

    /// <summary>
    /// Obtiene el igorganismo de la publicacion
    /// </summary>
    public Int32 organismoId { get; set; }

    /// <summary>
    /// Obtiene el orden
    /// </summary>
    public Int32 orden { get; set; }

    /// <summary>
    /// Obtiene el documentoId del procceso
    /// </summary>
    public Int32 documentoId { get; set; }


}
