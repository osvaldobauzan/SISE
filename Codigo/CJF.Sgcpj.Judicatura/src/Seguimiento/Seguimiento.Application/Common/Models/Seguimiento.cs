using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;

public class Seguimiento 
{

/// <summary> 
/// Representa el identificador del seguimiento a un documento.
/// </summary>   
public int SeguimientoId { get; set; }

/// <summary>
/// Representa el identficador del organismo donde se generó el registro del seguimiento del documento.
/// </summary>
public int CatOrganismoId { get; set; }

/// <summary>
/// Representa el identficador del área donde se generó el registro del seguimiento del documento.
/// </summary>
public int AreaId { get; set; }

/// <summary>
/// Representa el identficador del área donde se generó el registro del seguimiento del documento.
/// </summary>
public string Area { get; set; }

/// <summary>
/// Representa el identficador del empleado que registró el seguimiento del documento.
/// </summary>
public long EmpleadoId { get; set; }

/// <summary>
/// Representa el identficador del empleado que registró el seguimiento del documento.
/// </summary>
public string UserName { get; set; }

/// <summary>
/// Representa el número de expediente único nacional al que pertenece el documento del que se está registrando el seguimiento.
public long AsuntoNeun { get; set; }

/// <summary>
/// Representa el número de expediente único nacional al que pertenece el documento del que se está registrando el seguimiento.
/// </summary>
public string Expediente { get; set; }

/// <summary>
/// Representa la fecha y hora del momento en que se registró el movimiento.
/// </summary>
public DateTime FechaHora { get; set; }

/// <summary>
/// Representa la descripción del movimiento en un formato amigable al usuario.
/// </summary>
public string Descripcion { get; set; }

/// <summary>
/// Representa el estado del movimiento que se registró.
/// </summary>
public short StatusReg { get; set; }

/// <summary>
/// Representa el tipo de documento del que se está registrando el movimiento.
/// </summary>
/// public CatTipoDocumento TipoId { get; set; }
///// <summary>
///// Representa el tipo de documento del que se está registrando el movimiento.
///// </summary>public String? Tipo { get; set; }

    private CatTipoDocumento tipoId;

    public CatTipoDocumento TipoId
    {
        get { return tipoId; }
        set { tipoId = value; }
    }

    public string Tipo
    {
        get
        {
            string result = "No definido";

            switch (this.TipoId)
            {
                case CatTipoDocumento.Expediente:
                    result = "Expediente";
                    break;
                case CatTipoDocumento.Promocion:
                    result = "Promocion";
                    break;
                case CatTipoDocumento.Acuerdo:
                    result = "Acuerdo";
                    break;
                case CatTipoDocumento.Oficio:
                    result = "Oficio";
                    break;
                default:
                    result = "No definido";
                    break;
            }
            return result;
        }
        set
        {
            if (Enum.TryParse(value, out CatTipoDocumento tipo))
            {
                TipoId = tipo;
            }
            
        }
    }
    /// <summary>
    /// Representa el número de documento con que se identifica al documento del que se está registrando el movimiento.
    /// </summary>
    public string DocumentoId { get; set; }

/// <summary>
/// Obtiene o establece.
/// </summary>
public String FechaHora_F { get; set; }

/// <summary>
/// Obtiene o establece.
/// </summary>
public Int64 NumeroAlias { get; set; }

/// <summary>
/// Representa el nombre del Tipo de Asunto del seguimiento
/// </summary>
public string TipoAsunto { get; set; }

/// <summary>
/// Representa el nombre Empleado
/// </summary>
public string EmpleadoNombre { get; set; }

/// <summary>
/// Representa el nombre Empleado
/// </summary>
public String Fecha { get; set; }

/// <summary>
/// Representa el nombre Empleado
/// </summary>
public String Hora { get; set; }

/// <summary>
/// Representa el nombre Empleado
/// </summary>
public String Fecha_F { get; set; }

/// <summary>
/// Representa el nombre Empleado
/// </summary>
public String Hora_F { get; set; }

/// <summary>
/// Puesto de Empleado
/// </summary>
public String PuestoDescripcion { get; set; }

/// <summary>
/// tipo documento de Empleado
/// </summary>
public String TipoDocumento { get; set; }
/// <summary>
/// Nombre de la parte
public String NombreParte { get; set; }

/// <summary>
/// Caracter del expediente
/// </summary>
public String Caracter { get; set; }

/// <summary>
/// Caracter del expediente
/// </summary>
public String Cuaderno { get; set; }

/// <summary>
/// Filas afectadas al insertar
/// </summary>
public int FilasInsertadas { get; set; }

    /// <summary>
    /// Almacena el valor del QR leido por la pistola
    /// </summary>
public string QrString { get; set; }

    /// <summary>
    /// Almacena el valor de la fecha inicial para la consulta
    /// </summary>
    public DateTime FechaIni { get; set; }

    /// <summary>
    /// Almacena el valor de la fecha Final para la consulta
    /// </summary>
    public DateTime FechaFin { get; set; }

    public string TipoProcedimiento { get; set; }
}




