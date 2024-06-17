using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Promovente.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Promovente.Application.Promoventes.Comandos.AgregarPersonas;
public class AgregarPersonasAsuntosDto : IMapFrom<AgregarPersonaAsunto>
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
