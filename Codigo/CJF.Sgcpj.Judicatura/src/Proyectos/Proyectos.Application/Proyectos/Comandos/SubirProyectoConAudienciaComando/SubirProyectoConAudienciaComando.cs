using MediatR;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Comandos.SubirProyectoConAudienciaComando;

public record SubirProyectoConAudienciaComando : IRequest<ProyectoConAudienciaDto>
{
    public int CatOrganismoId { get; set; }

    public long AsuntoNeunId { get; set; }

    public long TitularId { get; set; }

    public long SecretarioId { get; set; }

    public int TipoSentenciaId { get; set; }

    public string? Sintesis { get; set; }

    public byte[] Archivo { get; set; }

    public string NombreArchivo { get; set; }

    public List<MotivosParteDto> Motivos { get; set; }
}
