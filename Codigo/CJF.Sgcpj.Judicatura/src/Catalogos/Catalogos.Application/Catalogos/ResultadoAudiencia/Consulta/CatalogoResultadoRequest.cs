using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Audiencia.Consulta;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.ResultadoAudiencia.Consulta;
public class CatalogoResultadoRequest : IRequest<List<CatalogoResultadoDto>>
{
    public int IdTipoAudiencia { get; set; }    
}
