using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;
/// <summary>
///                         DECLARACION ENTIDADES PARA DOCUMENTOS UNICAMENTE PARA SEGUIMIENTO
/// </summary>
public class Documentos : BaseCatalog
{
    /// <summary>
    /// Representa el identificador del documento.
    /// </summary>
    public string Id { get; set; }

    // <summary>
    /// Representa el identificador del catorganismo del documento .
    /// </summary>
    public int CatOrganismoId { get; set; }

    /// <summary>
    /// Representa el número del documento según el tipo que sea:
    /// Expediente: Número de expediente (alias)
    /// Promocion:  Número de Registro
    /// Acuerdo:    Número de Acuerdo
    /// Oficio:     Número de Fólio
    /// </summary>
    public string Numero { get; set; }

    /// <summary>
    /// Representa el Número de Expediente Único Nacional al que está asociado el documento.
    /// </summary>
    public long Neun { get; set; }

    /// <summary>
    /// Representa la descripción de la acción que se refiere a este registro.
    /// </summary>
    public string? Mensaje { get; set; }

    /// <summary>
    /// Representa el tipo de documento y está representado por el enumerador TipoDocumento.
    /// </summary>
    /// <remarks>
    ///     Expediente = 1,
    ///     Promocion = 2,
    ///     Oficio = 3,
    ///     Acuerdo = 4
    /// </remarks>
    public CatTipoDocumento? Tipo { get; set; }

}