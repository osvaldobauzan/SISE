using FluentValidation;

namespace CJF.Sgcpj.Judicatura.Alertas.Application.Alertas.Comandos.EnviarAlerta;
public class EnviarAlertasCommandValidator : AbstractValidator<EnviarAlertasCommand>
{
    public EnviarAlertasCommandValidator()
    {
        RuleFor(s => s.Alerts).NotEmpty().NotNull().WithMessage("Las alertas deben tener contenido");
    }
}
