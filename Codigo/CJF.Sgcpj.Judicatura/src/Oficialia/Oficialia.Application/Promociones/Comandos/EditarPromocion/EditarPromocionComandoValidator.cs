using FluentValidation;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Comandos;

public class EditarPromocionComandoValidator : AbstractValidator<EditarPromocionComando>
{
    
    public EditarPromocionComandoValidator()
    {
        List<int> asuntosConProcedimientos = new List<int> { 6, 9, 18, 74, 72, 67, 125, 126 };
    }
}
