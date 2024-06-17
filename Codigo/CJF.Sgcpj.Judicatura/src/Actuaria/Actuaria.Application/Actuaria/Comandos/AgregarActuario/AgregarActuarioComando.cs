using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.AgregarActuario;
public class AgregarActuarioComando : IRequest<bool>
{
    public AgregarActuarioDto Actuario { get; set; }
}
