using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.TipoPersonaJuridica.Consulta;
/// <summary>
/// Esta clase representa el Header que contiene las opciones del catálogo de Tipo de Persona Jurídica.
/// </summary>
public class CatalogoTipoPersonaJuridicaDTO
{
    /// <summary>
    /// Identificador del Tipo de Persona Juridica 
    /// </summary>
    public int CatTipoPerJuridicaId { get; set; }
    /// <summary>
    /// Descripción del Tipo de Persona Jurídica
    /// </summary>
    public string Descripcion { get; set; }
}
