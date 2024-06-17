using FluentValidation;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Comandos.ReasignarSecretarioComando;

public class ReasignarSecretarioComandoValidator : AbstractValidator<ReasignarSecretarioComando>
{
    public ReasignarSecretarioComandoValidator()
    {
        RuleFor(a => a.SecretarioNuevoId).GreaterThan(0).WithMessage("Información obligatoria");

        RuleFor(a => a.ProyectosId).NotNull().NotEmpty().WithMessage("Información obligatoria");
    }
}
