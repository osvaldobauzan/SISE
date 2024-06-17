using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Utils;
using FluentValidation;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.SubirAcuse;

public class SubirAcuseValidatorValidator : AbstractValidator<SubirAcuseComando>
{
    public SubirAcuseValidatorValidator()
    {
        RuleFor(c => c.AcuseDto.FechaNotificacion)
        .NotEmpty().WithMessage("La FechaNotificacion es requerida")
        .Must(f => ValidationsUtils.EsUnaFechaValida(f))
        .WithMessage("FechaNotificacion inválida");
        RuleFor(c => c.AcuseDto.FechaNotificacionCitatorio)
         
         .Must(f => string.IsNullOrEmpty(f) || ValidationsUtils.EsUnaFechaValida(f))
         .WithMessage("FechaNotificacionCitatorio inválida");
    }
}