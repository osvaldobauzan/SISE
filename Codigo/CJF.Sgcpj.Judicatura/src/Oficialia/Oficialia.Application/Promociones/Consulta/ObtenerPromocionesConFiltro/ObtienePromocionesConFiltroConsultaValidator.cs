using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtienePromocionesConFiltro;
using CJF.Sgcpj.Judicatura.Common.Application.Utils;
using FluentValidation;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerPromocionesConFiltro;
public class ObtienePromocionesConFiltroConsultaValidator : AbstractValidator<ObtienePromocionesConFiltroConsulta>
{
    public ObtienePromocionesConFiltroConsultaValidator()
    {
        RuleFor(c => c.FechaInicial)
         .NotEmpty().WithMessage("La fecha inical es requerida")
         .Must(f => ValidationsUtils.EsUnaFechaValida(f))
         .WithMessage("Fecha inicial inválida");
        RuleFor(c => c.FechaFinal)
         .NotEmpty().WithMessage("La fecha inical es requerida")
         .Must(f => ValidationsUtils.EsUnaFechaValida(f))
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


}
