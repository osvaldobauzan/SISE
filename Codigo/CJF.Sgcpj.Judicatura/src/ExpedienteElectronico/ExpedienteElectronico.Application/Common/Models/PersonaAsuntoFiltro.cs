using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ExpedienteElectronico.Application.Common.Models;
public class PersonaAsuntoFiltro:IRequest<PersonaAsuntoDTO>
{
    public Int64 PersonaId { get; set; }
}
