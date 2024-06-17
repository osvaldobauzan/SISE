using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Utils;
using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.Common.Models;
using ExpedienteElectronico.Application.Common.Models;
using FluentValidation;

namespace ExpedienteElectronico.Application.ExpedienteElectronico.Comandos.InsertaParte;

public class InsertaParteValidator : AbstractValidator<PersonaAsuntoInsert>
{
    public InsertaParteValidator()
    {
        RuleFor(c => c.AsuntoNeunId)
            .NotEmpty().WithMessage("Es requerido un NEUN")
            .GreaterThanOrEqualTo(1)
            .WithMessage("El NEUN debe ser mayor que 0");
        RuleFor(c => c.PersonaAsunto)
            .NotEmpty().WithMessage("Es requerida la captura de los datos de la parte");
        RuleFor(c => c.PersonaAsunto.Nombre)
            .NotEmpty().WithMessage("Es requerido el nombre de la parte");
        RuleFor(c => c.PersonaAsunto.CatTipoPersonaId)
            .NotEmpty().WithMessage("Es requerido el Tipo de Persona")
            .GreaterThanOrEqualTo(1)
            .WithMessage("El Tipo de Persona debe ser mayor que 0");
        RuleFor(c => c.PersonaAsunto.CatCaracterPersonaAsuntoId)
           .NotEmpty().WithMessage("Es requerido el Carácter de la persona")
            .GreaterThanOrEqualTo(1)
            .WithMessage("El Carácter de la persona debe ser mayor que 0");
        RuleFor(c => c.PersonaAsunto.FechaAceptaOponePublicarDatos)
            .Must(f => (string.IsNullOrEmpty(f) || ValidationsUtils.EsUnaFechaValida(f)))
            .WithMessage("La Fecha auto oposición publicación datos personales no es válida");
    }
}
