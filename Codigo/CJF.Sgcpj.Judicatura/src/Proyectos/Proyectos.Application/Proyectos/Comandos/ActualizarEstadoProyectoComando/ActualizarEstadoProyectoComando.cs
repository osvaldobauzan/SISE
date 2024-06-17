using MediatR;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Comandos.ActualizarEstadoProyectoComando;

public class ActualizarEstadoProyectoComando : IRequest<EstadoProyectoActualizadoDto>
{
    public int CatOrganismoId { get; set; }

    public long AsuntoNeunId { get; set; }

    public long ProyectoId { get; set; }

    public int EstadoId { get; set; }

    public string? Correcciones { get; set; }

    public byte[]? ArchivoCorreciones { get; set; }

    public string? NombreArchivoCorrecciones { get; set; }
}
