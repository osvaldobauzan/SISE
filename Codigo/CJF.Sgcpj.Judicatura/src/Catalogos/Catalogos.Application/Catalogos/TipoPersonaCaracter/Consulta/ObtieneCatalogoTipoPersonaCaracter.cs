using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.TipoPersonaCaracter.Consulta;
public record ObtieneCatalogoTipoPersonaCaracter : IRequest<List<CatalogoTipoPersonaCaracterDto>>
{
    public int TipoAsuntoId { get; set; }
}
