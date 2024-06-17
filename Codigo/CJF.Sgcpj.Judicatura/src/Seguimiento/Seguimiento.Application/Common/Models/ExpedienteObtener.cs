using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;
/// <summary>
///                         DECLARACION ENTIDADES PARA EXPEDIENTE UNICAMENTE PARA SEGUIMIENTO
/// </summary>
public class ExpedienteObtener : Documentos
{
    /// <summary>
    /// Obtiene o establece el valor del AsuntoNeun.
    /// </summary>
    public Int64 AsuntoNeunId { get; set; }

    /// <summary>
    /// Almacena el valor del organismoId de la consulta.
    /// </summary> 
    public int CatOrganismoId { get; set; }

    /// <summary>
    /// Obtiene o establece el valor del AsuntoAlias del documento.
    /// </summary>
    
    public string AsuntoAlias { get; set; }

    /// <summary>
    /// Obtiene o establece el valor del TipoAsunto.
    /// </summary>
    public int TipoAsuntoId { get; set; }

    /// <summary>
    /// Establece el valor de CatMateriaId relacionado a la consulta.
    /// </summary>
    public short CatMateriaId { get; set; }

    /// <summary>
    /// Obtiene el valor del NumeroOcc de la consulta
    /// </summary>
    public string NumeroOCC { get; set; }

    /// <summary>
    /// Obtiene o establece el tipo de asunto del documento.
    /// </summary>
    public int CatTipoAsuntoId { get; set; }


    /// <summary>
    /// Obtiene o establece el tipo de asunto del documento.
    /// </summary>
    public String CatTipoAsunto { get; set; }

    /// <summary>
    /// Obtiene o establece el identificador del secretario.
    /// </summary>
    public int SecretarioId { get; set; }

    /// <summary>
    /// Obtiene o establece el nombre del secretario.
    /// </summary>
    public String Secretario { get; set; }

    /// <summary>
    /// Representa la mesa a la cual pertenece el expediente
    /// </summary>
    public String Mesa { get; set; }

    /// <summary>
    /// Representa el identificador del empleado que crea/actualiza el expediente
    /// </summary>
    public Int32 EmpleadoExpedienteId { get; set; }

    /// <summary>
    /// Representa el nombre del empleado que crea/actualiza el expediente
    /// </summary>
    public String EmpleadoExpediente { get; set; }

    /// <summary>
    /// Representa el identificador del Tipo de Procedimiento para un asunto
    /// </summary>
    public Int32 TipoProcedimientoId { get; set; }

    /// <summary>
    /// Representa el nombre del Tipo de Procedimiento para un asunto
    /// </summary>
    public String TipoProcedimiento { get; set; }
}
