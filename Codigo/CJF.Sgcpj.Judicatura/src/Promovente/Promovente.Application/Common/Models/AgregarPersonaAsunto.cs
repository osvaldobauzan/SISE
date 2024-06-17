using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Promovente.Application.Common.Models;
public class AgregarPersonaAsunto
{
    public long AsuntoNeunId { get; set; }
    public long UsuarioCaptura { get; set; }
    public string? Nombre { get; set; }
    public string? APaterno { get; set; }
    public string? AMaterno { get; set; }
    public long? CatTipoPersonaId { get; set; }
    public long? CatCaracterPersonaAsuntoId { get; set; }
    public string? DenominacionDeAutoridad { get; set; }
    public long? PersonaId { get; set; }
    public int NumeroOrden { get; set; }
   
}
