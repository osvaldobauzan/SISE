using FluentValidation;

namespace CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.Sentencia;
public class EstadoSentenciaConsultaValidator : AbstractValidator<EstadoSentenciaConsulta>
{
    public EstadoSentenciaConsultaValidator()
    {
        RuleFor(c => c.AsuntoNeunId)
          .NotEmpty().WithMessage("Es requerido un NEUN")
          .GreaterThanOrEqualTo(1)
          .WithMessage("El NEUN debe ser mayor que 0");
    }    
}
