using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.AgregarSintesisManual;
public class AgregarSintesisManualRequest : IRequest<bool>
{
    public int TipoCuaderno { get; set; }
    public long AsuntoNeunId { get; set; }
    public DateTime FechaAuto { get; set; }
    public DateTime FechaPublicacion { get; set; }
    public long Titular { get; set; }
    public long Actuario { get; set; }
    public string Sintesis { get; set; }
    public int Quejoso { get; set; }
    public int Autoridad { get; set; }
}
