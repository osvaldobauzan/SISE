using MediatR;

namespace CJF.Sgcpj.Judicatura.Usuarios.Application.Usuarios.Secretarios.Consulta.ObtenerSecretario;
public class ObtieneSecretario : IRequest<ObtieneSecretarioDto>
{
    public long AsuntoNeunId { get; set; }
    public int CatOrganismoId { get; set; } 

}
