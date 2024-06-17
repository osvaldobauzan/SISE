using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Usuarios.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Usuarios.Application.Usuarios.Secretarios;

public class CatalogoSecretarioDto : IMapFrom<UsuarioSecretario>
{
    public long EmpleadoId { get; set; }
    public string Completo { get; set; }
    public string Area { get; set; }
}
