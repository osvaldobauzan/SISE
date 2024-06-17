using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Proyectos.Domain.Entities;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Comandos.ActualizarEstadoProyectoComando;

public class EstadoProyectoActualizadoDto : IMapFrom<ProyectoActualizado>
{
    public long AsuntoNeunId { get; set; }

    public string NumeroExpediente { get; set; }

    public int TipoAsuntoId { get; set; }

    public string TipoAsunto { get; set; }

    public int CuadernoId { get; set; }

    public string Cuaderno { get; set; }

    public int CatOrganismoId { get; set; }

    public string CatOrganismo { get; set; }

    public long ProyectoId { get; set; }

    public int EstadoId { get; set; }

    public string NombreArchivo { get; set; }

    public string Anio { get; set; }

    public long ProyectoArchivoId { get; set; }

    public string Titular { get; set; }

    public string Secretario { get; set; }

    public long TitularId { get; set; }

    public long SecretarioId { get; set; }

    public int Version { get; set; }

    public DateTime FechaAlta { get; set; }
}
