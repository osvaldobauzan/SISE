using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CJF.Sgcpj.Judicatura.LibretaOficios.Application.Common.Models;
public record InsertarOficioFiltro : IRequest<bool>
{
    /// <summary>
    /// Obtiene o establece el identificador del NEUN.
    /// </summary>
    public long AsuntoNeunId { get; set; }
    /// <summary>
    /// Establece el Identificador del Asunto
    /// </summary>
    public int AsuntoId { get; set; }
    /// <summary>
    /// Obtiene el id del organo jurisdiccional
    /// </summary>
    public int AsuntoDocumentoId { get; set; }
    /// <summary>
    /// Establece el tipo de anexo a insertar
    /// </summary>
    public short AnexoTipoId { get; set; }
    /// <summary>
    /// Establece el nombre del documento a insertat
    /// </summary>
    public string NombreDocumento { get; set; }
    /// <summary>
    /// Establece la ruta NAS donde se guarda el archivo físico
    /// </summary>
    public string RutaAnexo { get; set; }
    /// <summary>
    /// Establece el nombre del archivo para la NAS
    /// </summary>
    public string NombreArchivo { get; set; }
    /// <summary>
    /// Establece la extensión del archivo para la NAS
    /// </summary>
    public string ExtensionDocumento { get; set; }
}
