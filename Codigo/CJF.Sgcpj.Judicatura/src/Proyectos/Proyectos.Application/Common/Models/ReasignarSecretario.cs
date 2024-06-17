namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Models;

public class ReasignarSecretario
{
    public int CatOrganismoId { get; set; }

    public long EmpleadoId { get; set; }

    public string ProyectosId { get; set; }

    public long SecretarioNuevoId { get; set; }
}
