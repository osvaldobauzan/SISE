using MediatR;

namespace CJF.Sgcpj.Judicatura.Sentencias.Application.Sentencias.Comandos.PreautorizarSentenciaComando;
public class PreautorizarSentenciaComando : IRequest<string>
{
    public List<PreautorizarSentenciaDto>? Sentencias { get; set; }
}
