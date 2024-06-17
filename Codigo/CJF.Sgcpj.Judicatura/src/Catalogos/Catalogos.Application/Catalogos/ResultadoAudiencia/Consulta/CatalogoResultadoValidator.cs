using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Audiencia.Consulta;
using FluentValidation;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.ResultadoAudiencia.Consulta;
public class CatalogoResultadoValidator : AbstractValidator<CatalogoResultadoRequest>
{
    public CatalogoResultadoValidator()
    {
        RuleFor(c => c.IdTipoAudiencia).NotEmpty().WithMessage("Se requiere el campo IdTipoAudiencia");
    }
}
