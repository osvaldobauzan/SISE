using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.Common.Models;
public class ExpedienteElectronicoFiltro:IRequest<List<DetalleExpedienteElectronico>>
{
    /// <summary>
    /// Obtiene o establece el identificador del NEUN.
    /// </summary>
    public long AsuntoNeunId { get; set; }
}
