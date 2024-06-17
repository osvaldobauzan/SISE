using System.Globalization;
using FluentValidation;

namespace CJF.Sgcpj.Judicatura.Sentencias.Application.Sentencias.Consultas.ObtenerSentencias;

public class ObtenerSentenciasFiltroValidator : AbstractValidator<ObtenerSentenciasFiltro>
{
    public ObtenerSentenciasFiltroValidator()
    {
        RuleFor(c => c.Fecha)
         .NotEmpty().WithMessage("La fecha Inicial es requerida")
         .Must(f => EsUnaFechaValida(f))
         .WithMessage("Fecha inicial inválida");

        RuleFor(c => c.FechaFin)
         .NotEmpty().WithMessage("La fecha final es requerida")
         .Must(f => EsUnaFechaValida(f))
         .WithMessage("Fecha final inválida");
    }

    private static bool EsUnaFechaValida(string value)
    {
        return DateTime.TryParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }
}
