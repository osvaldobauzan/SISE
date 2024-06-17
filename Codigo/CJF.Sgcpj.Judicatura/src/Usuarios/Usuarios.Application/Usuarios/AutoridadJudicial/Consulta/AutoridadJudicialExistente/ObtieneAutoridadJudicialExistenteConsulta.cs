using MediatR;

namespace CJF.Sgcpj.Judicatura.Usuarios.Application.Usuarios.AutoridadJudicial.Consulta.AutoridadJudicialExistente;
public class ObtieneAutoridadJudicialExistenteConsulta : IRequest<List<ObtieneAutoridadJudicialExistenteDto>>
{
    public long AsuntoNeunId { get; set; }
    public string NoExp { get; set; }
}
