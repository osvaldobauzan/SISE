using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Comandos.ModificarEstado;
using FluentValidation;

namespace CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Comandos.InsertarAgenda;
public class InsertarAgendaValidator : AbstractValidator<InsertarAgendaRequest>
{
    public InsertarAgendaValidator()
    {
        RuleFor(c => c.NumeroNeun).NotEmpty().WithMessage("Se requiere el campo NumeroNeun");
        RuleFor(c => c.Expendiente).NotEmpty().WithMessage("Se requiere el campo Expendiente");
        RuleFor(c => c.IdTipoAsunto).NotEmpty().WithMessage("Se requiere el campo IdTipoAsunto");
        RuleFor(c => c.IdTipoAudiencia).NotEmpty().WithMessage("Se requiere el campo IdTipoAudiencia");
        RuleFor(c => c.FechaAudiencia).NotEmpty().WithMessage("Se requiere el campo FechaAudiencia");
        RuleFor(c => c.HoraAudiencia).NotEmpty().WithMessage("Se requiere el campo HoraAudiencia");
    }
}
