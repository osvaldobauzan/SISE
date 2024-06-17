using MediatR;

namespace CJF.Sgcpj.Judicatura.Sentencias.Application.Sentencias.Comandos.SubirSentenciaComando;

public record SubirSentenciaComando : IRequest<SentenciaDto>
{
    public string? Sentencia { get; set; }

    public string? SentenciaVP { get; set; }

    public byte[]? ArchivoBytes { get; set; }

    public string? NomArchivoReal { get; set; }
}