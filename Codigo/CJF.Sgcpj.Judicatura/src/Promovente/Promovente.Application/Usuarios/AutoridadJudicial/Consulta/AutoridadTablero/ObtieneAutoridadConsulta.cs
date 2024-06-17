using MediatR;

namespace CJF.Sgcpj.Judicatura.Promovente.Application.Usuarios.AutoridadJudicial.Consulta.AutoridadTablero;
public record ObtieneAutoridadConsulta : IRequest<List<ObtieneAutoridadDto>>
{
    public long AsuntoNeunId { get; set; }
    public string NoExp { get; set; }
    public string Texto { get; set; }
    public int Modulo { get; set; }
    public int TipoParte { get; set; }

}
