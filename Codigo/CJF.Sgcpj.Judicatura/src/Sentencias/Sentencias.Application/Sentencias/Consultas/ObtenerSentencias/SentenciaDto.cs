using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Sentencias.Domain.Entities;

namespace CJF.Sgcpj.Judicatura.Sentencias.Application.Sentencias.Consultas.ObtenerSentencias;

public class SentenciaDto : IMapFrom<SentenciaItemTablero>
{
    public ExpedienteDto Expediente { get; set; }

    public long SentenciaId { get; set; }

    public DateTime FechaAprobacionProyecto { get; set; }

    public string? TemaDelAsunto { get; set; }

    public long? ArchivoSentenciaId { get; set; }

    public string? NombreArchivoSentencia { get; set; }

    public long? ArchivoAcuseId { get; set; }

    public string? NombreArchivoAcuse { get; set; }

    public string? UsuarioCapturo { get; set; }

    public DateTime? FechaCapturo { get; set; }

    public string? UsuarioPreautorizo { get; set; }

    public DateTime? FechaPreautorizo { get; set; }

    public string? UsuarioAutorizo { get; set; }

    public DateTime? FechaAutorizo { get; set; }

    public DateTime? FechaPublicacion { get; set; }

    public DateTime? FechaAuto { get; set; }

    public int? EstadoSentenciaId { get; set; }

    public string? EstadoSentencia { get; set; }
    public int TipoOrganismoId { get; set; }
    public int AsuntoDocumentoId { get; set; }
    public Guid GuidDocumento { get; set; }
}
