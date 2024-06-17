using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.Common.Models;
/// <summary>
/// Representa el detalle del Expediente electrónico.
/// </summary>
public class DetalleExpedienteElectronico
{
    /// <summary>
    /// Obtiene o establece.
    /// </summary>
    public String No_Exp { get; set; }
    /// <summary>
    /// Obtiene o establece.
    /// </summary>
    public int CatTipoAsuntoId { get; set; }
    /// <summary>
    /// Obtiene o establece.
    /// </summary>
    public Nullable<DateTime> FechaPresentacion { get; set; }
    /// <summary>
    /// Obtiene o establece.
    /// </summary>
    public String OrigenPromo { get; set; }
    /// <summary>
    /// Obtiene o establece.
    /// </summary>
    public int Folio { get; set; }
    /// <summary>
    /// Obtiene o establece.
    /// </summary>
    public String ArchivoPromocion { get; set; }
    /// <summary>
    /// Obtiene o establece.
    /// </summary>
    public String TipoContenidoDescripcion { get; set; }
    /// <summary>
    /// Obtiene o establece.
    /// </summary>
    public String CaracterPromovente { get; set; }
    /// <summary>
    /// Obtiene o establece.
    /// </summary>
    public String Promovente { get; set; }
    /// <summary>
    /// Obtiene o establece.
    /// </summary>
    public Nullable<DateTime> FechaAcuerdo { get; set; }
    /// <summary>
    /// Obtiene o establece.
    /// </summary>
    public String PlantillaDocumento { get; set; }
    /// <summary>
    /// Obtiene o establece.
    /// </summary>
    public String NombreDocumento { get; set; }
    /// <summary>
    /// Obtiene o establece.
    /// </summary>
    public String SecretarioDescripcion { get; set; }
    /// <summary>
    /// Obtiene o establece.
    /// </summary>
    public String Mesa { get; set; }
    /// <summary>
    /// Obtiene o establece.
    /// </summary>
    public Int32 EstadoAutorizacion { get; set; }
    /// <summary>
    /// Obtiene o establece.
    /// </summary>
    public Nullable<DateTime> PorLista { get; set; }
    /// <summary>
    /// Obtiene o establece.
    /// </summary>
    public int PorOficio { get; set; }
    /// <summary>
    /// Obtiene o establece.
    /// </summary>
    public int Personal { get; set; }
    /// <summary>
    /// Obtiene o establece.
    /// </summary>
    public int Electronica { get; set; }
    /// <summary>
    /// Obtiene o establece.
    /// </summary>
    public Int64 AsuntoNeunId { get; set; }
    /// <summary>
    /// Obtiene o establece.
    /// </summary>
    public int AsuntoDocumentoId { get; set; }
    /// <summary>
    /// Obtiene o establece.
    /// </summary>
    public String Observaciones { get; set; }
    /// <summary>
    /// Obtiene o establece.
    /// </summary>
    public String UserNameSecretario { get; set; }
    /// <summary>
    /// Obtiene o establece.
    /// </summary>
    public String FechaPresentacion_F { get; set; }
    /// <summary>
    /// Obtiene o establece.
    /// </summary>
    public String FechaAcuerdo_F { get; set; }
    /// <summary>
    /// Obtiene o establece.
    /// </summary>
    public String PorLista_F { get; set; }
    /// <summary>
    /// Obtiene o establece.
    /// </summary>
    public String OrigenCorto { get; set; }
    /// <summary>
    /// Representa el color de la caratula del cuaderno al que pertence
    /// </summary>
    public String Color { get; set; }
    /// <summary>
    /// Representa el identificador del cuaderno
    /// </summary>
    public Int32 CuadernoId { get; set; }
    /// <summary>
    /// Representa el nombre del cuaderno
    /// </summary>
    public String Cuaderno { get; set; }
    /// <summary>
    /// Representta el nombre del documento creado en Word-Addin
    /// </summary>
    public String AsuntoDocumentoNombre { get; set; }
    /// <summary>
    /// Representa el nombre corto del cuaderno
    /// </summary>
    public string NombreCorto { get; set; }
    /// <summary>
    /// Obtiene o establece la ruta del archivo de la promoción
    /// </summary>
    public Int32 RutaArchivoNAS { get; set; }
    /// <summary>
    /// Identificador del origen de la promoción
    /// </summary>
    public Int32 OrigenPromocion { get; set; }
    public Int32 NumeroOrden { get; set; }
    public Int32 AnioPromocion { get; set; }
    public Int32 NumeroRegistro { get; set; }
}
