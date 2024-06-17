using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Comandos.ModificarEstado;
public class ModificarEstadoValidator : AbstractValidator<ModificarEstadoRequest>
{
    public ModificarEstadoValidator()
    {
        RuleFor(c => c.IdResultado).NotEmpty().WithMessage("Se requiere el campo IdResultado");
        RuleFor(c => c.IdAgenda).NotEmpty().WithMessage("Se requiere el campo IdAgenda");
    }
}
