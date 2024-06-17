using CJF.Sgcpj.Judicatura.Actuaria.Domain.Entities;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.EditarSintesis;
public class EditarSintesisComando : IRequest<bool>
{
    public EditarSintesisDto SintesisAcuerdo { get; set; }
}
