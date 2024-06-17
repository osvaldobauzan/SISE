using FluentValidation;

namespace ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.DatosGenerales;
public class DatosGeneralesConsultaValidator : AbstractValidator<DatosGeneralesConsulta>
{
    public DatosGeneralesConsultaValidator()
    {
        RuleFor(c => c.AsuntoNeunId)
          .NotEmpty().WithMessage("Es requerido un NEUN")
          .GreaterThanOrEqualTo(1)
          .WithMessage("El NEUN debe ser mayor que 0");
    }
}
