using FluentValidation;

namespace ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.InformacionParte;
public class InformacionParteConsultaValidator : AbstractValidator<InformacionParteConsulta>
{
    public InformacionParteConsultaValidator()
    {
        RuleFor(c => c.AsuntoNeunId)
          .NotEmpty().WithMessage("Es requerido un NEUN")
          .GreaterThanOrEqualTo(1)
          .WithMessage("El NEUN debe ser mayor que 0");
    }
}
