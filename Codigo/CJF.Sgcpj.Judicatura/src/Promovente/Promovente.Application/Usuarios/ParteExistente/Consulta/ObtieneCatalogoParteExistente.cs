using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Promovente.Application.Usuarios.ParteExistente.Consulta;

public record ObtieneCatalogoParteExistente : IRequest<List<CatalogoParteExistenteDto>>
{
    public long AsuntoNeunId { get; set; }
    public string NoExp { get; set; }
    public string Texto { get; set; }
    public int Modulo { get; set; }
    public int TipoParte { get; set; }
}
