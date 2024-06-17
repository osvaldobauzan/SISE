using FluentValidation;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Audiencia.Consulta;
public class CatalogoAudienciaValidator : AbstractValidator<CatalogAudienciaRequest>
{
    public CatalogoAudienciaValidator()
    {
        RuleFor(c => c.TipoAsuntoId).NotEmpty().WithMessage("Se requiere el campo TipoAsuntoId");
    }
}
