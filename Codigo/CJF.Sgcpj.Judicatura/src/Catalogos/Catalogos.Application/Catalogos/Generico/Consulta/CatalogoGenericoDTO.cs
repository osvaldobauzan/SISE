using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Generico.Consulta;
/// <summary>
/// Esta clase representa el Header que contiene las opciones de un catálogo.
/// </summary>
public class CatalogoGenericoDTO
{
    /// <summary>
    /// Identificador de la opción dentro de un catálogo
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Descripción de la opción dentro de un catálogo 
    /// </summary>
    public string Descripcion { get; set; }
}
