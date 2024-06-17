using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ExpedienteElectronico.Application.Common.Models;

public class PersonaAsuntoDelete : IRequest<bool>
{
    public Int64 PersonaId { get; set; }
    public Int64 UsuarioElimina { get; set; }
}