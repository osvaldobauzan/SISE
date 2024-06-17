using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ValidarExpediente;
using FluentValidation;

namespace CJF.Sgcpj.Judicatura.Proyectos.Proyectos.Application.Proyectos.Consultas.ValidarExpediente;

public class ValidarExpedienteConsultaValidator : AbstractValidator<ValidarExpedienteConsulta>
{
    public ValidarExpedienteConsultaValidator()
    {
        RuleFor(a => a.CatCuadernoId).GreaterThan(0);
    }
}
