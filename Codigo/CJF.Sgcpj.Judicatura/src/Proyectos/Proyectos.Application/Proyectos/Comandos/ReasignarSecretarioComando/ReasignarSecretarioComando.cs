using MediatR;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Comandos.ReasignarSecretarioComando;

public class ReasignarSecretarioComando: IRequest<ReasignacionSecretarioDTO>
{
    public int CatOrganismoId { get; set; }

    public long EmpleadoId { get; set; }

    public long SecretarioNuevoId { get; set; }

    public string ProyectosId { get; set; }
}
