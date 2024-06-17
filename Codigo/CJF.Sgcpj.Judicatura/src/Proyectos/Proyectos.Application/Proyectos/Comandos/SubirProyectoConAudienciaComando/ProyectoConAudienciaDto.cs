using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Proyectos.Domain.Entities;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Comandos.SubirProyectoConAudienciaComando;

public class ProyectoConAudienciaDto : IMapFrom<ProyectoConAudiencia>
{
    public long ProyectoId { get; set; }

    public int CatOrganismoId { get; set; }

    public long AsuntoNeunId { get; set; }

    public DateTime FechaCreacion { get; set; }

    public long TitularId { get; set; }

    public long SecretarioId { get; set; }

    public int TipoSentenciaId { get; set; }

    public int SentidoId { get; set; }

    public long ProyectoArchivoId { get; set; }

    public string NombreArchivo { get; set; }

    public string Sintesis { get; set; }

    public int Version { get; set; }

    public int EstadoId { get; set; }

    public string Anio { get; set; }

    public string Titular { get; set; }

    public string Secretario { get; set; }

    public string NumeroExpediente { get; set; }

    public string TipoAsunto { get; set; }
}
