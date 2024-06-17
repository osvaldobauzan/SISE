using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Comandos.ModificarEstado;
using FluentValidation;

namespace CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Consulta.ObtenerDetalleCaracter;
public class ObtenerDetalleCaracterValidator : AbstractValidator<ObtenerDetalleCaracterRequest>
{
    public ObtenerDetalleCaracterValidator()
    {
        RuleFor(c => c.IdNeun).NotEmpty().WithMessage("Se requiere el campo IdNeun");
        RuleFor(c => c.IdTipoAsunto).NotEmpty().WithMessage("Se requiere el campo IdTipoAsunto");
    }
}
