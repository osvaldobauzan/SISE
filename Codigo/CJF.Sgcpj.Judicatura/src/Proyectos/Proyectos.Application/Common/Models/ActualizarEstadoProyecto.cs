namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Models;

public class ActualizarEstadoProyecto
{
    public long ProyectoId { get; set; }

    public string? Correcciones { get; set; }

    public string? ArchivoCorrecciones { get; set; }

    public int EstadoId { get; set; }

    public string IpUsuario { get; set; }

    public long UsuarioId { get; set; }
}
