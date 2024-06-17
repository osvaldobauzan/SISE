using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.Common.Models;
using FluentValidation;

namespace ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.FichaTecnica;
public class ObtenerFichaTecnicaExpedienteElectronicoValidator : AbstractValidator<FichaTecnicaExpedienteElectronicoFiltro>
{
    public ObtenerFichaTecnicaExpedienteElectronicoValidator()
    {
        RuleFor(c => c.AsuntoNeunId)
          .NotEmpty().WithMessage("Es requerido un NEUN")
          .GreaterThanOrEqualTo(1)
          .WithMessage("El NEUN debe ser mayor que 0");
    }
}
