using CJF.Sgcpj.Judicatura.Actuaria.Domain.Entities;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.GuardarSintesis;
public class GuardarSintesisComando : IRequest<bool>
{
    public GuardarSintesisDto SintesisAcuerdo { get; set; }

}
