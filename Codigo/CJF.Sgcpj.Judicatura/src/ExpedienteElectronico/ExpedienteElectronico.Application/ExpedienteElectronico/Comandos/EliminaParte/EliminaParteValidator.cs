using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpedienteElectronico.Application.Common.Models;
using FluentValidation;

namespace ExpedienteElectronico.Application.ExpedienteElectronico.Comandos.EliminaParte;

public class EliminaParteValidator : AbstractValidator<PersonaAsuntoDelete>
{
    public EliminaParteValidator()
    {
        RuleFor(c => c.PersonaId)
          .NotEmpty().WithMessage("Es requerido un Identificador de Persona")
          .GreaterThanOrEqualTo(1)
          .WithMessage("El identificador de la persona debe ser mayor que 0");
    }
}
