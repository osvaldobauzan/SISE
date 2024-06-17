using FluentValidation;

namespace CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.Audiencia;
public class AudienciaConsultaValidator : AbstractValidator<AudienciaConsulta>
{
    public AudienciaConsultaValidator()
    {
        RuleFor(s => s.AsuntoNeunId)
          .NotEmpty().WithMessage("Es requerido un NEUN")
          .GreaterThanOrEqualTo(1)
          .WithMessage("El NEUN debe ser mayor que 0");
        RuleFor(s => s.CuadernoId)
            .NotNull().WithMessage("Parámetro CuadernoId requerido");
    }
}
