using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Usuarios.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Usuarios.Application.Usuarios.AutoridadJudicial.Consulta;

public class AutoridadJudicialDto : IMapFrom<BuscaAutoridadJudicial>
{
    public string NombreCompleto { get; set; }
    public long EmpleadoId { get; set; }
    public string CargoDescripcion { get; set; }
    public int CatOrganismoId { get; set; }
    public string NombreOficial { get; set; }
}