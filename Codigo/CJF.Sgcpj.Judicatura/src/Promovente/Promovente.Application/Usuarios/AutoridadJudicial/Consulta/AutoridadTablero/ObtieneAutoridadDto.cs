using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Promovente.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Promovente.Application.Usuarios.AutoridadJudicial.Consulta.AutoridadTablero;
public class ObtieneAutoridadDto : IMapFrom<Autoridad>
{
    public string Nombres { get; set; }
    public long AutoridadJudicialId { get; set; }
    public string AutoridadJudicialDescripcion { get; set; }
}
