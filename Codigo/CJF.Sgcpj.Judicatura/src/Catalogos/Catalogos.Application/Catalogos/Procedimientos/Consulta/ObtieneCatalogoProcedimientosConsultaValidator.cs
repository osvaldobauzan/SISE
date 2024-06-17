using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Procedimientos.Consulta;
public class ObtieneCatalogoProcedimientosConsultaValidator : AbstractValidator<ObtieneCatalogoProcedimientosConsulta>
{
    public ObtieneCatalogoProcedimientosConsultaValidator()
    {
        RuleFor(c => c.CatTipoAsuntoId).NotEmpty().WithMessage("Se requiere el campo CatTipoAsuntoId");
    }

}

