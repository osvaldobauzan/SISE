using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerTramitesConFiltro;
public class ObtieneTramitesConFiltroConsultaValidator : AbstractValidator<ObtieneTramitesConFiltroConsulta>
{
    public ObtieneTramitesConFiltroConsultaValidator()
    {
        RuleFor(c => c.FechaInicial)
         .NotEmpty().WithMessage("La fecha inical es requerida")
         .Must(f => EsUnaFechaValida(f))
         .WithMessage("Fecha inicial inválida");
        RuleFor(c => c.FechaFinal)
         .NotEmpty().WithMessage("La fecha inical es requerida")
         .Must(f => EsUnaFechaValida(f))
         .WithMessage("Fecha final inválida");

        RuleFor(c => c.Pagina)
          .NotEmpty().WithMessage("Pagina es requerida")
          .GreaterThanOrEqualTo(1)
          .WithMessage("La página debe ser mayor a 0");

        RuleFor(c => c.OrdenarPor)
         .NotEmpty().WithMessage("OrdenarPor es requerido");


        RuleFor(c => c.RegistrosPorPagina)
          .NotNull().WithMessage("RegistrosPorPagina es requerido")
          .GreaterThanOrEqualTo(0)
          .WithMessage("El número de registros por página debe ser mayor o igual a 0")
          .LessThanOrEqualTo(1000)
          .WithMessage("El número de registros por página debe ser menor a 1000");
    }

    private bool EsUnaFechaValida(string value)
    {
        DateTime dt;
        return DateTime.TryParseExact(value,
                            "dd/MM/yyyy",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out dt);
    }
}
