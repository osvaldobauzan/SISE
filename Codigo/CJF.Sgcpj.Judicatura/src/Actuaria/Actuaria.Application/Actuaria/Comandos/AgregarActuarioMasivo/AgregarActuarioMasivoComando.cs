using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.AgregarActuarioMasivo;
public class AgregarActuarioMasivoComando : IRequest<bool>
{
    public AgregarActuarioMasivoDto Actuario {  get; set; } 
}
