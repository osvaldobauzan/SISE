using MediatR;
namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.AgregarCOE;

public class AgregarCOEComando : IRequest<bool>
{ 
    public AgregarCOEDto COE { get; set; }
}
