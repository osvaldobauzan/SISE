using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using CJF.Sgcpj.Judicatura.LibretaOficios.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.LibretaOficios.Application.LibretaOficios.Consultas;
public class ObtenerLibretaOficioValidator : AbstractValidator<LibretaOficioFiltro>
{
    public ObtenerLibretaOficioValidator()
    {
        RuleFor(c => c.CantidadRegistros)
          .NotEmpty().WithMessage("Pagina es requerida")
          .GreaterThanOrEqualTo(1)
          .WithMessage("La página debe ser mayor a 0");

        RuleFor(c => c.NoRegistros)
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

