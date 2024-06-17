using FluentValidation;

namespace CJF.Sgcpj.Judicatura.Oficialia.Application.Promociones.Comandos.AgregarPromociones;

public class AgregarPromocionComandoValidator : AbstractValidator<AgregarPromocionComando>
{
    public AgregarPromocionComandoValidator()
    {
        List<int> asuntosConProcedimientos = new List<int> { 6, 9, 18, 74, 72, 67, 125, 126 };

        //RuleFor(s => s.Promocion.NumeroOCC).NotEmpty().WithMessage("El campo NumeroOCC es requerido");
        RuleFor(s => s.Promocion.TipoAsunto.CatTipoAsuntoId).NotEmpty().WithMessage("El campo CatTipoAsuntoId es requerido");
        RuleFor(s => s.Promocion.Cuaderno.CuadernoId).NotEmpty().WithMessage("El campo CuadernoId es requerido");
        RuleFor(s => s.Promocion.HoraPresentacion).NotEmpty().WithMessage("El campo HoraPresentacion es requerido");
        RuleFor(s => s.Promocion.FechaPresentacion).NotEmpty().WithMessage("El campo FechaPresentacion es requerido");
        RuleFor(s => s.Promocion.Copias)
                  .GreaterThanOrEqualTo(0).WithMessage("El campo Copias es requerido y no puede ser menor que 0");
        RuleFor(s => s.Promocion.NumeroExpediente).NotEmpty().WithMessage("El campo NumeroExpediente es requerido");
        RuleFor(s => s.Promocion.Origen).NotNull().WithMessage("El campo Origen es requerido");
        RuleFor(s => s.Promocion.Registro).NotEmpty().WithMessage("El campo Registro es requerido");
        RuleFor(s => s.Promocion.Contenido.ID).NotEmpty().WithMessage("El campo TipoContenidoId es requerido");

        RuleFor(s => s.Promocion.TipoProcedimiento.ID).NotEmpty().When(x => asuntosConProcedimientos.Contains(x.Promocion.TipoAsunto.CatTipoAsuntoId)).WithMessage("El TipoProcedimientoId es requerido");
    }
}
