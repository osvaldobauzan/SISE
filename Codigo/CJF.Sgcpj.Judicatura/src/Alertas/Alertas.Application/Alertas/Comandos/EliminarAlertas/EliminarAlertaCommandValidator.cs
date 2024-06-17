using FluentValidation;

namespace CJF.Sgcpj.Judicatura.Alertas.Application.Alertas.Comandos.EliminarAlertas;
public class EliminarAlertasCommandValidator : AbstractValidator<EliminarAlertasCommand>
{
    public EliminarAlertasCommandValidator()
    {
        RuleFor(s => s.AlertId)
                .NotNull()
                .NotEmpty()
                .WithMessage("El parámetro AlertId es requerido");
    }
}
