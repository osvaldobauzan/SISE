using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Usuarios.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Usuarios.Application.Usuarios.Secretarios.Consulta.ObtenerSecretario;
public class ObtieneSecretarioDto : IMapFrom<UsuarioSecretario>
{
    public long? Secretario { get; set; }
}
