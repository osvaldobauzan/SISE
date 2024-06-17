using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.ClasificacionAutoridadGenerica.Consulta;
/// <summary>
/// Esta clase representa el Header que contiene las opciones del catálogo de Clasificación de Autoridad Genérica
/// </summary>
public class CatalogoClasificacionAutoridadGenericaDTO
{
   /// <summary>
   /// Identificador de la opción de la Clasificación de la Utoridad Genérica
   /// </summary>
    public int ClasificaAutoridadGenericaId { get; set; }
    /// <summary>
    /// Descripción de la opción de la Clasificación de la Utoridad Genérica
    /// </summary>
    public string Descripcion { get; set; }
}
