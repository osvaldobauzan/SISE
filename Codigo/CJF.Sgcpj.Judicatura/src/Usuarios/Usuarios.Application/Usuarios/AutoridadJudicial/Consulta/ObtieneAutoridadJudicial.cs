using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Usuarios.Application.Usuarios.AutoridadJudicial.Consulta;

public record ObtieneAutoridadJudicial : IRequest<List<AutoridadJudicialDto>>
{
    public string Nombre { get; set; }
}
